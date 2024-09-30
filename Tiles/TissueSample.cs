using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class TissueSample : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(255, 125, 129));
        Main.tileSolid[Type] = true;
		//Main.tileBlockLight[Type] = true;
		TileID.Sets.GemsparkFramingTypes[Type] = Type;
		RegisterItemDrop(ItemID.TissueSample);
        DustType = DustID.HeartCrystal;
	}
	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
		return false;
	}
}
