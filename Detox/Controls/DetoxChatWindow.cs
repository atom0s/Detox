// -- Detox License ------------------------------------------------------
//
//    This file is part of Detox.
//
//    Detox is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    Detox is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with Detox.  If not, see <http://www.gnu.org/licenses/>.
// -----------------------------------------------------------------------

namespace Detox.Controls
{
    using Classes;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using TomShane.Neoforce.Controls;

    public class DetoxChatWindow : Panel
    {
        /// <summary>
        /// Internal list of chat history.
        /// </summary>
        private EventedList<ChatMessage> _messages;

        /// <summary>
        /// Text input control to allow chatting.
        /// </summary>
        private readonly TextBox _chatInput;

        /// <summary>
        /// Text scroller to view previous chat messages.
        /// </summary>
        private readonly ScrollBar _chatScroller;

        /// <summary>
        /// Flag to determine if the chat background is visible.
        /// </summary>
        private bool _isBackgroundVisible;

        /// <summary>
        /// Delegate used to send chat messages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ChatMessagDelegate(object sender, ChatMessageEventArgs e);

        /// <summary>
        /// Chat event to obtain sent messages.
        /// </summary>
        public event ChatMessagDelegate MessageSent;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="manager"></param>
        public DetoxChatWindow(Manager manager)
            : base(manager)
        {
            // Initialize class properties..
            this._messages = new EventedList<ChatMessage>();
            this._messages.ItemAdded += ChatMessages_OnItemAdded;
            this._isBackgroundVisible = true;

            // Initialize base control..
            base.Init();
            base.Width = 350;
            base.Height = 150;
            base.Alpha = 255;
            base.MinimumWidth = 64;
            base.MinimumHeight = 64;
            base.CanFocus = true;
            base.Movable = true;
            base.Resizable = true;
            base.ClientArea.Draw += ClientArea_OnDraw;
            //
            //
            //
            this._chatInput = new TextBox(manager);
            this._chatInput.Init();
            this._chatInput.Anchor = Anchors.Left | Anchors.Right | Anchors.Bottom;
            this._chatInput.AutoSelection = false;
            this._chatInput.Detached = false;
            this._chatInput.Left = 0;
            this._chatInput.Top = base.Height - this._chatInput.Height;
            this._chatInput.Visible = true;
            this._chatInput.KeyDown += ChatInput_OnKeyDown;
            this._chatInput.FocusGained += ChatInput_OnFocusGained;
            this._chatInput.FocusLost += ChatInput_OnFocusLost;
            //
            //
            //
            this._chatScroller = new ScrollBar(manager, Orientation.Vertical)
                {
                    Anchor = Anchors.Right | Anchors.Top | Anchors.Bottom,
                    Left = base.Width - 16,
                    PageSize = 1,
                    Range = 1,
                    Top = 2,
                    Value = 0
                };
            this._chatScroller.ValueChanged += ChatScroller_OnValueChanged;
            this._chatScroller.Init();
            //
            //
            //
            base.Add(this._chatInput, false);
            base.Add(this._chatScroller, false);
            manager.Add(this);

            // Update the control positions..
            this.PositionControls();
            base.Left = 5;
            base.Top = Terraria.MainGame.Window.ClientBounds.Height - base.Height - 5;
        }

        /// <summary>
        /// Repositions the chat window controls.
        /// </summary>
        private void PositionControls()
        {
            // Ensure the controls are valid..
            if (this._chatInput == null || this._chatScroller == null)
                return;

            // Reposition the chat input..
            this._chatInput.Left = 0;
            this._chatInput.Width = base.Width;

            // Reposition the chat scroller..
            this._chatScroller.Height = base.Height - this._chatInput.Height - 5;

            // Reposition the client margins..
            base.ClientMargins = new Margins(0, 0, this._chatScroller.Width, this._chatInput.Height + 5);

            // Redraw the control..
            base.Invalidate();
        }

        /// <summary>
        /// Calculates the scrollbar values based on message history.
        /// </summary>
        private void CalculateScrolling()
        {
            // Ensure the control is created..
            if (this._chatScroller == null)
                return;

            // Prepare needed variables..
            var lineSpace = base.Skin.Layers[0].Text.Font.Resource.LineSpacing;
            var lineCount = this._messages.Count;
            var page = (int)Math.Ceiling(base.ClientArea.ClientHeight / (float)lineSpace);

            // Update the control values..
            this._chatScroller.Range = lineCount == 0 ? 1 : lineCount;
            this._chatScroller.PageSize = lineCount == 0 ? 1 : page;
            this._chatScroller.Value = this._chatScroller.Range;
        }

        /// <summary>
        /// InitSkin override to load the skin for a window control.
        /// </summary>
        protected override void InitSkin()
        {
            base.InitSkin();
            base.Skin = new SkinControl(base.Manager.Skin.Controls["Window"]);

            // Update the control positions..
            this.PositionControls();
        }

