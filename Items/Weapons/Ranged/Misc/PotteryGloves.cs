using Avalon.Common.Extensions;
using Avalon.Projectiles.Ranged.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Misc;

public class PotteryGloves : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToThrownWeapon(ModContent.ProjectileType<Pot>(), 16, 8, 9f, 32, consumable: false, crit: 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(20)
			.AddIngredient(ItemID.Leather, 6)
			.AddIngredient(ItemID.ClayBlock, 30)
			.AddRecipeGroup("DemoniteBar", 5)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
