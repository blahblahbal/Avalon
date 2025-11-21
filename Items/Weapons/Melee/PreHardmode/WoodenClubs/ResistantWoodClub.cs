using Avalon.Items.Placeable.Tile;
using Avalon.Items.Tools.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.WoodenClubs;

public class ResistantWoodClub : WoodenClub
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();

		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<ResistantWoodClubProj>();
		Item.damage = 20;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ModContent.ItemType<ResistantWood>(), 20)
			.SortAfterFirstRecipesOf(ModContent.ItemType<ResistantWoodHammer>())
			.Register();
	}
}
public class ResistantWoodClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<ResistantWoodClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<ResistantWoodClub>().DisplayName;
}
