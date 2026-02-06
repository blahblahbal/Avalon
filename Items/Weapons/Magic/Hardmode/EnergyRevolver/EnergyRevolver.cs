using Avalon.Common.Extensions;
using Avalon.Common.Interfaces;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.EnergyRevolver;

public class EnergyRevolver : ModItem
{
	SoundStyle LaserNoise = new("Terraria/Sounds/Item_91")
	{
		Volume = 0.5f,
		PitchVariance = 0.3f,
		MaxInstances = 7,
		Pitch = 1.6f
	};
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeapon(50, 20, ModContent.ProjectileType<EnergyLaser>(), 36, 2f, 6, 5f, 6, 6, true);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 4);
		Item.UseSound = LaserNoise;
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 newPos = position + new Vector2(0, -6 * player.direction).RotatedBy(velocity.ToRotation());
		Vector2 beamStartPos = newPos + Vector2.Normalize(velocity) * 50;
		Projectile.NewProjectile(source, newPos, velocity, type, damage, knockback, player.whoAmI, beamStartPos.X, beamStartPos.Y);
		ParticleSystem.NewParticle(new EnergyRevolverParticle(Vector2.Normalize(velocity) * 2, new Color(64, 255, 255, 0), 0, 0.8f, 14), beamStartPos);
		ParticleSystem.NewParticle(new EnergyRevolverParticle(default, new Color(64, 64, 255, 0), 0, 1, 20), beamStartPos);
		return false;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-4f, 0);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.LaserRifle)
			.AddIngredient(ItemID.Lens, 10)
			.AddIngredient(ModContent.ItemType<Material.BloodshotLens>(), 5)
			.AddIngredient(ItemID.BlackLens)
			.AddIngredient(ItemID.SoulofFright, 16)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class EnergyLaser : ModProjectile, ISyncedOnHitEffect
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[Type] = 4800;
	}
	public override void SetDefaults()
	{
		Projectile.width = 9;
		Projectile.height = 9;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.tileCollide = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
		Projectile.timeLeft = 20000;
		Projectile.extraUpdates = 600;
		Projectile.alpha = 0;
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 0; height = 0;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public override void AI()
	{
		Projectile.ai[2]++;

		if (Projectile.ai[2] == 600)
		{
			Projectile.damage = 0;
			ParticleSystem.NewParticle(new EnergyRevolverParticle(default, new Color(64, 255, 255, 0), 0, 1, 14), Projectile.Center);
			Projectile.velocity = Vector2.Zero;
			for (int i = 0; i < 10; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(3, 3), 24);
				d.scale *= 2;
				d.noGravity = true;
			}
		}
		if (Projectile.timeLeft % 30 == 0)
			Projectile.alpha += 1;
		if (Projectile.alpha >= 255)
		{
			Projectile.Kill();
		}
		// fix to the laser not rendering if fired near the edges of the world, PROBABLY won't have issues with world size mods but... idk
		if (Projectile.position.X <= 16 || Projectile.position.X >= (Main.maxTilesX - 1) * 16 || Projectile.position.Y <= 16 || Projectile.position.Y >= (Main.maxTilesY - 1) * 16)
		{
			Projectile.velocity = Vector2.Zero;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Projectile.damage = (int)(Projectile.damage * 0.9f);
		if (Main.rand.NextBool(6))
			target.AddBuff(BuffID.OnFire3, 60 * 3);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (Main.rand.NextBool(6))
			target.AddBuff(BuffID.OnFire3, 60 * 3);
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity = Vector2.Zero;
		return false;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 StartPos = new Vector2(Projectile.ai[0], Projectile.ai[1]);
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, TextureAssets.Projectile[Type].Value.Width, TextureAssets.Projectile[Type].Value.Height), new Color(Projectile.Opacity, Projectile.Opacity, 1f, 0), Projectile.Center.DirectionTo(StartPos).ToRotation() + MathHelper.PiOver2, new Vector2(TextureAssets.Projectile[Type].Value.Width / 2f, TextureAssets.Projectile[Type].Value.Height), new Vector2(Projectile.Opacity * 1.3f, Projectile.Center.Distance(StartPos)), SpriteEffects.None);
		return false;
	}

	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		ParticleSystem.NewParticle(new EnergyRevolverParticle(default, new Color(64, 128, 255, 0), 0, Main.rand.NextFloat(0.9f, 1.1f), 14), Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(3, 3), 24);
			d.scale *= 2;
			d.noGravity = true;
		}
	}
}