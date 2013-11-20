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
    public static class Console
    {
        /// <summary>
        /// Console message delegate used for printing console messages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PrintConsoleMessageDelegate(object sender, ConsoleMessage e);

        /// <summary>
        /// Event to subscribe to to obtain incoming console messages.
        /// </summary>
        public static event PrintConsoleMessageDelegate PrintConsoleMessage;

        /// <summary>
        /// Prints a message to the console event.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public static void PrintConsole(string message, ConsoleMessageType type)
        {
            var msg = new ConsoleMessage(message, type);
            if (Console.PrintConsoleMessage != null)
                Console.PrintConsoleMessage(null, msg);
        }
    }
}
