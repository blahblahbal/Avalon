using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Avalon.Projectiles.Ranged.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Thrown;

public class EnchantedShuriken : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToThrownWeapon(ModContent.ProjectileType<EnchantedShurikenProj>(), 15, 0f, 10.5f, 16, false, false);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 1, 50);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type, 1).AddIngredient(ModContent.ItemType<EnchantedBar>(), 5).AddTile(TileID.Anvils).Register();
	}
}