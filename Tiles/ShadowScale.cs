using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class ShadowScale : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(102, 78, 123));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
		TileID.Sets.GemsparkFramingTypes[Type] = Type;
		RegisterItemDrop(ItemID.ShadowScale);
        DustType = DustID.CorruptPlants;
	}
	public override bool Slope(int i, int j)
	{
		return false;
	}
	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
		return false;
	}
}
