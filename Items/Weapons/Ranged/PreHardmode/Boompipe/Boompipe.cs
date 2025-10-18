using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Dusts;
using Avalon.Items.Material.Shards;
using Avalon.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.Boompipe;

public class Boompipe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBlowpipe(11, 3.5f, 14.5f, 40, 40);
		Item.rare = ItemRarityID.Orange;
		Item.value = 24000;
		Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Boompipe");
	}
	public override void UseItemFrame(Player player)
	{
		player.bodyFrame.Y = player.bodyFrame.Height * 2;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0, -7);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int amount = Main.rand.Next(4, 7);
		for (int i = 0; i < amount; i++)
		{
			Vector2 perturbedSpeed = AvalonUtils.GetShootSpread(velocity, position, Type, MathHelper.ToRadians(14), Main.rand.NextFloat(-4.125f, 0f), ItemID.PoisonDart, true);
			if (i == 0)
			{
				perturbedSpeed = AvalonUtils.GetShootSpread(velocity, position, Type, MathHelper.ToRadians(5), Main.rand.NextFloat(-2.475f, 1.65f), ItemID.PoisonDart, true);
				Projectile P = Projectile.NewProjectileDirect(source, player.RotatedRelativePoint(position - new Vector2(0, 8 * player.gravDir)), perturbedSpeed, type, damage, knockback);
				P.netUpdate = true;
				P.GetGlobalProjectile<BoompipeProjVisuals>().Shards = true;
			}
			else
			{
				Projectile P = Projectile.NewProjectileDirect(source, player.RotatedRelativePoint(position - new Vector2(0, 8 * player.gravDir)), perturbedSpeed, ModContent.ProjectileType<BoompipeShrapnel>(), (int)(damage / 2.6f), knockback);
				P.netUpdate = true;
				P.GetGlobalProjectile<BoompipeProjVisuals>().Shards = true;
			}
		}
		return false;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.HellstoneBar, 15)
			.AddIngredient(ModContent.ItemType<FireShard>(), 1)
			.AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.ImpStaff)
			.Register();
	}
	public class BoompipeShrapnel : ModProjectile
	{
		private static Asset<Texture2D>? glow;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Type] = 1;
			ProjectileID.Sets.TrailingMode[Type] = 2;
			Main.projFrames[Type] = 4;
			glow = ModContent.Request<Texture2D>(Texture + "_Glow");
		}
		public override void SetDefaults()
		{
			Projectile.Size = new Vector2(8);
			Projectile.aiStyle = 1;
			Projectile.tileCollide = true;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
		}
		public float rotation { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }
		public float initialTimeLeft { get => Projectile.ai[1]; set => Projectile.ai[1] = value; }
		public float glowLerpDiv { get => Projectile.ai[2]; set => Projectile.ai[2] = value; }
		public override void OnSpawn(IEntitySource source)
		{
			Projectile.frame = Main.rand.Next(4);
			glowLerpDiv = Main.rand.NextFloat(35f, 60f);
			initialTimeLeft = Projectile.timeLeft;
			rotation = Main.rand.NextFromList(Main.rand.NextFloat(-0.35f, -0.2f), Main.rand.NextFloat(0.2f, 0.35f));
		}
		public override void AI()
		{
			if (Projectile.timeLeft == (int)initialTimeLeft && !Main.rand.NextBool(3))
			{
				Vector2 correctedPos = Projectile.Center + new Vector2(0, 8);
				Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(14));
				Gore G = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), correctedPos + Vector2.Normalize(Projectile.velocity) * 50 - new Vector2(16, 24), Vector2.Zero, Main.rand.NextFromList(GoreID.Smoke1, GoreID.Smoke2, GoreID.Smoke3), Main.rand.NextFloat(0.7f, 1f));
				G.velocity = perturbedSpeed * 0.18f * Main.rand.NextFloat(0.45f, 1f);
				G.alpha = Main.rand.Next(80, 175);
			}
			Projectile.rotation = Projectile.oldRot[0] + rotation;
			if (((int)initialTimeLeft - Projectile.timeLeft) / glowLerpDiv * 10f < 15f && Main.rand.NextBool(1 + (int)(((int)initialTimeLeft - Projectile.timeLeft) / glowLerpDiv * 15f)))
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 0, default, 1f);
				if (!Main.rand.NextBool(4))
				{
					Main.dust[d].scale = 1.3f;
					Main.dust[d].noGravity = true;
				}
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (int i = 0; i < 4; i++)
			{
				int dustType = ModContent.DustType<BrimstoneDust>();
				// just a lazy way of making the dust colour "fade" like the projectile does
				if (initialTimeLeft - Projectile.timeLeft < glowLerpDiv / 4)
				{
					dustType = ModContent.DustType<BoompipeShrapnelDust>();
				}
				else if (initialTimeLeft - Projectile.timeLeft < glowLerpDiv / 2 && Main.rand.NextBool(2))
				{
					dustType = ModContent.DustType<BoompipeShrapnelDust>();
				}
				else if (initialTimeLeft - Projectile.timeLeft < glowLerpDiv && Main.rand.NextBool(4))
				{
					dustType = ModContent.DustType<BoompipeShrapnelDust>();
				}
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0, 0, 0, default, 0.7f);
			}
			SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
			return base.OnTileCollide(oldVelocity);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Projectile.type];
			Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Value.Width, frameHeight);
			Color glowLerp = Color.Lerp(new Color(255, 255, 255, 0), new Color(0, 0, 0, 0), (initialTimeLeft - Projectile.timeLeft) / glowLerpDiv);

			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.position - Main.screenPosition + (Projectile.Size / 2f), frame, lightColor, Projectile.rotation, new Vector2(TextureAssets.Projectile[Type].Value.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(glow.Value, Projectile.position - Main.screenPosition + (Projectile.Size / 2f), frame, glowLerp, Projectile.rotation, new Vector2(TextureAssets.Projectile[Type].Value.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
			return false;
		}
		public static int OnFireCutoff = 35;
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (initialTimeLeft - Projectile.timeLeft <= glowLerpDiv - OnFireCutoff)
			{
				target.AddBuff(BuffID.OnFire, (int)Utils.Remap(glowLerpDiv - (initialTimeLeft - Projectile.timeLeft), OnFireCutoff, 60, TimeUtils.SecondsToTicks(1), TimeUtils.SecondsToTicks(3)));
			}
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			if (initialTimeLeft - Projectile.timeLeft <= glowLerpDiv - OnFireCutoff)
			{
				target.AddBuff(BuffID.OnFire, (int)Utils.Remap(glowLerpDiv - (initialTimeLeft - Projectile.timeLeft), OnFireCutoff, 60, TimeUtils.SecondsToTicks(1), TimeUtils.SecondsToTicks(3)));
			}
		}
	}
	public class BoompipeProjVisuals : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		public bool Shards;
		public int blastDust;
		public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
		{
			bitWriter.WriteBit(Shards);
		}
		public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
		{
			Shards = bitReader.ReadBit();
		}
		public override void PostAI(Projectile projectile)
		{
			if (Shards && blastDust < 20)
			{
				projectile.netUpdate = true;
				if (blastDust == 0)
				{
					Vector2 correctedPos = projectile.Center + new Vector2(0, 8);
					Vector2 perturbedSpeed = projectile.velocity.RotatedByRandom(MathHelper.ToRadians(14));
					Dust D = Dust.NewDustDirect(correctedPos + Vector2.Normalize(projectile.velocity) * 50 - new Vector2(0, 14), default, default, DustID.Smoke, perturbedSpeed.X * Main.rand.NextFloat(0.55f, 1.1f), perturbedSpeed.Y * Main.rand.NextFloat(0.55f, 1.1f), 100, Main.rand.NextFromList(Color.LightGray, Color.Gray), Main.rand.NextFloat(0.7f, 1.3f));
					D.velocity = perturbedSpeed * 0.15f * Main.rand.NextFloat(0.65f, 1f);
					D.alpha = Main.rand.Next(120, 175);
					D.fadeIn = 1;
					Vector2 perturbedSpeed2 = projectile.velocity.RotatedByRandom(MathHelper.ToRadians(24));
					Dust D2 = Dust.NewDustDirect(correctedPos + Vector2.Normalize(perturbedSpeed2) * 50 - new Vector2(0, 14), default, default, DustID.Torch, perturbedSpeed.X * Main.rand.NextFloat(0.55f, 1.1f), perturbedSpeed.Y * Main.rand.NextFloat(0.55f, 1.1f), default, default, Main.rand.NextFloat(0.7f, 1.3f));
					D2.velocity = perturbedSpeed2 * 0.15f * Main.rand.NextFloat(0.65f, 1.5f);
					D2.fadeIn = 1;
					D2.noGravity = true;
				}
				if (Main.rand.NextBool(3 + blastDust))
				{
					Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Smoke);
					d.velocity = projectile.velocity.RotatedByRandom(0.1f) * 0.5f * (projectile.extraUpdates + 1);
					d.scale = Main.rand.NextFloat(0.3f, 0.7f);
					d.fadeIn = 2;
					d.noGravity = true;
					if (!Main.rand.NextBool(3))
					{
						Dust d2 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Torch);
						d2.velocity = projectile.velocity.RotatedByRandom(0.1f) * 0.5f * (projectile.extraUpdates + 1);
						d2.scale = Main.rand.NextFloat(0.6f, 1.1f);
						d2.fadeIn = 1;
						d2.noGravity = true;
					}
				}
				blastDust++;
			}
		}
	}
}
