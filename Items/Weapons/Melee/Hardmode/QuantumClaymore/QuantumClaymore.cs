using Avalon.Common.Extensions;
using Avalon.Items.Material.Shards;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.QuantumClaymore;
public class QuantumClaymore : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<QuantumBeam>(), 88, 6f, 16f, 46, 23, scale: 1.1f);
		Item.rare = ModContent.RarityType<Rarities.QuantumRarity>();
		Item.value = Item.sellPrice(0, 10, 90);
		Item.UseSound = SoundID.Item15;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<WickedShard>(), 10)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddIngredient(ItemID.Ectoplasm, 20)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		int DustType = DustID.CorruptTorch;
		if (Main.rand.NextBool())
			DustType = DustID.HallowedTorch;

		for (int j = 0; j < 2; j++)
		{
			ClassExtensions.GetPointOnSwungItemPath(60f, 120f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
			Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
			Dust d = Dust.NewDustPerfect(location2, DustType, vector2 * 2f, 100, default(Color), 0.7f + Main.rand.NextFloat() * 1.3f);
			d.noGravity = true;
			d.velocity *= 2f;
			if (Main.rand.NextBool(20))
			{
				int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 140, default(Color), 1.3f);
				Main.dust[num15].position = location2;
				Main.dust[num15].fadeIn = 1.2f;
				Main.dust[num15].noGravity = true;
				Main.dust[num15].velocity *= 2f;
				Main.dust[num15].velocity += vector2 * 5f;
			}
		}
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		player.itemLocation = Vector2.Lerp(player.itemLocation, player.MountedCenter, 1f);
	}
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		Vector2 SwordSpawn = player.position - new Vector2(Main.rand.Next(60, 180) * player.direction, Main.rand.Next(-75, 75));
		Projectile P = Projectile.NewProjectileDirect(player.GetSource_FromThis(), SwordSpawn, SwordSpawn.DirectionTo(target.Center) * (Item.shootSpeed * Main.rand.NextFloat(1.2f, 1.6f)), ModContent.ProjectileType<QuantumBeam>(), (int)(Item.damage * 0.6f), Item.knockBack * 0.1f, player.whoAmI, 0, Main.rand.Next(-20, -10));
		target.AddBuff(BuffID.ShadowFlame, 300);
	}
}
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
			Projectile.ai[2]++;
			ParticleSystem.AddParticle(new QuantumPortal(), Projectile.Center, default, default);
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

	public SoundStyle StarSoundReal = new SoundStyle("Terraria/Sounds/Item_9")
	{
		Volume = 0.6f,
		Pitch = -1f,
		PitchVariance = 0.1f,
		MaxInstances = 10,
	};

	public SoundStyle Impac = new SoundStyle("Terraria/Sounds/Item_72")
	{
		Volume = 0.6f,
		MaxInstances = 10,
	};
	public override void OnSpawn(IEntitySource source)
	{
		SoundEngine.PlaySound(StarSoundReal, Projectile.position);
	}
	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i <= 20; i++)
		{
			int DustType = DustID.CorruptTorch;
			if (Main.rand.NextBool())
				DustType = DustID.HallowedTorch;

			Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustType);
			D.noGravity = !Main.rand.NextBool(3);
			if (D.noGravity)
				D.fadeIn = Main.rand.NextFloat(1, 2);
			D.velocity = Main.rand.NextVector2Circular(4, 4);
		}
		SoundEngine.PlaySound(Impac, Projectile.position);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.ShadowFlame, 300);

		if (hit.Crit)
		{
			Vector2 SwordSpawn = Projectile.Center + Main.rand.NextVector2Circular(300, 300);
			Projectile P = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), SwordSpawn, SwordSpawn.DirectionTo(target.Center) * (Projectile.velocity.Length() * Main.rand.NextFloat(1.1f, 1.3f)), ModContent.ProjectileType<QuantumBeam>(), (int)(Projectile.damage * 0.6f), Projectile.knockBack, Projectile.owner, 0, Main.rand.Next(-20, -10));
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
