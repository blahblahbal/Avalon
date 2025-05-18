using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

public class PointingLaser : ModProjectile
{
	private static Asset<Texture2D>? LaserEnd;
	public override void SetStaticDefaults()
	{
		LaserEnd = ModContent.Request<Texture2D>(Texture + "End");

		ProjectileID.Sets.CanDistortWater[Type] = false;
		ProjectileID.Sets.NoLiquidDistortion[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.MaxUpdates = 1;
		Projectile.extraUpdates = 1;
		Projectile.alpha = 255;
		Projectile.damage = 0;
		Projectile.penetrate = -1;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		// todo: fix this using the player's velocity as an offset while at the borders of the map, causing desync from cursor position
		Player player = Main.player[Projectile.owner];
		Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);
		//float distToPlayer = playerCenter.DistanceSQ(player.GetModPlayer<AvalonPlayer>().MousePosition);
		//float playerVelMod = (1f - Utils.Remap(distToPlayer / 2116f, 0.4f, 1f, 0f, 1f));
		//Vector2 newPlayerVel = player.velocity * playerVelMod;
		Vector2 mousePosClamped = Vector2.Clamp(player.GetModPlayer<AvalonPlayer>().MousePosition, Vector2.Zero, new Vector2(Main.maxTilesX, Main.maxTilesY) * 16f);
		float length = playerCenter.Distance(mousePosClamped);
		float rotation = playerCenter.SafeDirectionTo(mousePosClamped).ToRotation() + MathHelper.PiOver2;
		// Draw laser
		Main.spriteBatch.Draw
		(
			TextureAssets.Projectile[Type].Value,
			playerCenter + (playerCenter.SafeDirectionTo(mousePosClamped) * 41f) - Main.screenPosition,
			new Rectangle(0, 0, TextureAssets.Projectile[Type].Value.Width, TextureAssets.Projectile[Type].Value.Height),
			Items.Material.PointingLaser.TeamColor(player),
			rotation,
			new Vector2(TextureAssets.Projectile[Type].Value.Width / 2f, TextureAssets.Projectile[Type].Value.Height),
			new Vector2(2f, length - 48f),
			SpriteEffects.None,
			1f
		);
		// Draw laser end
		float scale = 2f;
		float maxLength = 46f;
		if (length < maxLength)
		{
			scale *= length / maxLength;
			length = maxLength;
		}
		Main.spriteBatch.Draw
		(
			LaserEnd!.Value,
			playerCenter + (playerCenter.SafeDirectionTo(mousePosClamped) * length) - Main.screenPosition + new Vector2(-2f, -6f).RotatedBy(rotation) * (scale / 2f),
			new Rectangle(0, 0, LaserEnd!.Value.Width, LaserEnd!.Value.Height),
			Items.Material.PointingLaser.TeamColor(player),
			rotation,
			TextureAssets.Projectile[Type].Size() / 2f,
			scale,
			SpriteEffects.None,
			1f
		);
		return false;
	}

	public override void AI()
	{
		Projectile.velocity = Vector2.Zero;

		Player player = Main.player[Projectile.owner];
		if (!player.active || player.dead || player.noItems || player.CCed)
		{
			Projectile.Kill();
			return;
		}
		if (Main.myPlayer == Projectile.owner && Main.mapFullscreen)
		{
			Projectile.Kill();
			return;
		}
		if (!player.channel)
		{
			Projectile.Kill();
			return;
		}
		Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);
		//float distToPlayer = playerCenter.DistanceSQ(player.GetModPlayer<AvalonPlayer>().MousePosition);
		//float playerVelMod = Utils.Remap(distToPlayer / 2116f, 0.4f, 1f, 0f, 1f);
		//Vector2 newPlayerVel = player.velocity * playerVelMod;
		Vector2 mousePosClamped = Vector2.Clamp(player.GetModPlayer<AvalonPlayer>().MousePosition + player.velocity, Vector2.Zero, new Vector2(Main.maxTilesX, Main.maxTilesY) * 16f);
		Vector2 dirToMouse = playerCenter.SafeDirectionTo(mousePosClamped) * 16f;
		Projectile.Center = playerCenter + dirToMouse;
		player.ChangeDir(MathF.Sign(dirToMouse.X) == -1 ? -1 : 1); // 0 or 1 both return 1, if 0 returns -1 then the rotation will be incorrect

		// Copied from FlailTemplate/ExampleAdvancedFlail
		Projectile.timeLeft = 2; // Makes sure the flail doesn't die (good when the flail is resting on the ground)
		player.heldProj = Projectile.whoAmI;
		player.SetDummyItemTime(2); //Add a delay so the player can't button mash the flail
		player.itemRotation = dirToMouse.ToRotation() - player.fullRotation; // subtract player.fullRotation to fix rotation while laying in beds
		if (Projectile.Center.X < playerCenter.X)
		{
			player.itemRotation += MathF.PI;
		}
		player.itemRotation = MathHelper.WrapAngle(player.itemRotation);

		Lighting.AddLight(playerCenter + (dirToMouse * 3f), Items.Material.PointingLaser.TeamColor(Main.LocalPlayer).ToVector3() * 0.2f);
	}
}
