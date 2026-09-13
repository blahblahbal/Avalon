using Avalon.Core;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class QuantumBeam : ModProjectile
{
	private static Asset<Texture2D>? texture2;
	public override void SetStaticDefaults()
	{
		texture2 = ModContent.Request<Texture2D>(Texture + "2");
	}
	public override void SetDefaults()
	{
		Projectile.width = 25;
		Projectile.height = 25;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.alpha = 255;
		Projectile.friendly = true;
		Projectile.timeLeft = 300;
		Projectile.scale = 1f;
		Projectile.tileCollide = false;
		Projectile.penetrate = 5;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return Color.Black;
	}
	public override bool? CanHitNPC(NPC target)
	{
		if (Projectile.ai[1] >= 0)
			return base.CanHitNPC(target);
		else
			return false;
	}
	public override bool CanHitPvp(Player target)
	{
		if (Projectile.ai[1] >= 0)
			return base.CanHitPvp(target);
		else
			return false;
	}
	public override bool ShouldUpdatePosition()
	{
		if (Projectile.ai[1] >= 0)
			return base.ShouldUpdatePosition();
		else
			return false;
	}
	public override void AI()
	{
		Projectile.ai[1]++;
		if (Projectile.ai[1] < -1 && Projectile.ai[2] == 0)
		{
			Projectile.velocity = Projectile.velocity.LengthClamp(25);
			Projectile.ai[2]++;
			SoundEngine.PlaySound(Sounds.Item.ReverseWindChime.Asset with { pitchVariance = 0.2f, pitch = 0.2f, MaxInstances = 10}, Projectile.Center);
			Main.ParticleSystem_World_OverPlayers.Add(new QuantumPortal(Projectile.Center));

			for (int i = 0; i < 15; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Stone);
				d.velocity = Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(2,6);
				d.noGravity = true;
				d.fadeIn += Main.rand.NextFloat(1.5f);
				d.color = Color.Black;
			}
		}
		if (Projectile.ai[1] == -1)
		{
			int NPC = ClassExtensions.FindClosestNPC(Projectile, 400, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly || !Collision.CanHit(npc, Projectile));
			float speed = Projectile.velocity.Length();
			if (NPC != -1)
			{
				Projectile.velocity = Projectile.Center.DirectionTo(Main.npc[NPC].Center) * speed;
			}

			for (int j = 0; j < 20; j++)
			{
				int DustType = DustID.CorruptTorch;
				if (Main.rand.NextBool())
					DustType = DustID.HallowedTorch;

				Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustType);
				D.noGravity = true;
				D.fadeIn = Main.rand.NextFloat(0, 1);
				D.velocity = Vector2.Normalize(Projectile.velocity).RotatedByRandom(0.3f) * Main.rand.NextFloat(1, 6);
			}
			//Projectile.extraUpdates++;
			Projectile.penetrate = 1;
		}
		if (Projectile.ai[1] == 1)
		{
			SoundEngine.PlaySound(Sounds.Item.QuantumClaymorePortal.Asset with { pitchVariance = 1f, volume = 0.5f, MaxInstances = 10 }, Projectile.Center);
		}
		if (Projectile.ai[1] > 40)
		{
			Projectile.velocity *= 0.95f;
		}
		if (Projectile.ai[1] > 60)
		{
			Projectile.Kill();
		}
		if (Projectile.ai[1] >= 0)
		{
			int DustType = DustID.CorruptTorch;
			if (Main.rand.NextBool())
				DustType = DustID.HallowedTorch;

			if (Main.rand.NextBool(2))
			{
				Dust CoolDust1 = Dust.NewDustDirect(Projectile.position + Vector2.Normalize(Projectile.velocity) * 30, Projectile.width, Projectile.height, DustType);
				CoolDust1.noGravity = true;
				CoolDust1.velocity = Projectile.velocity;
				CoolDust1.fadeIn = 1;
			}
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
			Projectile.alpha = (int)(Projectile.alpha * 0.86f);

			if (!Projectile.tileCollide && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.tileCollide = true;
			}
		}
	}
	public override void OnKill(int timeLeft)
	{
		var t = AssetReferences.Assets.Textures.InverseGlowRing.Asset;
		t.Wait();
		for (int i2 = 0; i2 < 2; i2++)
		{
			var ring = VanillaParticles.RequestFadingParticle();
			ring.SetBasicInfo(t, null, Vector2.Zero, Projectile.Center);
			int time = Main.rand.Next(15, 20);
			ring.SetTypeInfo(time);
			ring.Scale = Vector2.One * 0.15f;
			ring.ScaleVelocity = Vector2.One.RotatedByRandom(0.75f) * Main.rand.NextFloat(0.05f,0.075f);
			ring.ScaleAcceleration = ring.ScaleVelocity / -time;
			ring.FadeInNormalizedTime = 0.1f;
			ring.FadeOutNormalizedTime = 0.1f;
			ring.ColorTint = i2 == 0 ? new Color(Main.rand.Next(128, 255), 0, 255, 0) : Color.Black;
			ring.Rotation = Main.rand.NextFloatDirection();
			ring.RotationVelocity = Main.rand.NextFloat(-0.15f, 0.15f);
			Main.ParticleSystem_World_OverPlayers.Add(ring);
		}

		for (int i = 0; i <= 20; i++)
		{
			int DustType = DustID.CorruptTorch;
			if (Main.rand.NextBool())
				DustType = DustID.HallowedTorch;

			Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustType);
			D.noGravity = !Main.rand.NextBool(3);
			if (D.noGravity)
				D.fadeIn = Main.rand.NextFloat(1, 1.3f);
			D.velocity = Main.rand.NextVector2Circular(4, 4);
		}
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.ShadowFlame, 300);

		if (hit.Crit)
		{
			int damage = (int)(Projectile.damage * 0.6f);
			if (damage <= 0)
				return;
			Vector2 SwordSpawn = Projectile.Center + Main.rand.NextVector2Circular(300, 300);
			Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), SwordSpawn, SwordSpawn.DirectionTo(target.Center) * (Projectile.velocity.Length() * Main.rand.NextFloat(1.1f, 1.3f)), ModContent.ProjectileType<QuantumBeam>(), (int)(Projectile.damage * 0.6f), Projectile.knockBack, Projectile.owner, 0, Main.rand.Next(-20, -10));
		}
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.ShadowFlame, 300);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Main.spriteBatch.End();
		BlendState BlendS = new BlendState
		{
			ColorBlendFunction = BlendFunction.ReverseSubtract,
			ColorDestinationBlend = Blend.One,
			ColorSourceBlend = Blend.SourceAlpha,
			AlphaBlendFunction = BlendFunction.ReverseSubtract,
			AlphaDestinationBlend = Blend.One,
			AlphaSourceBlend = Blend.SourceAlpha
		};
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendS, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

		Rectangle frame = TextureAssets.Projectile[Type].Frame();
		Vector2 frameOrigin = frame.Size() / 2f;
		Vector2 offset = new Vector2((Projectile.width / 1) - frameOrigin.X, Projectile.height - frame.Height);
		Vector2 drawPos = Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, frame, Color.Lerp(new Color(64, 255, 64), new Color(128, 255, 64), Main.masterColor) * Projectile.Opacity, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);

		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

		Main.EntitySpriteDraw(texture2.Value, drawPos, frame, Color.Lerp(new Color(255, 64, 255), new Color(128, 64, 255), Main.masterColor) * Projectile.Opacity * 0.4f, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture2.Value, drawPos, frame, new Color(255, 255, 255, 0) * Projectile.Opacity * 0.2f, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);

		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
		return false;
	}
}
