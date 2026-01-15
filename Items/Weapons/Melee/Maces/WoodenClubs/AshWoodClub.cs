using Avalon.Projectiles.Melee.Maces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces.WoodenClubs;

public class AshWoodClub : WoodenClub
{
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<AshWoodClubProj>();
		Item.damage = 18;
		Item.useTime = 50;
		Item.useAnimation = 50;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ItemID.AshWood, 20)
			.SortAfterFirstRecipesOf(ItemID.AshWoodBow)
			.Register();
	}
}
