using Avalon.Items.Armor.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.WoodenClubs;

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
public class RichMahoganyClubProj : WoodenClubProj
{
	public override string Texture => ModContent.GetInstance<RichMahoganyClub>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<RichMahoganyClub>().DisplayName;
}
