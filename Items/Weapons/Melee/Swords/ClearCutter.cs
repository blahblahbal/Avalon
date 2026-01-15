using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class ClearCutter : ModItem
{
	public override bool MeleePrefix()
	{
		return true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<ClearCutterSlash>(), 90, 5f, 9.5f, 28, 28, true, true, true);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 9, 63);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem);
		Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax * 0.9f, adjustedItemScale5);
		NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
		return false;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.CrystalShard, 35)
			.AddIngredient(ItemID.SoulofLight, 5)
			.AddIngredient(ItemID.PearlstoneBlock, 50)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}