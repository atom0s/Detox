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

namespace DetoxAPI.EventArgs
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ConsoleCommandArgs : HandledEventArgs
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="args"></param>
        public ConsoleCommandArgs(string raw, List<string> args)
        {
            this.RawMessage = raw;
            this.Arguments = args;
        }

        /// <summary>
        /// Gets the raw message sent to the console.
        /// </summary>
        public string RawMessage { get; internal set; }

        /// <summary>
        /// Gets the list of parsed arguments from the raw command.
        /// </summary>
        public List<string> Arguments { get; internal set; }
    }
}
