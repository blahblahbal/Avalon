using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.WoodenClubs;

public class EbonwoodClub : WoodenClub
{
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<EbonwoodClubProj>();
		Item.damage = 16;
		Item.knockBack = 7.4f;
		Item.useTime = 53;
		Item.useAnimation = 53;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ItemID.Ebonwood, 20)
			.SortAfterFirstRecipesOf(ItemID.EbonwoodBow)
			.Register();
	}
}
public class EbonwoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<EbonwoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<EbonwoodClub>().DisplayName;
}
