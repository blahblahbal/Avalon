using Avalon;
using Avalon.Common.Extensions;
using Avalon.Dusts;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class SoulEdgePlayer : ModPlayer
{
	public const int maxSoulEdge = 5000;
	public override void ResetEffects()
	{
		if (SoulEdgeDamage > maxSoulEdge)
			SoulEdgeDamage = maxSoulEdge;
	}
	public int SoulEdgeDamage = 0;
}
public class SoulEdgeLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.HeldItem);
	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		return drawInfo.drawPlayer.HeldItem.type == ModContent.ItemType<SoulEdge>() && drawInfo.drawPlayer.ItemAnimationActive;
	}
	private static Asset<Texture2D>? texture;
	public override void Load()
	{
		texture = ModContent.Request<Texture2D>(ModContent.GetInstance<SoulEdge>().Texture + "Glow");
	}
	private void drawSword(ref PlayerDrawSet drawInfo, Color color, int frame)
	{
		Vector2 basePosition = drawInfo.drawPlayer.itemLocation - Main.screenPosition;
		basePosition = new Vector2((int)basePosition.X, (int)basePosition.Y) + (drawInfo.drawPlayer.RotatedRelativePoint(drawInfo.drawPlayer.Center) - drawInfo.drawPlayer.Center);
		Item heldItem = drawInfo.drawPlayer.HeldItem;

		DrawData swingDraw = new DrawData(
		texture.Value, // texture
		basePosition, // position
		new Rectangle(0, texture.Height() / 3 * frame, texture.Width(), texture.Height() / 3), // texture coords
		color, // color (wow really!?)
		drawInfo.drawPlayer.itemRotation,  // rotation
		new Vector2(drawInfo.drawPlayer.direction == -1 ? texture.Value.Width : 0, // origin X
		drawInfo.drawPlayer.gravDir == 1 ? texture.Value.Height / 3 : 0), // origin Y
		drawInfo.drawPlayer.GetAdjustedItemScale(heldItem), // scale
		drawInfo.itemEffect // sprite effects
		);

		drawInfo.DrawDataCache.Add(swingDraw);
	}
	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.shadow != 0 || drawInfo.drawPlayer.ownedProjectileCounts[ModContent.ProjectileType<SoulEdgeSlash>()] == 0)
			return;

		drawSword(ref drawInfo, Color.White, 1);
		drawSword(ref drawInfo, Color.White * (drawInfo.drawPlayer.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage / (float)SoulEdgePlayer.maxSoulEdge), 2);
	}
}
public class SoulEdge : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<SoulEdgeSlash>(), 292, 6.5f, 1f, 20, 20, shootsEveryUse: true, noMelee: true, width: 56, height: 62);
		Item.noUseGraphic = true;
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 30);
		Item.UseSound = null;
	}
	public override bool AltFunctionUse(Player player)
	{
		return player.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage == SoulEdgePlayer.maxSoulEdge;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, position, velocity * 8, ModContent.ProjectileType<SoulEdgeDash>(), damage * 3, knockback * 2, player.whoAmI);
			return false;
		}

		float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem);
		Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale5 * 2f);
		NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
		return false;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}

