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
    using DetoxAPI;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Linq;
    using System.Reflection;

    public static class Terraria
    {
        /// <summary>
        /// Gets the value of a field located inside of the Terraria.Main class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="bUseGameInstance"></param>
        public static T GetMainField<T>(string name, bool bUseGameInstance = false)
        {
            // Attempt to locate the field..
            var field = Detox.Terraria.GetType("Terraria.Main").GetFields(Detox.BindFlags).FirstOrDefault(f => f.Name == name);
            return field == null ? default(T) : (T)field.GetValue(bUseGameInstance ? Terraria.MainGame : (object)Detox.Terraria.GetType("Terraria.Main"));
        }

        /// <summary>
        /// Sets the value of a field located inside of the Terraria.Main class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="bUseGameInstance"></param>
        public static void SetMainField<T>(string name, T value, bool bUseGameInstance = false)
        {
            // Attempt to locate the field..
            var field = Detox.Terraria.GetType("Terraria.Main").GetFields(Detox.BindFlags).FirstOrDefault(f => f.Name == name);
            if (field == null)
                return;

            // Set the fields value..
            field.SetValue(bUseGameInstance ? Terraria.MainGame : (object)Detox.Terraria.GetType("Terraria.Main"), value);
        }

        /// <summary>
        /// Terraria.Main.Ctor hook callback.
        /// </summary>
        /// <param name="game"></param>
        public static void TerrariaConstructor(Game game)
        {
            Logging.Instance.Log("[Detox:Terraria] Terraria.ctor() was called!");

            // Store the game object..
            Terraria.MainGame = game;

            // Obtain the main graphics object..
            Terraria.MainGraphics = Terraria.GetMainField<GraphicsDeviceManager>("graphics", true);

            // Skip the main splash screen if requested..
            // TODO: Add config option here..
            Terraria.SetMainField("showSplash", false);

            // Adjust the version strings..
            var version = Terraria.GetMainField<string>("versionNumber");
            Terraria.SetMainField("versionNumber", string.Format("Detox Client Mod :: by atom0s -- v{0} :: Terraria {1}", Assembly.GetExecutingAssembly().GetName().Version, version));
            Terraria.SetMainField("versionNumber2", string.Format("Detox Client Mod :: by atom0s -- v{0} :: Terraria {1}", Assembly.GetExecutingAssembly().GetName().Version, version));

            Logging.Instance.Log("[Detox:Terraria] Terraria.ctor() completed!");
        }

        /// <summary>
        /// Terraria.Main.NewText hook callback.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public static void TerrariaNewText(string text, byte r = 255, byte g = 255, byte b = 255)
        {
            var msg = new ChatMessage
                {
                    Color = new Color(r, g, b, 255),
                    Message = text,
                    Timestamp = DateTime.Now
                };

            // Add the message and update..
            Detox.DetoxChatWindow.MessageBuffer.Add(msg);
            Detox.DetoxChatWindow.Invalidate();
            Detox.DetoxChatWindow.Refresh();
        }

        /// <summary>
        /// Terraria.Main.SetTitle hook callback.
        /// </summary>
        public static void TerrariaSetTitle()
        {
            // Set the new window title..
            var version = Terraria.GetMainField<string>("versionNumber");
            Terraria.MainGame.Window.Title = version;
        }

        /// <summary>
        /// Terraria.Main.Update hook callback to block input handling.
        /// </summary>
        public static void TerrariaUpdateBlockInput()
        {
            // Ensure a control is focused..
            if (Detox.GuiManager.FocusedControl == null)
                return;

            // Disable the current mouse state..
            Terraria.SetMainField("mouseState", new MouseState(0, 0, InputHandler.CurrentMouse.ScrollWheelValue, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released));
            Terraria.SetMainField("oldMouseState", new MouseState(0, 0, InputHandler.CurrentMouse.ScrollWheelValue, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released));

            // Disable the mouse data..
            Terraria.SetMainField("mouseLeft", false);
            Terraria.SetMainField("mouseRight", false);
            Terraria.SetMainField("mouseHC", false);
            Terraria.SetMainField("mouseLeftRelease", false);
            Terraria.SetMainField("mouseRightRelease", false);

            // Disable player mouse events..
            DetoxPlayers.LocalPlayer["mouseInterface"] = false;
            DetoxPlayers.LocalPlayer["showItemIcon"] = false;
            DetoxPlayers.LocalPlayer["showItemIcon2"] = 0;
        }

        /// <summary>
        /// Invokes the Terraria.NetMessage.SendData function.
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="remoteClient"></param>
        /// <param name="ignoreClient"></param>
        /// <param name="text"></param>
        /// <param name="number"></param>
        /// <param name="number2"></param>
        /// <param name="number3"></param>
        /// <param name="number4"></param>
        /// <param name="number5"></param>
        public static void SendData(PacketTypes msgType, int remoteClient = -1, int ignoreClient = -1, string text = "", int number = 0, float number2 = 0.0f, float number3 = 0.0f, float number4 = 0.0f, int number5 = 0)
        {
            // Obtain the Terraria.NetMessage type..
            var netMessage = Detox.Terraria.GetType("Terraria.NetMessage");
            if (netMessage == null)
                return;

            // Obtain the SendData method..
            var netSend = netMessage.GetMethod("SendData");
            if (netSend == null)
                return;

            // Invoke the method..
            netSend.Invoke(null, new object[]
                {
                    (int)msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5
                });
        }

        /// <summary>
        /// Sends a chat packet to the server.
        /// </summary>
        /// <param name="text"></param>
        public static void SendMessage(string text)
        {
            Terraria.SendData(PacketTypes.Chat, -1, -1, text);
        }

        /// <summary>
        /// Gets or sets the main Terraria game object.
        /// </summary>
        public static Game MainGame { get; set; }

        /// <summary>
        /// Gets or sets the main Terraria graphics device manager.
        /// </summary>
        public static GraphicsDeviceManager MainGraphics;

        /// <summary>
        /// Gets or sets the main Terraria sprite batch object.
        /// </summary>
        public static SpriteBatch MainSpriteBatch;
    }
}
