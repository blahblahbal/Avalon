using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.Ancient;

public class Ancient : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<AncientSandstorm>(), 46, 4f, 40, 21f, 25, 25, 1f, 2);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 25);
		Item.UseSound = SoundID.Item34;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(5, 0);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		player.itemRotation = Utils.AngleLerp(player.itemRotation, 0f, 0.9f);
		NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
		NetMessage.SendData(MessageID.ShotAnimationAndSound, -1, -1, null, player.whoAmI);

		Vector2 dirToMouse = player.SafeDirectionTo(Main.MouseWorld);
		Vector2 velMult = player.velocity * new Vector2(MathF.Abs(dirToMouse.X), MathF.Abs(dirToMouse.Y)); // The player's current velocity, multiplied by the unsigned cosine & sine of the angle to the mouse

		int projCount = 5;
		for (int i = 1; i <= projCount; i++)
		{
			float distToMouseScaled = position.Distance(Main.MouseWorld);
			float zoomScale = Main.screenHeight / Main.GameViewMatrix.Zoom.Y;
			distToMouseScaled /= zoomScale / 2f;
			if (distToMouseScaled > 1f)
			{
				distToMouseScaled = 1f;
			}
			float radius = MathF.Pow(1f - distToMouseScaled, 4f);
			radius = Utils.Remap(radius, 0f, 0.7f, 0.005f, 0.4f);
			int projNumOffset = (int)(i - 0.5f - (projCount / 2f));
			Vector2 dir = Vector2.Normalize(velocity).RotatedBy(projNumOffset * radius).RotatedByRandom(0.05f);

			float distVelMod = Math.Clamp(distToMouseScaled, 0.3f, 1f);
			float projNumVelMod = (1f - (Math.Abs(projNumOffset) * 0.1f)) * Main.rand.NextFloat(0.5f, 1f);
			Vector2 vel = dir * velocity.Length() * distVelMod * projNumVelMod;

			vel += ((velMult * 0.7f) + (player.velocity * 0.1f)) * Math.Clamp(distToMouseScaled, 0.7f, 1f);

			Projectile.NewProjectile(source, position, vel, type, damage, knockback, player.whoAmI, (i + 1) % 2);
		}
		return false;
	}
	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.GetModPlayer<AvalonPlayer>().AncientLessCost)
		{
			mult *= 0.5f;
		}
	}
}
public class AncientSandstorm : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<Ancient>().DisplayName;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NoLiquidDistortion[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.width = 36;
		Projectile.height = 36;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.alpha = 128;
		Projectile.friendly = true;
		Projectile.timeLeft = 720;
		Projectile.ignoreWater = true;
		Projectile.hostile = false;
		Projectile.scale = 0.4f;
		Projectile.extraUpdates = 1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
	}
	public bool CanSpawnChild { get => Convert.ToBoolean(Projectile.ai[0]); set => Projectile.ai[0] = value.ToInt(); }
	public override void AI()
	{
		Projectile.ai[1]++;
		if (Projectile.ai[2] > 1)
		{
			Projectile.alpha += 1;
			if (Projectile.ai[1] % 10 == 0)
			{
				Projectile.damage--;
			}
		}
		else
			Projectile.alpha -= 3;

		if (Projectile.alpha <= 100)
		{
			Projectile.ai[2]++;
		}

		if (Projectile.alpha == 255) Projectile.Kill();

		Projectile.velocity = Projectile.velocity.RotatedByRandom(CanSpawnChild ? 0.025f : 0.05f) * 0.985f;
		Projectile.rotation += MathHelper.Clamp(Projectile.velocity.Length() * 0.03f, -0.3f, 0.3f);

		AvalonGlobalProjectile.AvoidOtherGas(Projectile, new Vector2(32), new Vector2(32), 0.5f);
		if (!AvalonGlobalProjectile.GasAvoidTiles(Projectile, true) || Projectile.scale < 1f)
		{
			Projectile.scale += Projectile.ai[0] == 0f ? 0.02f : 0.01f;
			Projectile.Resize((int)(32 * Projectile.scale), (int)(32 * Projectile.scale));
		}

		int rand = (int)(250 / Utils.Remap(Projectile.velocity.Length(), 0f, 10f, 1.5f, 50f));
		if (Main.rand.NextBool(rand + ((255 - Projectile.alpha) / 15)))
		{
			float dustMagnitude = 3f;
			float dustX = Main.rand.NextFloat(-dustMagnitude, dustMagnitude);
			float dustY = Main.rand.NextFloat(-dustMagnitude, dustMagnitude);
			int dustAlpha = Math.Clamp((int)(Projectile.alpha * 1.4f), 0, 230);
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AncientSandDust>(), dustX, dustY, dustAlpha, default, 0.5f);
			dust.velocity *= 0.9f;
			dust.fadeIn = 2f;
			dust.noGravity = true;
		}
		if (Projectile.alpha < 150 && Projectile.alpha % 32 == 0 && CanSpawnChild)
		{
			Vector2 vel = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.TwoPi) * 3f * MathF.Pow(Projectile.alpha / 255f, 1.5f);
			vel += Projectile.velocity * 0.2f;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner);
		}
	}
	public override bool? CanHitNPC(NPC target)
	{
		return (Projectile.alpha < 220 || Projectile.ai[2] < 1) && !target.friendly;
	}

	public override bool CanHitPvp(Player target)
	{
		return Projectile.alpha < 220;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		ClassExtensions.DrawGas(TextureAssets.Projectile[Type].Value, lightColor * 0.8f, Projectile, 4, 6);
		return false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity = Projectile.oldVelocity * 0.7f;
		AvalonGlobalProjectile.GasAvoidTiles(Projectile, true); // I could NOT find a way to make it not go through tiles sometimes without calling this method both here and in AI
		return false;
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 32;
		height = 32;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
}
