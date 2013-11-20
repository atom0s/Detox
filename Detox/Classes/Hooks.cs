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

namespace Detox.Classes
{
    using Controls;
    using DetoxAPI;
    using DetoxAPI.EventArgs;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;
    using System.IO;
    using TomShane.Neoforce.Controls;

    public static class Hooks
    {
        /// <summary>
        /// Xna PreInitialize event callback.
        /// </summary>
        /// <param name="e"></param>
        public static void OnXnaPreInitialize(System.EventArgs e)
        {
            // Set the content base path..
            Terraria.MainGame.Content.RootDirectory = Path.Combine(Detox.TerrariaBasePath, "Content");

            // Attach resizing event to fix UI manager..
            Terraria.MainGame.Window.AllowUserResizing = true;
            Terraria.MainGame.Window.ClientSizeChanged += (sender, args) =>
                {
                    // Resize the graphics device to the window size..
                    Terraria.MainGraphics.PreferredBackBufferWidth = Terraria.MainGame.Window.ClientBounds.Width;
                    Terraria.MainGraphics.PreferredBackBufferHeight = Terraria.MainGame.Window.ClientBounds.Height;
                    Terraria.MainGraphics.ApplyChanges();

                    // Recreate the UI render target..
                    if (Detox.GuiManager != null)
                        Detox.GuiManager.RenderTarget = null;
                };
        }
        
        /// <summary>
        /// Xna PostInitialize event callback.
        /// </summary>
        /// <param name="e"></param>
        public static void OnXnaPostInitialize(System.EventArgs e)
        {
            // Adjust the game resolution per-configurations..
            Terraria.MainGraphics.PreferredBackBufferWidth = Configurations.Instance.Current.Graphics.StartupWindowWidth;
            Terraria.MainGraphics.PreferredBackBufferHeight = Configurations.Instance.Current.Graphics.StartupWindowHeight;
            Terraria.MainGraphics.ApplyChanges();

            // Prepare the UI manager..
            Detox.GuiManager = new Manager(Terraria.MainGame, Configurations.Instance.Current.Graphics.Skin)
                {
                    AutoCreateRenderTarget = true,
                    SkinDirectory = Path.Combine(Detox.DetoxBasePath, "DetoxContent\\Skins\\")
                };
            Detox.GuiManager.Initialize();

            // Prepare the console window..
            Detox.DetoxConsole = new DetoxConsole(Detox.GuiManager);
            Detox.DetoxConsole.Init();
            Detox.DetoxConsole.Visible = false;

            // Prepare the chat window..
            Detox.DetoxChatWindow = new DetoxChatWindow(Detox.GuiManager);
            Detox.DetoxChatWindow.MessageSent += (sender, args) =>
                {
                    Terraria.SetMainField("chatText", string.Empty);
                    Terraria.SendMessage(args.Message.Message);
                };
            Detox.DetoxChatWindow.Init();
            Detox.DetoxChatWindow.Visible = true;

            // Register hotkey for toggling the console..
            HotkeyManager.Register("consoleWindowToggle", new List<Keys> { Keys.OemTilde }, hargs =>
                {
                    // Toggle the console if chat does not have focus..
                    if (!Detox.DetoxChatWindow.HasInputFocus)
                        Detox.DetoxConsole.Visible = !Detox.DetoxConsole.Visible;

                    // Toggle the focus of the console..
                    if (!Detox.DetoxConsole.Visible)
                        Detox.DetoxConsole.Focused = false;
                });
        }

