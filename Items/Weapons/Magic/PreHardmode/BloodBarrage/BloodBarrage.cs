using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode.BloodBarrage;

public class BloodBarrage : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<BloodBlob>(), 16, 4f, 8, 12f, 12, 24);
		Item.reuseDelay = 20;
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BloodBlob>(), (int)(damage * 1.25f), knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BloodBlob>(), damage, knockback, player.whoAmI);
		}
		return false;
	}
	public SoundStyle note = new SoundStyle("Terraria/Sounds/NPC_Hit_18")
	{
		Volume = 0.5f,
		Pitch = -0.5f,
		PitchVariance = 0.5f,
		MaxInstances = 10,
	};
	public override float UseTimeMultiplier(Player player)
	{
		if (player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
		{
			return 0.5f;
		}
		return base.UseTimeMultiplier(player);
	}
	public override bool? UseItem(Player player)
	{
		SoundEngine.PlaySound(note, player.Center);
		return true;
	}
	public override bool AltFunctionUse(Player player)
	{
		int healthSucked = 80;
		if (!player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
		{
			player.AddBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>(), 60 * 8);
			SoundEngine.PlaySound(SoundID.NPCDeath1, Main.LocalPlayer.position);

			CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, healthSucked, dramatic: false, dot: false);
			player.statLife -= healthSucked;
			if (player.statLife <= 0)
			{
				player.Hurt(PlayerDeathReason.ByCustomReason(NetworkText.FromKey($"Mods.Avalon.DeathText.{Name}_1", $"{player.name}")), healthSucked, 1, false, true, -1, false);
			}
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDustPerfect(player.MountedCenter, DustID.Blood, (player.velocity * 0.85f) + Main.rand.NextVector2Circular(2f, 1f).RotatedBy(i) + Main.rand.NextVector2Square(-3.5f, 3.5f), 100, default(Color), 0.7f + Main.rand.NextFloat() * 0.6f);
				Dust.NewDustPerfect(player.MountedCenter, DustID.Blood, (player.velocity * 0.85f) + Main.rand.NextVector2Circular(0.5f, 0.5f).RotatedBy(i) + Main.rand.NextVector2Square(-1.5f, 1.5f), 100, default(Color), 1f + Main.rand.NextFloat() * 0.6f);
			}
		}
		return !player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>());
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (!player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
		{
			velocity = AvalonUtils.GetShootSpread(velocity, position, Type, 0.11, random: true);
		}
		else
		{
			velocity = AvalonUtils.GetShootSpread(velocity, position, Type, 0.17, random: true);
		}
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(10, 0);
	}
}
public class BloodBlob : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 4;
	}
	public override void SetDefaults()
	{
		Projectile.penetrate = 1;
		Projectile.width = 10;
		Projectile.height = 10;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.timeLeft = 300;
		Projectile.scale = 1f;
		Projectile.alpha = 255;
	}
	private Player player => Main.player[Projectile.owner];
	public override void AI()
	{
		Lighting.AddLight(Projectile.position, 30 / 255f, 20 / 255f, 0);
		Projectile.spriteDirection = Projectile.direction;
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		Projectile.ai[0]++;
		if (Projectile.ai[0] == 4)
		{
			Projectile.alpha = 0;
			for (int num257 = 0; num257 < 15; num257++)
			{
				int newDust = Dust.NewDust(Projectile.position - new Vector2(Projectile.width / 2f, Projectile.height / 2f), Projectile.width * 2, Projectile.height * 2, DustID.Blood, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 100, default(Color), 1.25f);
				Main.dust[newDust].position = (Main.dust[newDust].position + Projectile.Center) / 2f;
				Main.dust[newDust].noGravity = true;
				Main.dust[newDust].velocity *= 1.5f;
			}
		}
		if (Projectile.ai[0] > 10)
		{
			Projectile.velocity.Y += Projectile.ai[0] / 100;
		}

		if (Projectile.velocity.Length() >= 20f)
		{
			Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 20f;
		}

		if (Projectile.ai[0] >= 4)
		{
			int blood = Dust.NewDust(Projectile.position - new Vector2(Projectile.width / 2f, Projectile.height / 2f), Projectile.width * 2, Projectile.height * 2, DustID.Blood, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f, 100, default(Color), 1f);
			Main.dust[blood].noGravity = true;
			Main.dust[blood].fadeIn = 0.75f;
		}

		Projectile.frameCounter++;
		if (Projectile.frameCounter > 4)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
		}
		if (Projectile.frame >= 4)
		{
			Projectile.frame = 0;
			Projectile.frameCounter = 0;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target.type != NPCID.TargetDummy && !NPCID.Sets.CountsAsCritter[target.type] && player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
		{
			int healAmount = Main.rand.Next(0, 2) + Main.rand.Next(0, 2) + 1;
			player.HealEffect(healAmount, true);
			player.statLife += healAmount;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Projectile.type];
		Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Value.Width, frameHeight);
		Vector2 drawPos = Projectile.Center - Main.screenPosition;

		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, frame, Color.White * 0.25f, Projectile.rotation, new Vector2(TextureAssets.Projectile[Type].Value.Width, frameHeight) / 2, Projectile.scale * 1.2f, Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, frame, Color.Lerp(Color.White, lightColor, 0.6f), Projectile.rotation, new Vector2(TextureAssets.Projectile[Type].Value.Width, frameHeight) / 2, Projectile.scale, Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

		return false;
	}
	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int size = Projectile.width * 2;
		hitbox.X -= size / 2;
		hitbox.Y -= size / 2;
		hitbox.Width += size;
		hitbox.Height += size;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath9, Projectile.position);
		for (int num237 = 0; num237 < 30; num237++)
		{
			int num239 = Dust.NewDust(Projectile.position - new Vector2(Projectile.width / 2f, Projectile.height / 2f), Projectile.width * 2, Projectile.height * 2, DustID.Blood, 0f, 0f, 50, default(Color), 0.5f * Main.rand.NextFloat(0, 3));
			Main.dust[num239].noGravity = true;
			Main.dust[num239].velocity *= 1.5f;
			Main.dust[num239].fadeIn = 0.5f;
		}
		for (int num237 = 0; num237 < 40; num237++)
		{
			int num239 = Dust.NewDust(Projectile.position - new Vector2(Projectile.width / 2f, Projectile.height / 2f), Projectile.width * 2, Projectile.height * 2, DustID.Blood, -Projectile.oldVelocity.X * Main.rand.NextFloat(-0.1f, -0.3f), Projectile.oldVelocity.Y * Main.rand.NextFloat(-0.1f, -0.4f), 50, default(Color), 0.5f * Main.rand.NextFloat(0, 3));
			Main.dust[num239].noGravity = false;
			Main.dust[num239].fadeIn = 0.75f;
		}
	}
}