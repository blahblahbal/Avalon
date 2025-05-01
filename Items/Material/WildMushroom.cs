using Avalon.Common.Extensions;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class WildMushroom : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(24, 24);
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ItemID.Mushroom)
			.AddIngredient(ItemID.GlowingMushroom)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
