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
    using Microsoft.Xna.Framework.Input;

    public static class InputHandler
    {
        /// <summary>
        /// Updates the input handler.
        /// </summary>
        public static void Update()
        {
            // Update the keyboard states..
            InputHandler.PreviousKeyboard = InputHandler.CurrentKeyboard;
            InputHandler.CurrentKeyboard = Keyboard.GetState();

            // Update the mouse states..
            InputHandler.PreviousMouse = InputHandler.CurrentMouse;
            InputHandler.CurrentMouse = Mouse.GetState();
        }

        /// <summary>
        /// Gets the previous keyboard state.
        /// </summary>
        public static KeyboardState PreviousKeyboard { get; set; }

        /// <summary>
        /// Gets the current keyboard state.
        /// </summary>
        public static KeyboardState CurrentKeyboard { get; set; }

        /// <summary>
        /// Gets the previous mouse state.
        /// </summary>
        public static MouseState PreviousMouse { get; set; }

        /// <summary>
        /// Gets the current mouse state.
        /// </summary>
        public static MouseState CurrentMouse { get; set; }
    }
}
