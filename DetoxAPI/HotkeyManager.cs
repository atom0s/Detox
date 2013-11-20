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
    using System.Collections.Generic;
    using System.Linq;

    public static class HotkeyManager
    {
        /// <summary>
        /// Internal list of current registered hotkeys.
        /// </summary>
        private static readonly List<Hotkey> _hotkeys;

        /// <summary>
        /// Default Constructor
        /// </summary>
        static HotkeyManager()
        {
            _hotkeys = new List<Hotkey>();
        }

        /// <summary>
        /// Registers the hotkey with the manager.
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="keys"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static bool Register(string alias, List<Keys> keys, HotkeyHandler<System.EventArgs> handler)
        {
            // Ensure this hotkey doesn't exist..
            if (_hotkeys.Any(h => h.Alias == alias.ToLower()))
                return false;

            // Insert the new hotkey..
            _hotkeys.Add(new Hotkey(alias, keys, handler));
            return true;
        }

        /// <summary>
        /// Deregisters the given hotkey by its alias.
        /// </summary>
        /// <param name="alias"></param>
        public static void Deregister(string alias)
        {
            var hotkey = _hotkeys.SingleOrDefault(h => h.Alias == alias.ToLower());
            if (hotkey != null)
                _hotkeys.Remove(hotkey);
        }

        /// <summary>
        /// Processes the current hotkeys.
        /// </summary>
        public static void ProcessHotkeys()
        {
            _hotkeys
                .Where(h => InputHandler.CurrentKeyboard.GetPressedKeys().ToList().Intersect(h.Keys).Count() == h.Keys.Count)
                .Where(h => !InputHandler.PreviousKeyboard.IsKeyDown(h.Keys.Last()))
                .ToList().ForEach(h => h.Handler.Invoke(System.EventArgs.Empty));
        }
    }
}
