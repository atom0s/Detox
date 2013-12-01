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
    using System;
    using System.IO;

    public class Logging
    {
        /// <summary>
        /// Internal static logging instance.
        /// </summary>
        private static Logging _instance;

        /// <summary>
        /// Gets the singleton instance of this class.
        /// </summary>
        public static Logging Instance
        {
            get { return _instance ?? (_instance = new Logging()); }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        private Logging()
        {
            // Ensure the log path exists..
            if (!Directory.Exists(Path.Combine(Detox.DetoxBasePath, "Log")))
                Directory.CreateDirectory(Path.Combine(Detox.DetoxBasePath, "Log"));

            // Delete the current run log..
            if (File.Exists(Path.Combine(Detox.DetoxBasePath, "Log", "detox_runlog.txt")))
                File.Delete(Path.Combine(Detox.DetoxBasePath, "Log", "detox_runlog.txt"));
        }

        /// <summary>
        /// Logs a message to the log file.
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            using (var sWriter = new StreamWriter(Path.Combine(Detox.DetoxBasePath, "Log", "detox_runlog.txt"), true))
            {
                sWriter.WriteLine("[{0}] {1}", DateTime.Now.ToString("hh:mm:ss tt"), message);
                sWriter.Flush();
            }
        }
    }
}
