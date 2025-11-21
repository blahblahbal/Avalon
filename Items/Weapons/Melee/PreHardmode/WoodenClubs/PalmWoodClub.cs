using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.WoodenClubs;

public class PalmWoodClub : WoodenClub
{
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<PalmWoodClubProj>();
		Item.damage = 13;
		Item.knockBack = 7.4f;
		Item.useTime = 53;
		Item.useAnimation = 53;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ItemID.PalmWood, 20)
			.SortAfterFirstRecipesOf(ItemID.PalmWoodBow)
			.Register();
	}
}
public class PalmWoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<PalmWoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<PalmWoodClub>().DisplayName;
}
