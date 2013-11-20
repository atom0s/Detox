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
    public enum PacketTypes
    {
        /// <summary>
        /// Packet used for requesting a connection to a server.
        /// 
        /// Client -> Server
        /// </summary>
        ConnectionRequest = 1,

        /// <summary>
        /// Packet used for disconnecting from a server.
        /// 
        /// Server -> Client
        /// </summary>
        Disconnect = 2,

        /// <summary>
        /// Packet used to continue connecting to a server.
        /// 
        /// Server -> Client
        /// </summary>
        ContinueConnecting = 3,

        /// <summary>
        /// Packet used for sending and receiving player information.
        /// 
        /// Client + Server
        /// </summary>
        PlayerInfo = 4,

        /// <summary>
        /// Packet used for sending and receiving inventory information.
        /// 
        /// Client + Server
        /// </summary>
        PlayerInventorySlot = 5,

        /// <summary>
        /// Packet used to continue connecting to a server.
        /// 
        /// Client -> Server
        /// </summary>
        ContinueConnecting2 = 6,

        /// <summary>
        /// Packet used for world information.
        /// 
        /// Server -> Client
        /// </summary>
        WorldInfo = 7,

        /// <summary>
        /// Packet used for querying section information.
        /// 
        /// Client -> Server
        /// </summary>
        GetSection = 8,

        /// <summary>
        /// Packet used for section status information.
        /// 
        /// Server -> Client
        /// </summary>
        Status = 9,

        /// <summary>
        /// Packet used for obtaining section information.
        /// 
        /// Server -> Client
        /// </summary>
        SendSection = 10,

        /// <summary>
        /// Packet used for obtaining section frame information.
        /// 
        /// Server -> Client
        /// </summary>
        SendSectionFrame = 11,

        /// <summary>
        /// Packet used for spawning a client.
        /// 
        /// Client -> Server
        /// </summary>
        SpawnPlayer = 12,

        /// <summary>
        /// Packet used for updating player information.
        /// 
        /// Client + Server
        /// </summary>
        UpdatePlayer = 13,

        /// <summary>
        /// Packet used for setting a player as active.
        /// 
        /// Server -> Client
        /// </summary>
        SetActivePlayer = 14,

        /// <summary>
        /// Packet used for syncing player data.
        /// 
        /// Server -> Client
        /// </summary>
        SyncPlayers = 15,

        /// <summary>
        /// Packet used for setting a players health.
        /// 
        /// Client + Server
        /// </summary>
        SetPlayerHp = 16,

        /// <summary>
        /// Packet used to modify a tile.
        /// 
        /// Client + Server
        /// </summary>
        ModifyTile = 17,

        /// <summary>
        /// Packet used to set the world time.
        /// 
        /// Server -> Client
        /// </summary>
        SetTime = 18,

        /// <summary>
        /// Packet used when interacting with a door.
        /// 
        /// Client + Server
        /// </summary>
        ToggleDoor = 19,

        /// <summary>
        /// Packet used when sending a tile square.
        /// 
        /// Client + Server
        /// </summary>
        SendTileSquare = 20,

        /// <summary>
        /// Packet used to update an item within the world.
        /// 
        /// Client + Server
        /// </summary>
        UpdateItem = 21,

        /// <summary>
        /// Packet used to update an items owner in the world.
        /// 
        /// Client + Server
        /// </summary>
        SetItemOwner = 22,

        /// <summary>
        /// Packet used to update an npcs information.
        /// 
        /// Server -> Client
        /// </summary>
        UpdateNpc = 23,

        /// <summary>
        /// Packet used for striking an npc with the current held item.
        /// 
        /// Client + Server
        /// </summary>
        StrikeNpcHeldItem = 24,

        /// <summary>
        /// Packet used for chat messages.
        /// 
        /// Client + Server
        /// </summary>
        Chat = 25,

        /// <summary>
        /// Packet used for damaging a player.
        /// 
        /// Client + Server
        /// </summary>
        DamagePlayer = 26,

        /// <summary>
        /// Packet used for updating a projectile.
        /// 
        /// Client + Server
        /// </summary>
        UpdateProjectile = 27,

        /// <summary>
        /// Packet used for damaging an npc.
        /// 
        /// Client + Server
        /// </summary>
        StrikeNpc = 28,

        /// <summary>
        /// Packet used for destroying a projectile.
        /// 
        /// Client + Server
        /// </summary>
        DestroyProjectile = 29,

        /// <summary>
        /// Packet used for setting a players pvp status.
        /// 
        /// Client + Server
        /// </summary>
        SetPlayerPvp = 30,

        /// <summary>
        /// Packet used for obtaing a chests contents.
        /// 
        /// Client -> Server
        /// </summary>
        GetChestContents = 31,

        /// <summary>
        /// Packet used for chest items.
        /// 
        /// Client + Server
        /// </summary>
        ChestItem = 32,

        /// <summary>
        /// Packet used for setting the active chest.
        /// 
        /// Client + Server
        /// </summary>
        SetCurrentChest = 33,

        /// <summary>
        /// Packet used for destroying a chest.
        /// 
        /// Client -> Server
        /// </summary>
        DestroyChest = 34,

        /// <summary>
        /// Packet used for sending a player heal effect.
        /// 
        /// Client + Server
        /// </summary>
        PlayerHealEffect = 35,

        /// <summary>
        /// Packet used for setting a players zone.
        /// 
        /// Client + Server
        /// </summary>
        SetPlayerZone = 36,

        /// <summary>
        /// Packet used while connecting to request the server password.
        /// 
        /// Server -> Client
        /// </summary>
        RequestPassword = 37,

        /// <summary>
        /// Packet used to send the password to the server.
        /// 
        /// Client -> Server
        /// </summary>
        SendPassword = 38,

        /// <summary>
        /// Packet used to remove ownership of an item.
        /// 
        /// Server -> Client
        /// </summary>
        RemoveItemOwner = 39,

        /// <summary>
        /// Packet used for setting the players active npc.
        /// 
        /// Client + Server
        /// </summary>
        SetActivekNpc = 40,

        /// <summary>
        /// Packet used for animating the players item.
        /// 
        /// Client + Server
        /// </summary>
        AnimatePlayerItem = 41,

        /// <summary>
        /// Packet used for updating a players mana.
        /// 
        /// Client + Server
        /// </summary>
        SetPlayerMana = 42,

        /// <summary>
        /// Packet used for sending a player mana effect.
        /// 
        /// Client + Server
        /// </summary>
        PlayerManaEffect = 43,

        /// <summary>
        /// Packet used for killing a player.
        /// 
        /// Client + Server
        /// </summary>
        KillPlayer = 44,

        /// <summary>
        /// Packet used for setting a players team.
        /// 
        /// Client + Server
        /// </summary>
        SetPlayerTeam = 45,

        /// <summary>
        /// Packet used for requesting a sign.
        /// 
        /// Client -> Server
        /// </summary>
        RequestSign = 46,

        /// <summary>
        /// Packet used for updating a signs text.
        /// 
        /// Client + Server
        /// </summary>
        UpdateSign = 47,

        /// <summary>
        /// Packet used for modifying liquid.
        /// 
        /// Client + Server
        /// </summary>
        ModifyLiquid = 48,

        /// <summary>
        /// Packet used for a completed connection to spawn a player.
        /// 
        /// Server -> Client
        /// </summary>
        ConnectionCompleteSpawn = 49,

        /// <summary>
        /// Packet used for updating a players buffs.
        /// 
        /// Client + Server
        /// </summary>
        UpdatePlayerBuffs = 50,

        /// <summary>
        /// Packet used for special npc effects.
        /// 
        /// Client + Server
        /// </summary>
        SpecialNpcEffect = 51,

        #region "Added in Terraria 1.0.6"
        /// <summary>
        /// Packet used for unlocking objects.
        /// 
        /// Client + Server
        /// </summary>
        UnlockObject = 52,

        /// <summary>
        /// Packet used for adding a buff to an npc.
        /// 
        /// Client + Server
        /// </summary>
        AddNpcBuff = 53,

        /// <summary>
        /// Packet used for updating an npcs buff.
        /// 
        /// Server -> Client
        /// </summary>
        UpdateNpcBuffs = 54,

        /// <summary>
        /// Packet used for adding a buff to a player.
        /// 
        /// Client + Server
        /// </summary>
        AddPlayerBuff = 55,
        #endregion

        #region "Added in Terraria 1.1"
        /// <summary>
        /// Packet used for setting an npcs name.
        /// 
        /// Server -> Client
        /// </summary>
        SetNpcName = 56,

        /// <summary>
        /// Packet used for setting the worlds good/evil status.
        /// 
        /// Server -> Client
        /// </summary>
        SetGoodEvilPercent = 57,

        /// <summary>
        /// Packet used for playing specific music.
        /// 
        /// Client + Server
        /// </summary>
        PlayMusic = 58,

        /// <summary>
        /// Packet used for activating a switch.
        /// 
        /// Client + Server
        /// </summary>
        ActivateSwitch = 59,

        /// <summary>
        /// Packet used for setting an npcs home.
        /// 
        /// Client + Server
        /// </summary>
        SetNpcHome = 60,
        #endregion

        #region "Added in Terraria 1.1.2"
        /// <summary>
        /// Packet used for spawning a boss or invasion.
        /// 
        /// Client + Server
        /// </summary>
        SpawnBossInvasion = 61,
        #endregion

        #region "Added in Terraria 1.2.x"
        /// <summary>
        /// Packet used for a player dodging.
        /// 
        /// Client + Server
        /// </summary>
        PlayerDodge = 62,

        /// <summary>
        /// Packet used for painting a tile.
        /// 
        /// Client + Server
        /// </summary>
        PaintTile = 63,

        /// <summary>
        /// Packet used for painting a wall.
        /// 
        /// Client + Server
        /// </summary>
        PaintWall = 64,

        /// <summary>
        /// Packet used for teleporting a player or npc.
        /// 
        /// Client + Server
        /// </summary>
        Teleport = 65,

        /// <summary>
        /// Packet used to heal a player.
        /// 
        /// Client + Server
        /// </summary>
        HealPlayer = 66,

        /// <summary>
        /// Packet used for sending the client UUID to the server.
        /// 
        /// Client -> Server
        /// </summary>
        Uuid = 68
        #endregion
    }
}
