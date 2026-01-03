using Avalon.Buffs.Debuffs;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.HemorrhagingHalberd;

public class HemorrhagingHalberd : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<HemorrhagingHalberdProj>(), 48, 4.5f, 35, 4f, true);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 3);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float RotationAmount = 0.4f;
		Projectile.NewProjectile(source, position, velocity.RotatedBy(RotationAmount * -player.direction), type, damage / 2, knockback, player.whoAmI, 0, 0, RotationAmount * 2);
		return false;
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}
public class HemorrhagingHalberdProj : SpearTemplate2
{
	public override LocalizedText DisplayName => ModContent.GetInstance<HemorrhagingHalberd>().DisplayName;
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
	}
	protected override float HoldoutRangeMax => 140;
	protected override float HoldoutRangeMin => 40;
	public override void PostAI()
	{
		Projectile.ai[1]++;
		Player player = Main.player[Projectile.owner];
		Projectile.position.Y += 10 * player.gravDir;
		Projectile.position.X += (Math.Abs(Projectile.velocity.Y - 1)) * 5 * player.direction;
		float duration = player.itemAnimationMax;

		Projectile.velocity = Projectile.velocity.RotatedBy(player.direction * (1 / duration) * Projectile.ai[2]);

		if (Main.rand.NextBool(3))
		{
			Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1.4f);
			d2.noGravity = true;
			d2.fadeIn = 1.5f;
			d2.velocity += Projectile.velocity * 3;
		}
		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<ContagionWeapons>(), 0, 0, 128);
		d.noGravity = true;
		d.velocity += Projectile.velocity * 3;
		if (Projectile.ai[1] % 4 == 0 && Main.myPlayer == Projectile.owner)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 3, ModContent.ProjectileType<PathogenSmoke>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
		}
	}
}
public class PathogenSmoke : ModProjectile
{
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
		Projectile.scale = 1f;
		Projectile.extraUpdates = 1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
	}

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
			Projectile.ai[2]++;

		if (Projectile.alpha == 255) Projectile.Kill();

		Projectile.velocity = Projectile.velocity.RotatedByRandom(0.05f) * 0.99f;
		Projectile.rotation += MathHelper.Clamp(Projectile.velocity.Length() * 0.03f, -0.3f, 0.3f);
		Projectile.scale += 0.005f;
		Projectile.Resize((int)(32 * Projectile.scale), (int)(32 * Projectile.scale));
		Lighting.AddLight(Projectile.Center, new Vector3(0.4f, 0.2f, 0.5f) * Projectile.scale * Projectile.Opacity * 0.3f);
	}
	public override bool? CanHitNPC(NPC target)
	{
		return (Projectile.alpha < 220 || Projectile.ai[2] < 1) && !target.friendly;
	}

	public override bool CanHitPvp(Player target)
	{
		return Projectile.alpha < 220;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Pathogen>(), 7 * 60);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Pathogen>(), 7 * 60);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		ClassExtensions.DrawGas(TextureAssets.Projectile[Type].Value, lightColor * 0.8f, Projectile, 4, 6);
		return false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity = Projectile.oldVelocity * 0.3f;
		return false;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
}
