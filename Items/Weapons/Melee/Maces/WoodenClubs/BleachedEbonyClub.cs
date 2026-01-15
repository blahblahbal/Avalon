using Avalon.Items.Placeable.Tile;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Projectiles.Melee.Maces;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces.WoodenClubs;

public class BleachedEbonyClub : WoodenClub
{
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<BleachedEbonyClubProj>();
		Item.damage = 13;
		Item.knockBack = 7.4f;
		Item.useTime = 53;
		Item.useAnimation = 53;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ModContent.ItemType<BleachedEbony>(), 20)
			.SortAfterFirstRecipesOf(ModContent.ItemType<BleachedEbonyHammer>())
			.Register();
	}
}