        /// <summary>
        /// DrawControl override to properly position the control.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="rect"></param>
        /// <param name="gameTime"></param>
        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            // Calculate new position..
            var height = this._chatInput.Height + 1;
            var r = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height - height);

            // Draw the repositioned control..
            base.DrawControl(renderer, r, gameTime);
        }

        /// <summary>
        /// OnResize override to reposition the control properly.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // Adjust the controls..
            this.CalculateScrolling();
            this.PositionControls();
        }

        /// <summary>
        /// Update override to adjust game chat mode.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Set the chat mode based on our controls focus..
            Terraria.SetMainField("chatMode", this._chatInput.Focused);
        }

        /// <summary>
        /// Client area draw event callback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientArea_OnDraw(object sender, DrawEventArgs e)
        {
            // Ignore if we have no history..
            if (this._messages.Count == 0)
                return;

            // Prepare variables..
            var font = Detox.TinyFont;
            var rect = e.Rectangle;
            var pos = 0;

            // Determine the chat lines to display..
            for (var x = (this._chatScroller.Value + this._chatScroller.PageSize) - 1;
                (x >= (this._chatScroller.Value + this._chatScroller.PageSize) - this._chatScroller.PageSize);
                x--)
            {
                const int posX = 4;
                var posY = rect.Bottom - (pos + 1) * font.LineSpacing;

                // Draw outline around the text..
                for (var y = -1; y <= 1; y++)
                    for (var z = -1; z <= 1; z++)
                        e.Renderer.DrawString(font, this._messages[x].Message, posX + y, posY - z, Color.Black);

                // Draw the chat text..
                e.Renderer.DrawString(font, this._messages[x].Message, posX, posY, this._messages[x].Color);
                pos += 1;
            }
        }

        /// <summary>
        /// FocusGained callback to enable the Terraria chat mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatInput_OnFocusGained(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            this._chatInput.TextColor = Color.White;
            Terraria.SetMainField("chatMode", true);
        }

        /// <summary>
        /// FocusLost callback to disable the Terraria chat mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatInput_OnFocusLost(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            Terraria.SetMainField("chatMode", false);
        }

        /// <summary>
        /// KeyDown callback to process the input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatInput_OnKeyDown(object sender, KeyEventArgs e)
        {
            // Play the chat click sound..
            var playSound = Detox.Terraria.GetType("Terraria.Main").GetMethod("PlaySound");
            if (playSound != null)
                playSound.Invoke(null, new object[] { 12, -1, -1, 1 });

            // Obtain the message to send..
            var message = this._chatInput.Text.Trim();

            // Process the enter key..
            if ((e.Key == Keys.Enter) && !string.IsNullOrEmpty(message))
            {
                // Send the current message..
                var msg = new ChatMessageEventArgs(new ChatMessage
                    {
                        Color = Color.White,
                        Message = message,
                        Timestamp = DateTime.Now
                    });
                this.MessageSent(this, msg);
                e.Handled = true;

                // Update the control..
                this._chatInput.Text = string.Empty;
                base.ClientArea.Invalidate();
                this.CalculateScrolling();
            }

                // Cleanup invalid text..
            else if ((e.Key == Keys.Enter) && string.IsNullOrEmpty(message))
            {
                this._chatInput.Text = string.Empty;
                e.Handled = true;
            }
        }

        /// <summary>
        /// ValueChanged callback to update the client area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatScroller_OnValueChanged(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            base.ClientArea.Invalidate();
        }

        /// <summary>
        /// ItemAdded callback to update the client area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatMessages_OnItemAdded(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            // Trim the chat history..
            while (this._messages.Count > 100)
                this._messages.RemoveAt(0);

            base.ClientArea.Invalidate();
            this.CalculateScrolling();
        }

        /// <summary>
        /// Gets or sets the message buffer object.
        /// </summary>
        public EventedList<ChatMessage> MessageBuffer
        {
            get { return this._messages; }
            set
            {
                this._messages.ItemAdded -= ChatMessages_OnItemAdded;
                this._messages = value;
                this._messages.ItemAdded += ChatMessages_OnItemAdded;
            }
        }

        /// <summary>
        /// Gets or sets the current chat input text.
        /// </summary>
        public string InputText
        {
            get { return this._chatInput.Text; }
            set { this._chatInput.Text = value; }
        }

        /// <summary>
        /// Gets or sets the focus of the input control.
        /// </summary>
        public bool HasInputFocus
        {
            get { return this._chatInput.Focused; }
            set { this._chatInput.Focused = value; }
        }

        /// <summary>
        /// Gets or sets if the chat background is visible.
        /// </summary>
        public bool BackgroundVisible
        {
            get { return this._isBackgroundVisible; }
            set
            {
                this._isBackgroundVisible = value;
                base.Color = value ? new Color(255, 255, 255, 0) : Color.Transparent;
                base.CanFocus = value;
                this.CanFocus = value;
                base.Movable = value;
                base.Resizable = value;
                this._chatScroller.Visible = value;
            }
        }
    }
}
