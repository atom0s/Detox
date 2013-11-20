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
    using System.Collections.Generic;

    public static class DetoxPluginErrorReason
    {
        /// <summary>
        /// Plugin error enumeration.
        /// </summary>
        public enum ErrorTypes
        {
            /// <summary>
            /// No error; successful.
            /// </summary>
            Success = 0,

            /// <summary>
            /// File was not found.
            /// </summary>
            FileNotFound = 1,

            /// <summary>
            /// File was not a Detox plugin file.
            /// </summary>
            FileNotPlugin = 2,

            /// <summary>
            /// File was missing the DetoxAPIVersion attribute.
            /// </summary>
            FileMissingAttribute = 3,

            /// <summary>
            /// File has an invalid Api version.
            /// </summary>
            InvalidApiVersion = 4,

            /// <summary>
            /// File did not initialize properly.
            /// </summary>
            FailedInitialization = 5,

            /// <summary>
            /// Unknown error or exception occurred.
            /// </summary>
            UnknownErrorOrException = 9999
        }

        /// <summary>
        /// List of error strings that can occur when loading a plugin.
        /// </summary>
        public static List<string> ErrorStrings = new List<string>
            {
                // 0 - Success
                "Success",
                // 1 - FileNotFound
                "File was not found.",
                // 2 - FileNotPlugin
                "File does not contain a class inherting 'DetoxPlugin'.",
                // 3 - FileMissingAttribute
                "File does not define the attribute 'DetoxAPIVersion'.",
                // 4 - InvalidApiVersion
                "Api version mismatch. Plugin is outdated or your Detox client is old. Update(s) needed!",
                // 5 - FailedInitialization
                "Plugin did not initialize properly.",
                // 9999 - UnknownErrorOrException
                "Unknown error or exception occurred."
            };
    }
}
