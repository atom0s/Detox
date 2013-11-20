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
    /// <summary>
    /// Console message object.
    /// </summary>
    public class ConsoleMessage : System.EventArgs
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public ConsoleMessage(string message, ConsoleMessageType type)
        {
            this.Message = message;
            this.MessageType = type;
        }

        /// <summary>
        /// Gets or sets this console message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets this console message tye.
        /// </summary>
        public ConsoleMessageType MessageType { get; set; }
    }
}
