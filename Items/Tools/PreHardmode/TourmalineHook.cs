using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class TourmalineHook : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToGrapplingHook(ModContent.ProjectileType<Projectiles.Tools.TourmalineHook>(), 11f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(silver: 54);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>(), 15)
			.AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.TopazHook)
			.Register();
	}
}
