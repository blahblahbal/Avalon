using Avalon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode.Smogscreen;
public class Smogscreen : ModItem
{
	public SoundStyle gas = new("Terraria/Sounds/Item_34")
	{
		Volume = 0.5f,
		Pitch = -0.5f,
		PitchVariance = 0.1f,
		MaxInstances = 10,
	};
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.Vilethorn);
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.shoot = ModContent.ProjectileType<Smog>();
		Item.useAnimation = 40;
		Item.useTime = 10;
		Item.damage = 8;
		Item.consumeAmmoOnFirstShotOnly = true;
		Item.shootSpeed = 6.5f;
		Item.ArmorPenetration = 7;
		Item.UseSound = gas;
	}
}
public class Smog : ModProjectile
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
		Projectile.alpha = 254;
		Projectile.friendly = true;
		Projectile.timeLeft = 720;
		Projectile.ignoreWater = true;
		Projectile.hostile = false;
		Projectile.scale = 0.9f;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		if (Projectile.ai[2] > 1)
			Projectile.alpha += 1;
		else
			Projectile.alpha -= 7;

		if (Projectile.alpha <= 100)
			Projectile.ai[2]++;

		if (Projectile.alpha == 255) Projectile.Kill();

		Projectile.velocity = Projectile.velocity.RotatedByRandom(0.1f) * 0.985f;
		Projectile.rotation += MathHelper.Clamp(Projectile.velocity.Length() * 0.1f, -0.3f, 0.3f);
		Projectile.scale *= 1.005f;
		Projectile.Resize((int)(36 * Projectile.scale), (int)(36 * Projectile.scale));
	}

	public override bool? CanHitNPC(NPC target)
	{
		return Projectile.alpha < 220 && !target.friendly;
	}

	public override bool CanHitPvp(Player target)
	{
		return Projectile.alpha < 220;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.Poisoned, 180);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Poisoned, 180);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		ClassExtensions.DrawGas(TextureAssets.Projectile[Type].Value, lightColor, Projectile, 4, 6);
		return false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity = oldVelocity * Main.rand.NextFloat(0.4f, 0.6f);
		Projectile.tileCollide = false;
		return false;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
}