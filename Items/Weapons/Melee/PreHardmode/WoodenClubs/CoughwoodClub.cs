using Avalon.Items.Tools.PreHardmode;
using Avalon.Tiles.Contagion.Coughwood;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.WoodenClubs;

public class CoughwoodClub : WoodenClub
{
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<CoughwoodClubProj>();
		Item.damage = 16;
		Item.knockBack = 7.4f;
		Item.useTime = 53;
		Item.useAnimation = 53;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ModContent.ItemType<Coughwood>(), 20)
			.SortAfterFirstRecipesOf(ModContent.ItemType<CoughwoodHammer>())
			.Register();
	}
}
public class CoughwoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<CoughwoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<CoughwoodClub>().DisplayName;
}
