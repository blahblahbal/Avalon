using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class JunglePickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(56, 7, 3f, 16, 20);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 36);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Stinger, 10)
			.AddIngredient(ItemID.JungleSpores, 12)
			.AddIngredient(ModContent.ItemType<Material.Shards.ToxinShard>())
			.AddTile(TileID.Anvils)
			.SortBeforeFirstRecipesOf(ItemID.BladeofGrass)
			.Register();
	}
}
