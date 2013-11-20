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
    public class ChatMessageEventArgs : TomShane.Neoforce.Controls.EventArgs
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="message"></param>
        public ChatMessageEventArgs(ChatMessage message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the chat message of this event.
        /// </summary>
        public ChatMessage Message { get; set; }
    }
}
