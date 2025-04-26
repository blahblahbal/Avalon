using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class ZirconHook : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToGrapplingHook(ModContent.ProjectileType<Projectiles.Tools.ZirconHook>(), 14f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 54);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Ores.Zircon>(), 15)
			.AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.DiamondHook)
			.Register();
	}
}
