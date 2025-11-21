using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.WoodenClubs;

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
public class BorealWoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<BorealWoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<BorealWoodClub>().DisplayName;
}
