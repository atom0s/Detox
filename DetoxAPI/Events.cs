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

namespace DetoxAPI
{
    using EventArgs;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using System.ComponentModel;

    /// <summary>
    /// Event handler delegate.
    /// </summary>
    /// <typeparam name="ArgumentType"></typeparam>
    /// <param name="args"></param>
    public delegate void EventHandler<in ArgumentType>(ArgumentType args) where ArgumentType : System.EventArgs;

    /// <summary>
    /// Event Manager
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// Initializes the event objects.
        /// </summary>
        public static void Initialize()
        {
            // Initialize Detox events..
            Events.DetoxEvents.DetoxInitialize = new EventHandlerCollection<System.EventArgs>("DetoxInitialize");

            // Initialize Xna events..
            Events.XnaEvents.PreInitialize = new EventHandlerCollection<System.EventArgs>("PreInitialize");
            Events.XnaEvents.PostInitialize = new EventHandlerCollection<System.EventArgs>("PostInitialize");
            Events.XnaEvents.PreLoadContent = new EventHandlerCollection<XnaLoadContentEventArgs>("PreLoadContent");
            Events.XnaEvents.PostLoadContent = new EventHandlerCollection<XnaLoadContentEventArgs>("PostLoadContent");
            Events.XnaEvents.PreUpdate = new EventHandlerCollection<XnaUpdateEventArgs>("PreUpdate");
            Events.XnaEvents.PostUpdate = new EventHandlerCollection<XnaUpdateEventArgs>("PostUpdate");
            Events.XnaEvents.PreDraw = new EventHandlerCollection<XnaDrawEventArgs>("PreDraw");
            Events.XnaEvents.PostDraw = new EventHandlerCollection<XnaDrawEventArgs>("PosDraw");

            // Initialize Terraria.Main events..
            Events.TerrariaMainEvents.PreDrawInterface = new EventHandlerCollection<System.EventArgs>("PreDrawInterface");
            Events.TerrariaMainEvents.PostDrawInterface = new EventHandlerCollection<System.EventArgs>("PostDrawInterface");

            // Initialize Terraria.Player events..
            Events.PlayerEvents.PreUpdatePlayer = new EventHandlerCollection<PlayerUpdateEventArgs>("PreUpdatePlayer");
            Events.PlayerEvents.PostUpdatePlayer = new EventHandlerCollection<PlayerUpdateEventArgs>("PreUpdatePlayer");
            Events.PlayerEvents.PrePlayerFrame = new EventHandlerCollection<PlayerFrameEventArgs>("PrePlayerFrame");
            Events.PlayerEvents.PostPlayerFrame = new EventHandlerCollection<PlayerFrameEventArgs>("PostPlayerFrame");

            // Initialize Terraria.Recipe events..
            Events.RecipeEvents.PreFindRecipes = new EventHandlerCollection<HandledEventArgs>("PreFindRecipes");
            Events.RecipeEvents.PostFindRecipes = new EventHandlerCollection<System.EventArgs>("PostFindRecipes");
            Events.RecipeEvents.PreSetupRecipes = new EventHandlerCollection<HandledEventArgs>("PreSetupRecipes");
            Events.RecipeEvents.PostSetupRecipes = new EventHandlerCollection<System.EventArgs>("PostSetupRecipes");

            // Initialize Terraria.Lighting events..
            Events.LightingEvents.Brightness = new EventHandlerCollection<LightingBrightnessEventArgs>("PreFindRecipes");
            Events.LightingEvents.LightColor = new EventHandlerCollection<HandledEventArgs>("LightColor");
            Events.LightingEvents.GetColor1 = new EventHandlerCollection<GetColorEventArgs>("GetColor1");
            Events.LightingEvents.GetColor2 = new EventHandlerCollection<GetColorEventArgs>("GetColor2");
        }

        /// <summary>
        /// Events specific to Detox.
        /// </summary>
        public static class DetoxEvents
        {
            /// <summary>
            /// Invokes the Detox Initialize event.
            /// </summary>
            public static void InvokeDetoxInitialize()
            {
                DetoxInitialize.Invoke(System.EventArgs.Empty);
            }

            /// <summary>
            /// Gets the event handler collection for the Detox Initialize event.
            /// </summary>
            public static EventHandlerCollection<System.EventArgs> DetoxInitialize { get; internal set; }
        }

        /// <summary>
        /// Events specific to Xna.
        /// </summary>
        public static class XnaEvents
        {
            /// <summary>
            /// Invokes the Xna PreInitialize event.
            /// </summary>
            public static void InvokePreInitialize()
            {
                PreInitialize.Invoke(System.EventArgs.Empty);
            }

            /// <summary>
            /// Invokes the Xna PostInitialize event.
            /// </summary>
            public static void InvokePostInitialize()
            {
                PostInitialize.Invoke(System.EventArgs.Empty);
            }

            /// <summary>
            /// Invokes the Xna PreLoadContent event.
            /// </summary>
            /// <param name="content"></param>
            public static void InvokePreLoadContent(ContentManager content)
            {
                var args = new XnaLoadContentEventArgs
                    {
                        Content = content
                    };
                PreLoadContent.Invoke(args);
            }

