using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode.ChaosTome;

public class ChaosTome : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<ChaosBolt>(), 21, 4f, 8, 8f, 25, 25);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 54);
		Item.UseSound = SoundID.Item20;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(10, 0);
	}
}
public class ChaosBolt : ModProjectile
{
	public override string Texture => $"Terraria/Images/NPC_{NPCID.ChaosBall}";
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(16);
		Projectile.aiStyle = -1;
		Projectile.tileCollide = false;
		Projectile.friendly = true;
		Projectile.timeLeft = 540;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Projectile.damage = (int)(Projectile.damage * 0.9f);
	}
	public float opacityMult = 1f;
	public override void AI()
	{
		int dustAmount = 4;

		if (Projectile.ai[1] >= 20f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
		}
		Projectile.rotation += 0.3f * Projectile.direction;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
			return;
		}
		if (Main.tile[Projectile.Center.ToTileCoordinates()].HasUnactuatedTile)
		{
			Projectile.ai[2]++;
			dustAmount = 1;
			opacityMult = 0.5f;
			if (Projectile.ai[2] > 30)
			{
				Projectile.timeLeft--;
			}
			if (Projectile.ai[2] == 120)
			{
				Projectile.timeLeft = 0;
			}
		}
		else
		{
			Projectile.ai[2] = 0;
			opacityMult = 1f;
		}
		for (int num127 = 0; num127 < dustAmount; num127++)
		{
			int num128 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1.3f);
			Main.dust[num128].noGravity = true;
			Main.dust[num128].position += new Vector2(0, -4) + (Vector2.Normalize(Projectile.velocity) * 6);
			Dust dust = Main.dust[num128];
			dust.velocity *= 0.3f;
			Main.dust[num128].velocity.X -= Projectile.velocity.X * 0.2f;
			Main.dust[num128].velocity.Y -= Projectile.velocity.Y * 0.2f;
		}
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(250, 250, 250, 100) * opacityMult;
	}
	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 10; i++)
		{
			Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, Main.rand.NextVector2Circular(1.7f, 1.7f).RotatedBy(i), 70, default, 0.6f);
		}
		for (int i = 0; i < 5; i++)
		{
			Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, Main.rand.NextVector2Circular(1.3f, 1.3f).RotatedBy(i + 0.3f), 100, default, 0.9f);
		}
	}
}

