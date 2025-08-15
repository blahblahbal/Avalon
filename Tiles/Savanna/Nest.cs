using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Savanna;

public class Nest : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(198, 175, 132));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        DustType = DustID.MarblePot;
        TileID.Sets.DoesntGetReplacedWithTileReplacement[Type] = true;
        TileID.Sets.JungleSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
    }
	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		if (!fail && !effectOnly)
		{
			if (Main.tile[i, j - 1].TileType == ModContent.TileType<NestStalac>())
			{
				WorldGen.KillTile(i, j - 1);
				if (Main.tile[i, j - 2].TileType == ModContent.TileType<NestStalac>())
				{
					WorldGen.KillTile(i, j - 2);
				}
			}
			if (Main.tile[i, j + 1].TileType == ModContent.TileType<NestStalac>())
			{
				WorldGen.KillTile(i, j + 1);
				if (Main.tile[i, j + 2].TileType == ModContent.TileType<NestStalac>())
				{
					WorldGen.KillTile(i, j + 2);
				}
			}
		}
	}
}
