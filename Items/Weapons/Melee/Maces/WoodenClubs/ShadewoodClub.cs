using Avalon.Items.Armor.PreHardmode;
using Avalon.Projectiles.Melee.Maces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces.WoodenClubs;

public class ShadewoodClub : WoodenClub
{
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<ShadewoodClubProj>();
		Item.damage = 16;
		Item.knockBack = 7.4f;
		Item.useTime = 53;
		Item.useAnimation = 53;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ItemID.Shadewood, 20)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<CoughwoodHelmet>())
			.Register();
	}
}