            /// <summary>
            /// Invokes the Xna PostLoadContent event.
            /// </summary>
            /// <param name="content"></param>
            public static void InvokePostLoadContent(ContentManager content)
            {
                var args = new XnaLoadContentEventArgs
                    {
                        Content = content
                    };
                PostLoadContent.Invoke(args);
            }

            /// <summary>
            /// Invokes the Xna PreUpdate event.
            /// </summary>
            /// <param name="gameTime"></param>
            public static void InvokePreUpdate(GameTime gameTime)
            {
                var args = new XnaUpdateEventArgs
                    {
                        GameTime = gameTime
                    };
                PreUpdate.Invoke(args);
            }

            /// <summary>
            /// Invokes the Xna PostUpdate event.
            /// </summary>
            /// <param name="gameTime"></param>
            public static void InvokePostUpdate(GameTime gameTime)
            {
                var args = new XnaUpdateEventArgs
                    {
                        GameTime = gameTime
                    };
                PostUpdate.Invoke(args);
            }

            /// <summary>
            /// Invokes the Xna PreDraw event.
            /// </summary>
            /// <param name="gameTime"></param>
            public static void InvokePreDraw(GameTime gameTime)
            {
                var args = new XnaDrawEventArgs
                    {
                        GameTime = gameTime
                    };
                PreDraw.Invoke(args);
            }

            /// <summary>
            /// Invokes the Xna PostDraw event.
            /// </summary>
            /// <param name="gameTime"></param>
            public static void InvokePostDraw(GameTime gameTime)
            {
                var args = new XnaDrawEventArgs
                    {
                        GameTime = gameTime
                    };
                PostDraw.Invoke(args);
            }

