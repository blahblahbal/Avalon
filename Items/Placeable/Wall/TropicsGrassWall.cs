using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class TropicsGrassWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.TropicalGrassWallSafe>());
	}
}
