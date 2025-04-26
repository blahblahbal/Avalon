using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class Sandstormer : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, 15, 30);
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Sandstormer>();
		Item.maxStack = 1;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 70);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup(RecipeGroupID.Sand, 75)
			.AddRecipeGroup("GoldBar", 8)
			.AddIngredient(ItemID.SoulofLight, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
