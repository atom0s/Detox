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

    public static class DetoxNPCs
    {
        /// <summary>
        /// Obtains a field value from inside the Terraria.NPC type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetField<T>(string name)
        {
            var npc = Detox.Terraria.GetType("Terraria.NPC");
            if (npc == null) return default(T);

            var field = npc.GetField(name, Detox.BindFlags);
            return (field != null) ? (T)field.GetValue(null) : default(T);
        }

        /// <summary>
        /// Sets a field value inside the Terraria.NPC type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetField<T>(string name, T value)
        {
            var npc = Detox.Terraria.GetType("Terraria.NPC");
            if (npc == null) return;

            var field = npc.GetField(name, Detox.BindFlags);
            if (field == null) return;

            field.SetValue(null, value);
        }

        /// <summary>
        /// Obtains the npc with the matching name; if any.
        /// Case sensitive!
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DetoxObject GetNPC(string name)
        {
            return DetoxNPCs.NPCs.SingleOrDefault(n => n != null && n["name"] == name);
        }

        /// <summary>
        /// Obtains the npc at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DetoxObject GetNPC(int index)
        {
            return DetoxNPCs.NPCs[index];
        }

        /// <summary>
        /// Gets the current list of npcs.
        /// </summary>
        public static List<DetoxObject> NPCs
        {
            get { return Terraria.GetMainField<dynamic[]>("npc").Select(n => new DetoxObject(n, "NPC")).ToList(); }
        }
    }
}
