using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonGlobalTile : GlobalTile
{
    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)].GetModPlayer<AvalonPlayer>().OreDupe && TileID.Sets.Ore[Main.tile[i, j].TileType])
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
}
