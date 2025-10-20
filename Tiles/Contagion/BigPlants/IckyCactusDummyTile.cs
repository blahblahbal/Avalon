using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.BigPlants;

public class IckyCactusDummyTile : ModTile
{
	public override string Texture => "Avalon/Tiles/Contagion/BigPlants/IckyCactus";

	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileNoFail[Type] = true;
		AddMapEntry(new Color(134, 95, 59));
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return false;
	}
}
