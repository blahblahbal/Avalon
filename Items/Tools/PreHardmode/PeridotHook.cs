using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class PeridotHook : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToGrapplingHook(ModContent.ProjectileType<Projectiles.Tools.PeridotHook>(), 12f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 54);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Ores.Peridot>(), 15)
			.AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.EmeraldHook)
			.Register();
	}
}
