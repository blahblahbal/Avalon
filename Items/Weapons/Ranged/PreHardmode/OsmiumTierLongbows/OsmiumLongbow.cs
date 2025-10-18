using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.OsmiumTierLongbows;

public class OsmiumLongbow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToLongbow(64, 2.3f, 24f, 84);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<OsmiumLongbowHeld>(), damage, knockback, player.whoAmI, type);
		return false;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<OsmiumBar>(), 13)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
