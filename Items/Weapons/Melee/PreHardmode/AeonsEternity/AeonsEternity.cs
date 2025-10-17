using Avalon;
using Avalon.Common.Extensions;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.PreHardmode.MinersSword;
using Avalon.Items.Weapons.Melee.PreHardmode.OsmiumTierSwords;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.AeonsEternity;

public class AeonsEternity : ModItem
{
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 128);
	}
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<AeonStar>(), 40, 5, 8f, 81, 20);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
	}
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		ParticleSystem.AddParticle(new AeonStarburst(), Main.rand.NextVector2FromRectangle(target.Hitbox), Vector2.Zero, Color.Cyan, Main.rand.NextFloat(MathHelper.TwoPi), 1.5f);
	}
	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		ParticleSystem.AddParticle(new AeonStarburst(), Main.rand.NextVector2FromRectangle(target.Hitbox), Vector2.Zero, Color.Cyan, Main.rand.NextFloat(MathHelper.TwoPi), 1.5f);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int lastStar = -255;
		SoundEngine.PlaySound(SoundID.Item9, player.Center);
		for (int i = 0; i < Main.rand.Next(4, 8); i++)
		{
			Vector2 velRand = velocity.RotatedByRandom(Math.PI / 6) * Main.rand.NextFloat(0.3f, 2.4f);

			Vector2 dirToMouse = player.SafeDirectionTo(Main.MouseWorld);
			Vector2 velMult = player.velocity * new Vector2(MathF.Abs(dirToMouse.X), MathF.Abs(dirToMouse.Y)); // The player's current velocity, multiplied by the unsigned cosine & sine of the angle to the mouse

			Projectile p = Projectile.NewProjectileDirect(Item.GetSource_FromThis(), position, velRand + velMult * 0.8f + player.velocity * 0.2f, ModContent.ProjectileType<AeonStar>(), damage / 5, knockback, player.whoAmI, lastStar, 160 + i * 10, (float)Main.timeForVisualEffects);
			p.scale = Main.rand.NextFloat(0.9f, 1.1f);
			p.rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
			lastStar = p.whoAmI;
		}
		return false;
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{

		ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
		Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * player.direction * player.gravDir);
		int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<SimpleColorableGlowyDust>(), player.velocity.X * 0.2f + player.direction * 3, player.velocity.Y * 0.2f, 140, Color.Lerp(new Color(1f,1f,0.8f,0f), new Color(1f,0.7f,0.6f,0f),player.itemAnimation / (float)player.itemAnimationMax), 1.2f);
		Main.dust[num15].position = location2;
		Main.dust[num15].noGravity = true;
		Main.dust[num15].velocity *= 0.25f;
		Main.dust[num15].velocity += vector2 * 5f;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword.MinersSword>())
			.AddIngredient(ItemID.AntlionClaw)
			.AddIngredient(ModContent.ItemType<OsmiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();
		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword.MinersSword>())
			.AddIngredient(ModContent.ItemType<DesertLongsword.DesertLongsword>())
			.AddIngredient(ModContent.ItemType<OsmiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword.MinersSword>())
			.AddIngredient(ItemID.AntlionClaw)
			.AddIngredient(ModContent.ItemType<RhodiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();
		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword.MinersSword>())
			.AddIngredient(ModContent.ItemType<DesertLongsword.DesertLongsword>())
			.AddIngredient(ModContent.ItemType<RhodiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword.MinersSword>())
			.AddIngredient(ItemID.AntlionClaw)
			.AddIngredient(ModContent.ItemType<IridiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();
		CreateRecipe(1)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.IceBlade)
			.AddIngredient(ModContent.ItemType<MinersSword.MinersSword>())
			.AddIngredient(ModContent.ItemType<DesertLongsword.DesertLongsword>())
			.AddIngredient(ModContent.ItemType<IridiumGreatsword>())
			.AddTile(TileID.DemonAltar)
			.Register();
	}
}
public class AeonStar : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 24;
		Projectile.height = 24;
		Projectile.aiStyle = -1;
		Projectile.alpha = 0;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 25;
		Projectile.tileCollide = false;
		DrawOriginOffsetY = 2;
		DrawOffsetX = 4;
		Projectile.extraUpdates = 1;
	}
	Vector2 LastStarPos;
	public override void OnSpawn(IEntitySource source)
	{
		int J = Projectile.ai[0] != -255 ? Main.projectile[(int)Projectile.ai[0]].whoAmI : Projectile.whoAmI;
		LastStarPos = Main.projectile[J].Center;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Projectile.type];
		Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Value.Width, frameHeight);
		Vector2 frameOrigin = frame.Size() / 2f;
		Color color = Color.Lerp(new Color(255, 255, 255, 0), new Color(128, 128, 128, 64), Projectile.ai[1] * 0.03f);
		for (int i = 0; i < 6; i++)
		{
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.position + frameOrigin - Main.screenPosition + new Vector2(0, (float)Math.Sin(Main.GlobalTimeWrappedHourly * MathHelper.TwoPi / 12f) * 4).RotatedBy(i * MathHelper.PiOver2 + Main.timeForVisualEffects * 0.03f), frame, color * 0.2f, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None);
		}
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.position + frameOrigin - Main.screenPosition, frame, color, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None);
		return false;
	}
	public override void AI()
	{
		float Seed = Projectile.ai[2];
		Projectile lastStar = Projectile.ai[0] != -255 ? Main.projectile[(int)Projectile.ai[0]] : Projectile;
		float distanceToStar = Projectile.Center.Distance(lastStar.Center);
		if (!lastStar.active)
		{
			lastStar = Projectile;
		}
		Projectile.ai[1]--;
		if (Projectile.ai[1] == 100 && lastStar.ai[2] == Seed && lastStar.whoAmI != Projectile.whoAmI == true)
		{
			SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);
		}
		if (lastStar.ai[2] == Seed && lastStar.whoAmI != Projectile.whoAmI && Projectile.ai[1] < 100)
		{
			for (int i = 0; i < distanceToStar; i += 6)
			{
				int D = Dust.NewDust(Projectile.Center + new Vector2(i, 0).RotatedBy(Projectile.Center.AngleTo(lastStar.Center)), 0, 0, DustID.UnusedWhiteBluePurple, 0, 0, 0, default, 1);
				Main.dust[D].noGravity = true;
				Main.dust[D].velocity *= 0;
			}
		}
		Projectile.velocity *= 0.95f;
		Projectile.rotation += Projectile.velocity.Length() / 30;
		Projectile.rotation += 0.007f;


		if (Projectile.ai[1] > 30)
		{
			LastStarPos = lastStar.Center;
		}

		if (Projectile.ai[1] < 10)
		{
			int D = Dust.NewDust(Vector2.Lerp(Projectile.Center, LastStarPos, Projectile.ai[1] / 10), 0, 0, DustID.UnusedWhiteBluePurple, 0, 0, 0, default, 2);
			Main.dust[D].color = new Color(255, 255, 255, 0);
			Main.dust[D].noGravity = true;
			Main.dust[D].velocity *= 0;
			Main.dust[D].noLightEmittence = true;
		}
		if (Projectile.ai[1] < 0)
		{
			Projectile.Kill();
		}
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item110, Projectile.Center);

		for (int i = 0; i < 30; i++)
		{
			int D = Dust.NewDust(Projectile.Center, 0, 0, DustID.GoldCoin, 0, 0, 0, default, 3);
			Main.dust[D].color = new Color(255, 255, 255, 0);
			Main.dust[D].noGravity = true;
			Main.dust[D].noLightEmittence = true;
			Main.dust[D].fadeIn = Main.rand.NextFloat(0.5f, 1.5f);
			Main.dust[D].velocity = new Vector2(Main.rand.NextFloat(3, 8), 0).RotatedBy(MathHelper.Pi / 15 * i);
		}

		if (Main.myPlayer == Projectile.owner)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AeonExplosion>(), Projectile.damage * 5, Projectile.knockBack * 2, Projectile.owner);
		}

		Projectile lastStar = Projectile.ai[0] != -255 ? Main.projectile[(int)Projectile.ai[0]] : Projectile;
		if (!lastStar.active)
		{
			lastStar = Projectile;
		}
		ParticleSystem.AddParticle(new AeonStarburst(), Projectile.Center, Vector2.Zero, Color.Yellow, Projectile.rotation, 2);
		if (lastStar == Projectile)
		{
			ParticleSystem.AddParticle(new AeonStarburst(), Projectile.Center, Vector2.Zero, Color.Red, Projectile.rotation + MathHelper.Pi, 3);
		}
	}
}
public class AeonExplosion : ModProjectile
{
	public override string Texture => ModContent.GetInstance<AeonStar>().Texture;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(128);
		Projectile.aiStyle = -1;
		Projectile.alpha = 0;
		Projectile.penetrate = -1;
		Projectile.scale = 1f;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.friendly = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 21;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 1;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = target.Center.X <= Projectile.Center.X ? -1 : 1;
	}
}
