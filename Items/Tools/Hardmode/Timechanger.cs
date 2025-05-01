using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class Timechanger : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, 15, 30);
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Timechanger>();
		Item.maxStack = 1;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 70);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup("GoldBar", 30)
			.AddIngredient(ItemID.SoulofLight, 15)
			.AddIngredient(ItemID.SoulofNight, 15)
			.AddRecipeGroup("GoldWatch")
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void UpdateInventory(Player player)
	{
		player.accWatch = 3;
	}
}
