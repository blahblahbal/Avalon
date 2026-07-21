using Avalon.Projectiles.Ranged.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Misc;

public class StasisRifle : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.width = 14;
		Item.height = 32;
		Item.scale = 1f;
		Item.shootSpeed = 14f;
		Item.DamageType = DamageClass.Ranged;
		Item.noMelee = true;
		Item.knockBack = 2.3f;
		Item.shoot = ModContent.ProjectileType<StasisRifleHeld>();
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 7, 0);
		Item.damage = 200;
		Item.useAnimation = 84;
		Item.useTime = 84;
		Item.reuseDelay = 40;
		Item.channel = true;
		Item.noUseGraphic = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddRecipeGroup("AdamantiteBar", 20)
			.AddIngredient(ItemID.FrostCore, 2)
			.AddIngredient(ItemID.Shotgun)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}