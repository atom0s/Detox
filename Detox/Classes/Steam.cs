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
    using System.Runtime.InteropServices;

    public static class Steam
    {
        /// <summary>
        /// Initializes the Steam API and informs Steam 
        /// we are playing Terraria.
        /// </summary>
        /// <returns></returns>
        public static bool Initialize()
        {
            // Set the SteamAppId to Terraria..
            Environment.SetEnvironmentVariable("SteamAppId", "105600");
            return NativeMethods.SteamAPI_Init();
        }

        /// <summary>
        /// Shuts down the Steam API.
        /// </summary>
        public static void Shutdown()
        {
            NativeMethods.SteamAPI_Shutdown();
        }
    }

    internal class NativeMethods
    {
        /// <summary>
        /// steam_api.SteamAPI_Init
        /// </summary>
        /// <returns></returns>
        [DllImport("steam_api.dll")]
        public static extern bool SteamAPI_Init();

        /// <summary>
        /// steam_api.SteamAPI_Shutdown
        /// </summary>
        /// <returns></returns>
        [DllImport("steam_api.dll")]
        public static extern bool SteamAPI_Shutdown();
    }
}
