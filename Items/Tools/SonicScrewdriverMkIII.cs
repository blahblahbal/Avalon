using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Network;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Items.Tools;

class SonicScrewdriverMkIII : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 36;
        Item.rare = ItemRarityID.Cyan;
        Item.useTime = 70;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useStyle = ItemUseStyleID.Thrust;
        Item.useAnimation = 70;
        Item.scale = 0.7f;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SonicScrewdriver");
    }

    public override bool? UseItem(Player player)
    {
        if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
        {
            Point c = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();

            // unlocking avalon locked chests
            if (Main.tile[c.X, c.Y].TileType == ModContent.TileType<Tiles.Furniture.LockedChests>())
            {
                int xpos;
                for (xpos = (int)(Main.tile[c.X, c.Y].TileFrameX / 18); xpos > 1; xpos -= 2)
                {
                }
                xpos = Player.tileTargetX - xpos;
                int ypos = Player.tileTargetY - (Main.tile[c.X, c.Y].TileFrameY / 18);

                Tiles.Furniture.LockedChests.Unlock(xpos, ypos);
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    SyncLockUnlock.SendPacket(1, xpos, ypos);
                    NetMessage.SendTileSquare(-1, xpos, ypos);
                }
            }
            // contagion chest
            else if (Main.tile[c.X, c.Y].TileType == ModContent.TileType<Tiles.Contagion.ContagionChest>())
            {
                int xpos;
                for (xpos = (int)(Main.tile[c.X, c.Y].TileFrameX / 18); xpos > 1; xpos -= 2)
                {
                }
                xpos = Player.tileTargetX - xpos;
                int ypos = Player.tileTargetY - (Main.tile[c.X, c.Y].TileFrameY / 18);

                if (Main.tile[c.X, c.Y].TileFrameX >= 36 && Main.tile[c.X, c.Y].TileFrameX < 72)
                {
                    bool returnflag = false;
                    if (!NPC.downedPlantBoss)
                    {
                        returnflag = true;
                        NPC.downedPlantBoss = true;
                    }
                    Chest.Unlock(xpos, ypos);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(Terraria.ID.MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 1, xpos, ypos);
                    }
                    if (returnflag)
                    {
                        NPC.downedPlantBoss = false;
                    }
                }
                else
                {
                    bool returnflag = false;
                    if (!NPC.downedPlantBoss)
                    {
                        returnflag = true;
                        NPC.downedPlantBoss = true;
                    }
                    AvalonGlobalTile.LockOrUnlock(xpos, ypos);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(Terraria.ID.MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 3, xpos, ypos);
                    }
                    if (returnflag)
                    {
                        NPC.downedPlantBoss = false;
                    }
                }
            }
            // vanilla chests
            else if (Main.tile[c.X, c.Y].TileType == TileID.Containers)
            {
                int xpos;
                for (xpos = (int)(Main.tile[c.X, c.Y].TileFrameX / 18); xpos > 1; xpos -= 2)
                {
                }
                xpos = Player.tileTargetX - xpos;
                int ypos = Player.tileTargetY - (Main.tile[c.X, c.Y].TileFrameY / 18);

                if (Chest.IsLocked(xpos, ypos))
                {
                    bool returnflag = false;
                    if (!NPC.downedPlantBoss)
                    {
                        returnflag = true;
                        NPC.downedPlantBoss = true;
                    }
                    Chest.Unlock(xpos, ypos);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(Terraria.ID.MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 1, xpos, ypos);
                    }
                    if (returnflag)
                    {
                        NPC.downedPlantBoss = false;
                    }
                }
                else
                {
                    Tile tile = Main.tile[xpos, ypos];
                    int style = TileObjectData.GetTileStyle(tile);
                    if (style == 0 || style >= 7 && style <= 16 || style == 48)
                    {
                        Tiles.Furniture.LockedChests.Lock(xpos, ypos);
                        if (Main.netMode != NetmodeID.SinglePlayer)
                        {
                            SyncLockUnlock.SendPacket(0, xpos, ypos);
                            NetMessage.SendTileSquare(-1, xpos, ypos);
                        }
                    }
                    else
                    {
                        bool returnflag = false;
                        if (!NPC.downedPlantBoss)
                        {
                            returnflag = true;
                            NPC.downedPlantBoss = true;
                        }
                        Chest.Lock(xpos, ypos);
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(Terraria.ID.MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 3, xpos, ypos);
                        }
                        if (returnflag)
                        {
                            NPC.downedPlantBoss = false;
                        }
                    }
                }
            }
        }
        return true;
    }

    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ModContent.ItemType<SonicScrewdriverMkII>())
    //        .AddIngredient(ItemID.Emerald, 10)
    //        .AddIngredient(ItemID.Wire, 20)
    //        .AddIngredient(ItemID.SoulofMight, 5)
    //        .AddIngredient(ItemID.SoulofFright, 5)
    //        .AddIngredient(ItemID.SoulofSight, 5)
    //        .AddIngredient(ModContent.ItemType<Material.Onyx>(), 10)
    //        .AddTile(TileID.TinkerersWorkbench).Register();
    //}
    public override void UpdateInventory(Player player)
    {
        player.findTreasure = player.detectCreature = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
        //player.GetModPlayer<AvalonPlayer>().OpenLocks = true;
    }
}
