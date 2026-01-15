using Avalon.Projectiles.Melee.Maces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces.WoodenClubs;

public class BorealWoodClub : WoodenClub
{
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<BorealWoodClubProj>();
		Item.damage = 13;
		Item.knockBack = 7.4f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ItemID.BorealWood, 20)
			.SortAfterFirstRecipesOf(ItemID.BorealWoodBow)
			.Register();
	}
}
