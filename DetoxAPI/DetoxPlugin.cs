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

    public abstract class DetoxPlugin : IDisposable
    {
        /// <summary>
        /// Gets or sets the name of this plugin.
        /// </summary>
        public virtual string Name
        {
            get { return "Unknown"; }
        }

        /// <summary>
        /// Gets or sets the author of this plugin.
        /// </summary>
        public virtual string Author
        {
            get { return "Unknown"; }
        }

        /// <summary>
        /// Gets or sets the description of this plugin.
        /// </summary>
        public virtual string Description
        {
            get { return "Unknown"; }
        }

        /// <summary>
        /// Gets or sets the version of this plugin.
        /// </summary>
        public virtual Version Version
        {
            get { return new Version(1, 0, 0); }
        }

        /// <summary>
        /// Default Deconstructor
        /// </summary>
        ~DetoxPlugin()
        {
            this.Dispose(false);
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
        /// Initialize function called when this plugin is first loaded.
        /// </summary>
        /// <returns></returns>
        public virtual bool Initialize()
        {
            return false;
        }
    }
}
