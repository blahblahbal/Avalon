using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.WoodenClubs;

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
public class AshWoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<AshWoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<AshWoodClub>().DisplayName;
}