            /// <summary>
            /// Gets the event handler collection for the Xna PreInitialize event.
            /// </summary>
            public static EventHandlerCollection<System.EventArgs> PreInitialize { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Xna PostInitialize event.
            /// </summary>
            public static EventHandlerCollection<System.EventArgs> PostInitialize { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Xna PreLoadContent event.
            /// </summary>
            public static EventHandlerCollection<XnaLoadContentEventArgs> PreLoadContent { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Xna PostLoadContent event.
            /// </summary>
            public static EventHandlerCollection<XnaLoadContentEventArgs> PostLoadContent { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Xna PreUpdate event.
            /// </summary>
            public static EventHandlerCollection<XnaUpdateEventArgs> PreUpdate { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Xna PostUpdate event.
            /// </summary>
            public static EventHandlerCollection<XnaUpdateEventArgs> PostUpdate { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Xna PreDraw event.
            /// </summary>
            public static EventHandlerCollection<XnaDrawEventArgs> PreDraw { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Xna PostDraw event.
            /// </summary>
            public static EventHandlerCollection<XnaDrawEventArgs> PostDraw { get; internal set; }
        }

        /// <summary>
        /// Events that are fired from Terraria.Main functions.
        /// </summary>
        public static class TerrariaMainEvents
        {
            /// <summary>
            /// Invokes the Terraria PreDrawInterface event.
            /// </summary>
            public static void InvokePreDrawInterface()
            {
                PreDrawInterface.Invoke(System.EventArgs.Empty);
            }

            /// <summary>
            /// Invokes the Terraria PostDrawInterface event.
            /// </summary>
            public static void InvokePostDrawInterface()
            {
                PostDrawInterface.Invoke(System.EventArgs.Empty);
            }

            /// <summary>
            /// Gets the event handler collection for the Terraria.Main.DrawInterface event.
            /// </summary>
            public static EventHandlerCollection<System.EventArgs> PreDrawInterface { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Terraria.Main.DrawInterface event.
            /// </summary>
            public static EventHandlerCollection<System.EventArgs> PostDrawInterface { get; internal set; }
        }

        /// <summary>
        /// Events that are fired from Terraria.Player functions.
        /// </summary>
        public static class PlayerEvents
        {
            /// <summary>
            /// Invokes the Player PreUpdatePlayer event.
            /// </summary>
            /// <param name="player"></param>
            public static void InvokePreUpdatePlayer(dynamic player)
            {
                var args = new PlayerUpdateEventArgs
                    {
                        Player = player
                    };
                PreUpdatePlayer.Invoke(args);
            }

            /// <summary>
            /// Invokes the Player PostUpdatePlayer event.
            /// </summary>
            /// <param name="player"></param>
            public static void InvokePostUpdatePlayer(dynamic player)
            {
                var args = new PlayerUpdateEventArgs
                    {
                        Player = player
                    };
                PostUpdatePlayer.Invoke(args);
            }

            /// <summary>
            /// Invokes the Player PrePlayerFrame event.
            /// </summary>
            /// <param name="player"></param>
            public static void InvokePrePlayerFrame(dynamic player)
            {
                var args = new PlayerFrameEventArgs
                    {
                        Player = player
                    };
                PrePlayerFrame.Invoke(args);
            }

            /// <summary>
            /// Invokes the Player PostPlayerFrame event.
            /// </summary>
            /// <param name="player"></param>
            public static void InvokePostPlayerFrame(dynamic player)
            {
                var args = new PlayerFrameEventArgs
                    {
                        Player = player
                    };
                PostPlayerFrame.Invoke(args);
            }

            /// <summary>
            /// Gets the event handler collection for the Player PreUpdatePlayer event.
            /// </summary>
            public static EventHandlerCollection<PlayerUpdateEventArgs> PreUpdatePlayer { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Player PostUpdatePlayer event.
            /// </summary>
            public static EventHandlerCollection<PlayerUpdateEventArgs> PostUpdatePlayer { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Player PrePlayerFrame event.
            /// </summary>
            public static EventHandlerCollection<PlayerFrameEventArgs> PrePlayerFrame { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Player PostPlayerFrame event.
            /// </summary>
            public static EventHandlerCollection<PlayerFrameEventArgs> PostPlayerFrame { get; internal set; }
        }

        /// <summary>
        /// Events that are fired from Terraria.Recipe functions.
        /// </summary>
        public static class RecipeEvents
        {
            /// <summary>
            /// Invokes the Recipe PreFindRecipes event.
            /// </summary>
            public static bool InvokePreFindRecipes()
            {
                var args = new HandledEventArgs(false);
                PreFindRecipes.Invoke(args);
                return args.Handled;
            }

            /// <summary>
            /// Invokes the Recipe PostFindRecipes event.
            /// </summary>
            public static void InvokePostFindRecipes()
            {
                PostFindRecipes.Invoke(System.EventArgs.Empty);
            }

            /// <summary>
            /// Invokes the Recipe PreSetupRecipes event.
            /// </summary>
            public static bool InvokePreSetupRecipes()
            {
                var args = new HandledEventArgs(false);
                PreSetupRecipes.Invoke(args);
                return args.Handled;
            }

            /// <summary>
            /// Invokes the Recipe PostSetupRecipes event.
            /// </summary>
            public static void InvokePostSetupRecipes()
            {
                PostSetupRecipes.Invoke(System.EventArgs.Empty);
            }

            /// <summary>
            /// Gets the event handler collection for the Recipe PreFindRecipes event.
            /// </summary>
            public static EventHandlerCollection<HandledEventArgs> PreFindRecipes { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Recipe PostFindRecipes event.
            /// </summary>
            public static EventHandlerCollection<System.EventArgs> PostFindRecipes { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Recipe PreSetupRecipes event.
            /// </summary>
            public static EventHandlerCollection<HandledEventArgs> PreSetupRecipes { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Recipe PostSetupRecipes event.
            /// </summary>
            public static EventHandlerCollection<System.EventArgs> PostSetupRecipes { get; internal set; }
        }

        /// <summary>
        /// Events that are fired from Terraria.Lighting functions.
        /// </summary>
        public static class LightingEvents
        {
            /// <summary>
            /// Invokes the Lighting Brightness event.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public static float InvokeBrightness(int x, int y)
            {
                var args = new LightingBrightnessEventArgs
                    {
                        X = x,
                        Y = y,
                        Brightness = 0.0f
                    };
                Brightness.Invoke(args);
                return args.Handled ? args.Brightness : -1.0f;
            }

            /// <summary>
            /// Invokes the Lighting LightColor event.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public static bool InvokeLightColor(int x, int y)
            {
                var args = new HandledEventArgs(false);
                LightColor.Invoke(args);
                return args.Handled;
            }

            /// <summary>
            /// Invokes the Lighting GetColor event.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="c"></param>
            /// <returns></returns>
            public static bool InvokeGetColor1(int x, int y, out Color c)
            {
                var args = new GetColorEventArgs
                    {
                        X = x,
                        Y = y,
                        OldColor = Color.White,
                        Color = Color.White,
                        Handled = false
                    };
                GetColor1.Invoke(args);
                c = args.Handled ? args.Color : Color.White;
                return args.Handled;
            }

            /// <summary>
            /// Invokes the Lighting GetColor event
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="oldColor"></param>
            /// <param name="c"></param>
            /// <returns></returns>
            public static bool InvokeGetColor2(int x, int y, Color oldColor, out Color c)
            {
                var args = new GetColorEventArgs
                    {
                        X = x,
                        Y = y,
                        OldColor = oldColor,
                        Color = Color.White,
                        Handled = false
                    };
                GetColor2.Invoke(args);
                c = args.Handled ? args.Color : Color.White;
                return args.Handled;
            }

            /// <summary>
            /// Gets the event handler collection for the Lighting Brightness event.
            /// </summary>
            public static EventHandlerCollection<LightingBrightnessEventArgs> Brightness { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Lighting LightColor event.
            /// </summary>
            public static EventHandlerCollection<HandledEventArgs> LightColor { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Lighting GetColor1 event.
            /// </summary>
            public static EventHandlerCollection<GetColorEventArgs> GetColor1 { get; internal set; }

            /// <summary>
            /// Gets the event handler collection for the Lighting GetColor1 event.
            /// </summary>
            public static EventHandlerCollection<GetColorEventArgs> GetColor2 { get; internal set; }
        }
    }
}
