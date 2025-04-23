using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile.Ancient;

public class AncientAdamantiteBrick : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ancient.AncientAdamantiteBrick>());
	}
}
