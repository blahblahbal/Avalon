using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.HerbsAndSeeds;

public class LargeDeathweed : ModItem
{
	public override string Texture => ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement ? $"Avalon/Items/Placeable/HerbsAndSeeds/{Name}_Alt" : base.Texture;
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 15;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Herbs.LargeHerbsStage4>(), 3);
		Item.width = 10;
		Item.height = 24;
		Item.value = Item.sellPrice(copper: 60);
	}
}
