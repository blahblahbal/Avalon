using Avalon.Items.Placeable.Tile;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Projectiles.Melee.Maces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces.WoodenClubs;

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
