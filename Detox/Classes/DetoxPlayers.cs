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
    using System.Collections.Generic;
    using System.Linq;

    public static class DetoxPlayers
    {
        /// <summary>
        /// Obtains the player with the matching name; if any.
        /// Case sensitive!
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DetoxObject GetPlayer(string name)
        {
            return DetoxPlayers.Players.SingleOrDefault(p => p != null && p["name"] == name);
        }

        /// <summary>
        /// Obtains the player at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DetoxObject GetPlayer(int index)
        {
            return DetoxPlayers.Players[index];
        }

        /// <summary>
        /// Gets the current list of players.
        /// </summary>
        public static List<DetoxObject> Players
        {
            get
            {
                var players = Terraria.GetMainField<dynamic[]>("player");
                return players.Select(p => new DetoxObject(p, "Player")).ToList();
            }
        }

        /// <summary>
        /// Gets the local index of the current player.
        /// </summary>
        public static int LocalPlayerIndex
        {
            get { return Terraria.GetMainField<int>("myPlayer"); }
        }

        /// <summary>
        /// Gets the current players player object.
        /// </summary>
        public static DetoxObject LocalPlayer
        {
            get
            {
                var players = Terraria.GetMainField<dynamic[]>("player");
                return new DetoxObject(players.FirstOrDefault(p => p.whoAmi == DetoxPlayers.LocalPlayerIndex), "Player");
            }
        }
    }
}
