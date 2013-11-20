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
    using Microsoft.Xna.Framework;
    using System;

    public class ChatMessage
    {
        /// <summary>
        /// Gets or sets this chat message text.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets this chat message color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets this message timestamp.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
