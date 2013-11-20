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
    using System.ComponentModel;

    public class LightingBrightnessEventArgs : HandledEventArgs
    {
        /// <summary>
        /// Gets the X coord of the light.
        /// </summary>
        public int X { get; internal set; }

        /// <summary>
        /// Gets the Y coord of the light.
        /// </summary>
        public int Y { get; internal set; }

        /// <summary>
        /// Gets or sets the brightness of the light.
        /// </summary>
        public float Brightness { get; set; }
    }
}
