using Avalon.Common.Extensions;
using Avalon.Dusts;
using Avalon.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode.SunsShadow;

public class SunsShadow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBlowpipe(27, 3.5f, 4.5f, 40, 40);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 7);
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(4, -2);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile P = Projectile.NewProjectileDirect(source, position - new Vector2(0, 4) + Vector2.Normalize(velocity) * 42, velocity, ModContent.ProjectileType<SunsShadowProj>(), damage, knockback, player.whoAmI, type);
		return false;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.BeetleHusk, 8)
			.AddIngredient(ItemID.TurtleShell, 1)
			.AddIngredient(ItemID.ChlorophyteBar, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class SunsShadowProj : ModProjectile
{
	private Color lightColor;
	private int dustType;
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 10;
		Projectile.alpha = 0;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.friendly = true;
		Rectangle dims = this.GetDims();
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
		DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
		Projectile.timeLeft = 200;

		lightColor = new Color(255, 200, 70);
		dustType = ModContent.DustType<SunsShadowDust>();
	}
	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int size = 16;
		hitbox.X -= size;
		hitbox.Y -= size;
		hitbox.Width += size * 2;
		hitbox.Height += size * 2;
	}
	public override void AI()
	{
		if (Projectile.ai[1] == 0)
		{
			Projectile.scale = 0.3f;
		}
		Projectile.ai[1]++;
		if (Projectile.ai[1] > 5 && !Main.rand.NextBool(3))
		{
			var dust = Dust.NewDust(Projectile.position - Projectile.velocity * 2, Projectile.width, Projectile.height, dustType, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.5f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 0.3f;
			Main.dust[dust].alpha = 20;
			Main.dust[dust].noLightEmittence = true;
		}
		if (Projectile.ai[1] % 20 == 0)
		{
			int TargetNPC = ClassExtensions.FindClosestNPC(Projectile, 500, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly || npc.Distance(Projectile.Center) > 1000);
			if (TargetNPC != -1)
			{
				SoundEngine.PlaySound(SoundID.Item73, Projectile.position);
				Vector2 dustRad = new Vector2(2f, 2f);
				float dustAngle = 1f;
				for (int i = 0; i < 20; i++)
				{
					var dust2 = Dust.NewDustPerfect(Projectile.Center + Projectile.velocity * 3f, dustType, dustRad.RotatedBy((MathHelper.Pi / 180f) * dustAngle), 50, default, 1f);
					dust2.noGravity = true;
					dust2.alpha = 20;
					dustAngle += 18f;
					dust2.noLightEmittence = true;
				}
				float angle = Main.rand.NextFloat(-2.5f, -3.5f);
				for (int i = 0; i < 2; i++)
				{
					Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center + Projectile.velocity * 3f, Vector2.Normalize(Projectile.Center.DirectionTo(Main.npc[TargetNPC].Center + new Vector2(0, -10f)).RotatedBy((MathHelper.Pi / 180f) * angle)) * Projectile.velocity.Length() * 1.25f, (int)Projectile.ai[0], Projectile.damage / 2, Projectile.knockBack);
					angle = Main.rand.NextFloat(2.5f, 3.5f);
				}
			}
		}
		if (Projectile.ai[1] <= 20)
		{
			Projectile.scale += 0.04f;
		}
		else if (Projectile.ai[1] % 40 < 20)
		{
			Projectile.scale += 0.01f;
		}
		else
		{
			Projectile.scale -= 0.01f;
		}
		Projectile.rotation += 0.025f * Projectile.direction;
		Lighting.AddLight(Projectile.Center, lightColor.ToVector3() * 0.8f);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 60 * 4);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.OnFire, 60 * 4);
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item45, Projectile.position);
		float dustAngle = 1f;
		for (int i = 0; i < 14; i++)
		{
			Vector2 dustRad = new Vector2(Main.rand.NextFloat(1.5f, 5f), Main.rand.NextFloat(1.5f, 5f));
			var dust = Dust.NewDustPerfect(Projectile.Center + Projectile.velocity * 2.5f, dustType, dustRad.RotatedBy((MathHelper.Pi / 180f) * dustAngle) + Projectile.velocity * 0.5f, 50, default, 1f);
			dust.noGravity = true;
			dust.alpha = 20;
			dust.scale *= 1.25f;
			dust.velocity *= 0.5f;
			dustAngle += 36f;
		}
		for (int i = 0; i < 6; i++)
		{
			int dust = Dust.NewDust(Projectile.position - Projectile.velocity * Main.rand.NextFloat(1f, 2f), Projectile.width, Projectile.height, dustType, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, default, default, 1.2f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].alpha = 20;
			Main.dust[dust].scale *= 1.25f;
			Main.dust[dust].velocity *= 0.65f;
		}
	}
}
