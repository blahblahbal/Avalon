using Avalon.Common.Players;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonGlobalTile : GlobalTile
{
    public override void SetStaticDefaults()
    {
        int[] spelunkers = { TileID.Crimtane, TileID.Meteorite, TileID.Obsidian, TileID.Hellstone };
        int[] ores = { TileID.Topaz, TileID.Ruby, TileID.Amethyst, TileID.Diamond, TileID.Emerald, TileID.Sapphire, TileID.AmberStoneBlock };
        foreach (int tile in spelunkers)
        {
            Main.tileSpelunker[tile] = true;
        }
        foreach(int tile in ores)
        {
            TileID.Sets.Ore[tile] = true;
        }
    }
    public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        if (Main.tile[i, j].TileType == TileID.LihzahrdAltar && NPC.downedGolemBoss)
        {
            Main.tileFrameCounter[TileID.LihzahrdAltar]++;

            int frameX = Main.tile[i, j].TileFrameX;
            int frameY = Main.tile[i, j].TileFrameY;

            if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 0 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 8)
            {
                frameY += 0;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 8 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 16)
            {
                frameY += 36;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 16 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 24)
            {
                frameY += 36 * 2;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 24 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 32)
            {
                frameY += 36 * 3;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 32 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 40)
            {
                frameY += 36 * 4;
            }
            else if (Main.tileFrameCounter[TileID.LihzahrdAltar] >= 40 && Main.tileFrameCounter[TileID.LihzahrdAltar] < 48)
            {
                frameY += 36 * 5;
            }
            //if (Main.tileFrameCounter[TileID.LihzahrdAltar] % 6 == 0)
            //{
            //    //Main.NewText(Main.tileFrameCounter[TileID.LihzahrdAltar]);
            //    frameY = (Main.tile[i, j].TileFrameY + 1) * Main.tileFrameCounter[TileID.LihzahrdAltar];
            //    //Main.NewText(frameY);
            //}
            if (Main.tileFrameCounter[TileID.LihzahrdAltar] == 48)
            {
                Main.tileFrameCounter[TileID.LihzahrdAltar] = 0;
            }
            var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Assets/Textures/LihzahrdAltarPortal").Value,
                new Vector2(i * 16 - (int)Main.screenPosition.X - 0 / 2f, j * 16 - (int)Main.screenPosition.Y) + zero,
                new Rectangle(frameX, frameY, 16, 16), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            //Main.NewText(frameY);
        }

        //if (Main.tile[i, j - 1].TileType == TileID.LihzahrdAltar)
        //{
        //    if (Main.tileSolid[Main.tile[i, j].TileType])
        //    {
        //        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        //        if (Main.drawToScreen)
        //        {
        //            zero = Vector2.Zero;
        //        }

        //        Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Assets/Textures/BlueLihzahrdGlowmask").Value,
        //            new Vector2(i * 16 - (int)Main.screenPosition.X - 0 / 2f, j * 16 - (int)Main.screenPosition.Y) + zero,
        //            new Rectangle(0, 0, 48, 16), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //    }




        //    //Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Assets/Textures/BlueLihzahrdGlowmask").Value,
        //    //    new Vector2(i * 16 + (int)Main.screenPosition.X / 2f, j * 16 + (int)Main.screenPosition.Y), Color.Cyan);
        //    //Main.NewText(i);

        //}
    }
    public override void FloorVisuals(int type, Player player)
    {
        if (type == 229 && player.GetModPlayer<AvalonPlayer>().NoSticky)
        {
            player.sticky = false;
        }
    }
    public override void Drop(int i, int j, int type)
    {
        int pid = Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16);
        // four leaf clover drops
        if (type is TileID.CorruptPlants or TileID.JunglePlants or TileID.JunglePlants2 or TileID.CrimsonPlants or TileID.Plants or TileID.Plants2 ||
            type == ModContent.TileType<Tiles.Contagion.ContagionShortGrass>())
        {
            bool doRealCloverDrop = false;
            bool doFakeCloverDrop = false;
            int realChance = 8000;
            int fakeChance = 500;
            if (pid >= 0)
            {
                Player p = Main.player[pid];
                if (p.RollLuck(realChance) < 1)
                {
                    doRealCloverDrop = true;
                }
                else if (p.RollLuck(fakeChance) < 1)
                {
                    doFakeCloverDrop = true;
                }
            }
            else
            {
                doRealCloverDrop = Main.rand.NextBool(realChance);
                doFakeCloverDrop = Main.rand.NextBool(fakeChance);
            }

            if (doRealCloverDrop)
            {
                int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<FourLeafClover>(), 1, false, 0);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a, 0f, 0f, 0f, 0);
                    Main.item[a].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[a].position, 8, 8);
                }
            }
            else if (doFakeCloverDrop)
            {
                int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<FakeFourLeafClover>(), 1, false, 0);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a, 0f, 0f, 0f, 0);
                    Main.item[a].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[a].position, 8, 8);
                }
            }
        }
    }
    public override bool CanPlace(int i, int j, int type)
    {
        Main.NewText(type);
        Console.WriteLine(type);
        if (Data.Sets.Tile.AvalonPlanterBoxes[Main.tile[i, j + 1].TileType] &&
            (Main.tile[i, j].TileType == TileID.ImmatureHerbs || Main.tile[i, j].TileType == TileID.MatureHerbs ||
            Main.tile[i, j].TileType == TileID.BloomingHerbs || Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Barfbush>() ||
            Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Bloodberry>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Holybird>() ||
            Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.Sweetstem>() || Main.tile[i, j].TileType == ModContent.TileType<Tiles.Herbs.TwilightPlume>()))
        {
            return false;
        }
        return base.CanPlace(i, j, type);
    }
    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        int pid = Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16);
        if (pid >= 0)
        {
            if (TileID.Sets.Ore[Main.tile[i, j].TileType])
            {
                if (Main.player[pid].GetModPlayer<AvalonPlayer>().OreDupe)
                {
                    if (Data.Sets.Tile.OresToChunks.ContainsKey(Main.tile[i, j].TileType))
                    {
                        int drop = Data.Sets.Tile.OresToChunks[Main.tile[i, j].TileType];
                        int stack = 1;
                        if (Main.rand.NextBool(3))
                        {
                            stack = 2;
                        }
                        int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, drop, stack);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a, 0f, 0f, 0f, 0);
                        }
                        noItem = true;
                    }
                }
            }
            // Probably doesn't work in multiplayer
            if (type == ModContent.TileType<Tiles.UltraResistantWood>() && Main.player[pid].inventory[Main.player[pid].selectedItem].axe < 40)
            {
                fail = true;
            }
        }
    }
}
