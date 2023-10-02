using Avalon.Common.Players;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
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

    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        int pid = Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16);
        if (pid >= 0)
        {
            if (Main.player[pid].GetModPlayer<AvalonPlayer>().OreDupe && TileID.Sets.Ore[Main.tile[i, j].TileType])
            {
                if (Data.Sets.Tile.OresToChunks.ContainsKey(Main.tile[i, j].TileType))
                {
                    int drop = Data.Sets.Tile.OresToChunks[Main.tile[i, j].TileType];
                    if (Main.rand.NextBool(3))
                    {
                        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, drop);
                    }
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, drop);
                    noItem = true;
                }
            }
        }
        
        // four leaf clover drops
        if (type is TileID.CorruptPlants or TileID.JunglePlants or TileID.JunglePlants2 or TileID.CrimsonPlants or TileID.Plants or TileID.Plants2 || type == ModContent.TileType<Tiles.Contagion.ContagionShortGrass>())
        {
            if (Main.rand.NextBool(8000))
            {
                int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<FourLeafClover>(), 1, false, 0);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.FromLiteral(""), a, 0f, 0f, 0f, 0);
                    Main.item[a].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[a].position, 8, 8);
                }
            }
            else if (Main.rand.NextBool(500))
            {
                int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<FakeFourLeafClover>(), 1, false, 0);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.FromLiteral(""), a, 0f, 0f, 0f, 0);
                    Main.item[a].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[a].position, 8, 8);
                }
            }
        }
    }
}