        /// <summary>
        /// Xna PostLoadContent event callback.
        /// </summary>
        /// <param name="e"></param>
        public static void OnXnaPostLoadContent(XnaLoadContentEventArgs e)
        {
            // Store the sprite batch object..
            Terraria.MainSpriteBatch = Terraria.GetMainField<SpriteBatch>("spriteBatch", true);

            // Load tiny font for chat..
            Detox.TinyFont = e.Content.Load<SpriteFont>(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\Fonts\\tiny"));

            // Override fonts per-configrations..
            if (Configurations.Instance.Current.CustomObjects.UseCustomFonts)
            {
                // Load ou custom fonts..
                var small = e.Content.Load<SpriteFont>(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\Fonts\\small"));
                var medium = e.Content.Load<SpriteFont>(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\Fonts\\medium"));
                var large = e.Content.Load<SpriteFont>(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\Fonts\\large"));
                var huge = e.Content.Load<SpriteFont>(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\Fonts\\huge"));

                // Set the Terraria fonts..
                Terraria.SetMainField("fontCombatText", new[] { medium, large });
                Terraria.SetMainField("fontDeathText", huge);
                Terraria.SetMainField("fontItemStack", small);
                Terraria.SetMainField("fontMouseText", small);
            }

            // Override hp/mp icons per-configurations..
            if (Configurations.Instance.Current.CustomObjects.UseCustomHpMpIcons)
            {
                using (var hpStream = new FileStream(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\hp.png"), FileMode.Open, FileAccess.Read))
                {
                    var hpTexture = Texture2D.FromStream(Terraria.MainGame.GraphicsDevice, hpStream);
                    Terraria.SetMainField("heartTexture", hpTexture);
                }

                using (var hpStream = new FileStream(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\hp2.png"), FileMode.Open, FileAccess.Read))
                {
                    var hpTexture = Texture2D.FromStream(Terraria.MainGame.GraphicsDevice, hpStream);
                    Terraria.SetMainField("heart2Texture", hpTexture);
                }

                using (var mpStream = new FileStream(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\mp.png"), FileMode.Open, FileAccess.Read))
                {
                    var mpTexture = Texture2D.FromStream(Terraria.MainGame.GraphicsDevice, mpStream);
                    Terraria.SetMainField("manaTexture", mpTexture);
                }
            }

            // Override background textures per-configurations..
            if (Configurations.Instance.Current.CustomObjects.UseCustomBackgrounds)
            {
                // Set the inventory background textures..
                using (var bgStream = new FileStream(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\bg.png"), FileMode.Open, FileAccess.Read))
                {
                    var bg = Texture2D.FromStream(Terraria.MainGame.GraphicsDevice, bgStream);
                    Terraria.SetMainField("inventoryBackTexture", bg);
                    for (var x = 2; x <= 12; x++)
                        Terraria.SetMainField(string.Format("inventoryBack{0}Texture", x), bg);
                }

                // Set the npc chat background texture..
                using (var chatBackgroundStream = new FileStream(Path.Combine(Detox.DetoxBasePath, "DetoxContent\\chat_bg.png"), FileMode.Open, FileAccess.Read))
                {
                    var chatBackgroundTexture = Texture2D.FromStream(Terraria.MainGame.GraphicsDevice, chatBackgroundStream);
                    Terraria.SetMainField("chatBackTexture", chatBackgroundTexture);
                }
            }

            // Delete the original Terraria mouse texture..
            var cursorTexture = new Texture2D(Terraria.MainGame.GraphicsDevice, 1, 1);
            cursorTexture.SetData(new[] { Color.Transparent });
            Terraria.SetMainField("cursorTexture", cursorTexture);
        }

        /// <summary>
        /// Xna PreUpdate event callback.
        /// </summary>
        /// <param name="e"></param>
        public static void OnXnaPreUpdate(XnaUpdateEventArgs e)
        {
            // Force the mouse to render..
            Terraria.MainGame.IsMouseVisible = true;

            // Clear current chat text..
            Terraria.SetMainField("chatText", string.Empty);

            // Update the input handler..
            InputHandler.Update();

            // Update the hotkey manager..
            HotkeyManager.ProcessHotkeys();

            // Update the UI manager..
            Detox.GuiManager.Update(e.GameTime);

            // Handle the enter key..
            if (InputHandler.CurrentKeyboard.IsKeyDown(Keys.Enter) && !InputHandler.PreviousKeyboard.IsKeyDown(Keys.Enter)
                && !Terraria.GetMainField<bool>("editSign"))
            {
                if (Detox.DetoxConsole.Visible && !Detox.DetoxConsole.HasInputFocus)
                    Detox.DetoxConsole.HasInputFocus = true;
                else
                    Detox.DetoxChatWindow.HasInputFocus = !Detox.DetoxChatWindow.HasInputFocus;
            }
        }

        /// <summary>
        /// Xna PostUpdate event callback.
        /// </summary>
        /// <param name="e"></param>
        public static void OnXnaPostUpdate(XnaUpdateEventArgs e)
        {
            // Force the mouse to render..
            Terraria.MainGame.IsMouseVisible = true;

            // Clear the chat text and mode..
            Terraria.SetMainField("chatText", string.Empty);
            Terraria.SetMainField("chatMode", false);

            // Update the UI manager..
            Detox.GuiManager.PostUpdate(e.GameTime);
        }

        /// <summary>
        /// Xna PreDraw event callback.
        /// </summary>
        /// <param name="e"></param>
        public static void OnXnaPreDraw(XnaDrawEventArgs e)
        {
            // Begin drawing the UI..
            Detox.GuiManager.BeginDraw(e.GameTime);
            Detox.GuiManager.PreDraw(e.GameTime);
        }

        /// <summary>
        /// Xna PostDraw event callback.
        /// </summary>
        /// <param name="e"></param>
        public static void OnXnaPostDraw(XnaDrawEventArgs e)
        {
            // End drawing the UI..
            Detox.GuiManager.EndDraw();
            Detox.GuiManager.PostDraw(e.GameTime);
        }
    }
}
