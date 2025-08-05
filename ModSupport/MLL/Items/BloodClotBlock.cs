using Avalon.ModSupport.MLL.Tiles;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class BloodClotBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<BloodClot>());
	}
}
