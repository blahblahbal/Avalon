using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class TourmalineStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<Projectiles.Magic.TourmalineBolt>(), 18, 3.5f, 5, 6.5f, 38, 38, true);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 7);
		Item.UseSound = SoundID.Item43;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 10)
			.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
