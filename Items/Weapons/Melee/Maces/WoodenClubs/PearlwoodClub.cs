using Avalon.Items.Armor.PreHardmode;
using Avalon.Projectiles.Melee.Maces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces.WoodenClubs;

public class PearlwoodClub : WoodenClub
{
	public override void SetDefaults()
	{
		base.SetDefaults();

		Item.shoot = ModContent.ProjectileType<PearlwoodClubProj>();
		Item.damage = 45;
		Item.knockBack = 8.4f;
		Item.useTime = 47;
		Item.useAnimation = 47;
		Item.autoReuse = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ItemID.Pearlwood, 20)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<ResistantWoodHelmet>())
			.Register();
	}
}
