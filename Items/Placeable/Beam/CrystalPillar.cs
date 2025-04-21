using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Beam;

public class CrystalPillar : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 50;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CrystalMines.CrystalColumn>());
	}
}
