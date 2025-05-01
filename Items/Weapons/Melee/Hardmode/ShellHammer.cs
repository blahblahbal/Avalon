using Avalon.Common.Extensions;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class ShellHammer : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<Projectiles.Melee.Shell>(), 98, 12f, 7f, 50, 35, width: 56, height: 62);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 6, 20);
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		player.itemLocation = Vector2.Lerp(player.itemLocation, player.MountedCenter, 0.5f);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity.Y -= 3f;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.ChlorophyteBar, 18)
			.AddIngredient(ItemID.TurtleShell)
			.AddIngredient(ModContent.ItemType<VenomShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
