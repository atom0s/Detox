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
    using System;

    public class DetoxPluginContainer : IDisposable
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="file"></param>
        /// <param name="plugin"></param>
        public DetoxPluginContainer(string file, DetoxPlugin plugin)
        {
            this.File = file;
            this.Plugin = plugin;
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Gets the full path to the plugin file.
        /// </summary>
        public string File { get; internal set; }

        /// <summary>
        /// Gets the plugin object.
        /// </summary>
        public DetoxPlugin Plugin { get; internal set; }
    }
}
