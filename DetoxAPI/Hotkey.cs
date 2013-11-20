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

    /// <summary>
    /// Hotkey handler delegate.
    /// </summary>
    /// <typeparam name="ArgumentType"></typeparam>
    /// <param name="args"></param>
    public delegate void HotkeyHandler<in ArgumentType>(ArgumentType args) where ArgumentType : System.EventArgs;

    public class Hotkey
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="keys"></param>
        /// <param name="handler"></param>
        public Hotkey(string alias, List<Keys> keys, HotkeyHandler<System.EventArgs> handler)
        {
            this.Alias = alias;
            this.Keys = keys;
            this.Handler = handler;
        }

        /// <summary>
        /// Gets the hotkey alias.
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        /// Gets the hotkey key list.
        /// </summary>
        public List<Keys> Keys { get; private set; }

        /// <summary>
        /// Gets the hotkey handler.
        /// </summary>
        public HotkeyHandler<System.EventArgs> Handler { get; private set; }
    }
}
