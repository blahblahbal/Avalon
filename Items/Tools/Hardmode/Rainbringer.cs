using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class Rainbringer : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, 15, 30);
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Rainbringer>();
		Item.maxStack = 1;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 70);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.RainCloud, 50)
			.AddRecipeGroup("CopperBar", 10)
			.AddIngredient(ItemID.SoulofNight, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
