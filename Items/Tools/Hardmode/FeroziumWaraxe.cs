using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Shards;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class FeroziumWaraxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAxe(120, 30, 8f, 20, 35, 1, width: 40, height: 48);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 7);

	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.AdamantiteBar, 12)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.TitaniumBar, 12)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<TroxiniumBar>(), 12)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
