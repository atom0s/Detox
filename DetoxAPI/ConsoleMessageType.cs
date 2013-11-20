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
    /// The type of message this should be sent as to the console.
    /// </summary>
    public enum ConsoleMessageType
    {
        /// <summary>
        /// Informational text that is displayed in charteruse green.
        /// </summary>
        About = 0,

        /// <summary>
        /// Normal text that is displayed in white.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Warning text that is displayed in yellow.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Error text that is displayed in red.
        /// </summary>
        Error = 3
    }
}
