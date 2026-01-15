using Avalon.Items.Armor.PreHardmode;
using Avalon.Projectiles.Melee.Maces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces.WoodenClubs;

public class RichMahoganyClub : WoodenClub
{
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<RichMahoganyClubProj>();
		Item.damage = 13;
		Item.knockBack = 7.4f;
		Item.useTime = 53;
		Item.useAnimation = 53;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ItemID.RichMahogany, 20)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<BleachedEbonyHelmet>())
			.Register();
	}
}
