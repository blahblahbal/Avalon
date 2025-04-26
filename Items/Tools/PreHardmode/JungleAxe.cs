using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class JungleAxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAxe(70, 12, 4f, 23, 23);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 10);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.JungleSpores, 12)
			.AddIngredient(ItemID.Stinger, 4)
			.AddIngredient(ItemID.Vine)
			.AddIngredient(ModContent.ItemType<Material.Shards.ToxinShard>())
			.AddTile(TileID.Anvils)
			.SortBeforeFirstRecipesOf(ItemID.BladeofGrass)
			.Register();
	}
}
