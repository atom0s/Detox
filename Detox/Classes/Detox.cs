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
    using Microsoft.Xna.Framework.Graphics;
    using Mono.Cecil;
    using System.Reflection;
    using TomShane.Neoforce.Controls;

    public static class Detox
    {
        /// <summary>
        /// Binding flags used to locate objects inside of Terraria.
        /// </summary>
        public const BindingFlags BindFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        /// <summary>
        /// The base path to Detox.exe
        /// </summary>
        public static string DetoxBasePath;

        /// <summary>
        /// The base path to Terraria.exe
        /// </summary>
        public static string TerrariaBasePath;

        /// <summary>
        /// The Terraria assembly definition used for IL editing.
        /// </summary>
        public static AssemblyDefinition TerrariaAsm;

        /// <summary>
        /// The main Terraria assembly file.
        /// </summary>
        public static Assembly Terraria;

        /// <summary>
        /// Global Gui manager object used in Detox.
        /// </summary>
        public static Manager GuiManager;

        /// <summary>
        /// Global console UI object used in Detox.
        /// </summary>
        public static DetoxConsole DetoxConsole;

        /// <summary>
        /// Global chat UI object used in Detox.
        /// </summary>
        public static DetoxChatWindow DetoxChatWindow;

        /// <summary>
        /// Sprite font used for the chat window.
        /// </summary>
        public static SpriteFont TinyFont;
    }
}
