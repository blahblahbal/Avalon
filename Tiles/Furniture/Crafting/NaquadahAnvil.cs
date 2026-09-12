using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.Crafting;

public class NaquadahAnvil : ModTile
{
	public override void SetStaticDefaults()
	{
		AddMapEntry(new Color(66, 66, 255), this.GetLocalization("MapEntry"));
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
		TileObjectData.newTile.CoordinateHeights = [18, 18];
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(Type);
		Main.tileObsidianKill[Type] = true;
		Main.tileSolidTop[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.placementPreview = true;
		DustType = ModContent.DustType<Dusts.NaquadahDust>();
		AdjTiles = [TileID.Anvils, TileID.MythrilAnvil];
	}
}
