using Avalon;
using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.SoulEdge;

public class SoulEdgePlayer : ModPlayer
{
	public const int maxSoulEdge = 5000;
	public override void ResetEffects()
	{
		if (SoulEdgeDamage > maxSoulEdge)
			SoulEdgeDamage = maxSoulEdge;
	}
	public int SoulEdgeDamage = 0;
}
public class SoulEdgeLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.HeldItem);
	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		return drawInfo.drawPlayer.HeldItem.type == ModContent.ItemType<SoulEdge>() && drawInfo.drawPlayer.ItemAnimationActive;
	}
	private static Asset<Texture2D>? texture;
	public override void Load()
	{
		texture = ModContent.Request<Texture2D>(ModContent.GetInstance<SoulEdge>().Texture + "Glow");
	}
	private void drawSword(ref PlayerDrawSet drawInfo, Color color, int frame)
	{
		Vector2 basePosition = drawInfo.drawPlayer.itemLocation - Main.screenPosition;
		basePosition = new Vector2((int)basePosition.X, (int)basePosition.Y) + (drawInfo.drawPlayer.RotatedRelativePoint(drawInfo.drawPlayer.Center) - drawInfo.drawPlayer.Center);
		Item heldItem = drawInfo.drawPlayer.HeldItem;

		DrawData swingDraw = new DrawData(
		texture.Value, // texture
		basePosition, // position
		new Rectangle(0, texture.Height() / 3 * frame, texture.Width(), texture.Height() / 3), // texture coords
		color, // color (wow really!?)
		drawInfo.drawPlayer.itemRotation,  // rotation
		new Vector2(drawInfo.drawPlayer.direction == -1 ? texture.Value.Width : 0, // origin X
		drawInfo.drawPlayer.gravDir == 1 ? texture.Value.Height / 3 : 0), // origin Y
		drawInfo.drawPlayer.GetAdjustedItemScale(heldItem), // scale
		drawInfo.itemEffect // sprite effects
		);

		drawInfo.DrawDataCache.Add(swingDraw);
	}
	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.shadow != 0 || drawInfo.drawPlayer.ownedProjectileCounts[ModContent.ProjectileType<SoulEdgeSlash>()] == 0)
			return;

		drawSword(ref drawInfo, Color.White, 1);
		drawSword(ref drawInfo, Color.White * (drawInfo.drawPlayer.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage / (float)SoulEdgePlayer.maxSoulEdge), 2);
	}
}
public class SoulEdge : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<SoulEdgeSlash>(), 292, 6.5f, 1f, 20, 20, shootsEveryUse: true, noMelee: true, width: 56, height: 62);
		Item.noUseGraphic = true;
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 30);
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		int DustType = DustID.SpectreStaff;
		if (Main.rand.NextBool())
			DustType = Main.rand.NextFloat() > player.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage / (float)SoulEdgePlayer.maxSoulEdge ? DustID.DungeonSpirit : ModContent.DustType<PhantoplasmDust>();

		for (int j = 0; j < 2; j++)
		{
			ClassExtensions.GetPointOnSwungItemPath(60f, 120f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
			Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * player.direction * player.gravDir);
			Dust d = Dust.NewDustPerfect(location2, DustType, vector2 * 2f, 100, default, 0.7f + Main.rand.NextFloat() * 1.3f);
			d.noGravity = true;
			d.velocity *= 2f;
			if (Main.rand.NextBool(20))
			{
				int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType, player.velocity.X * 0.2f + player.direction * 3, player.velocity.Y * 0.2f, 128, default, 1.3f);
				Main.dust[num15].position = location2;
				Main.dust[num15].fadeIn = 1.2f;
				Main.dust[num15].noGravity = true;
				Main.dust[num15].velocity *= 2f;
				Main.dust[num15].velocity += vector2 * 5f;
			}
		}
	}
	public override bool AltFunctionUse(Player player)
	{
		return player.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage == SoulEdgePlayer.maxSoulEdge;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.altFunctionUse == 2)
		{
			SoundEngine.PlaySound(SoundID.Zombie53, player.position);
			Projectile.NewProjectile(source, position, velocity * 8, ModContent.ProjectileType<SoulEdgeDash>(), damage * 3, knockback * 2, player.whoAmI);
			return false;
		}

		float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem);
		Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale5 * 2f);
		NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
		return false;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
public class SoulEdgeSlash : EnergySlashTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<SoulEdge>().DisplayName;
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TheHorsemansBlade}";
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.penetrate = 3;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		float percent = 1f - Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage / (float)SoulEdgePlayer.maxSoulEdge;
		float multiply = 0.8f;
		DrawSlash(
			Color.Lerp(new Color(244, 19, 0, 0), new Color(0, 140, 244, 0), percent) * multiply,
			Color.Lerp(new Color(255, 140, 163, 0), new Color(88, 219, 255, 0), percent) * multiply,
			Color.Lerp(new Color(255, 223, 240, 0), new Color(237, 171, 255, 0), percent) * multiply,
			new Color(1f, 1f, 1f, 0f), 0, 1f, 0f, -0.1f, -0.2f, true, true);
		return false;
	}
	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		int DustType = DustID.SpectreStaff;
		float percent = (player.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage / (float)SoulEdgePlayer.maxSoulEdge);
		if (Main.rand.NextBool())
			DustType = (Main.rand.NextFloat() > percent ? DustID.DungeonSpirit : ModContent.DustType<PhantoplasmDust>());

		for (int j = 0; j < 2; j++)
		{
			ClassExtensions.GetPointOnSwungItemPath(60f, 120f, 0.2f + 0.8f * Main.rand.NextFloat(), player.HeldItem.scale, out var location2, out var outwardDirection2, player);
			Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
			Dust d = Dust.NewDustPerfect(location2, DustType, vector2 * 2f, 100, default(Color), 0.7f + Main.rand.NextFloat() * 1.3f);
			d.noGravity = true;
			d.velocity *= 2f;
			if (Main.rand.NextBool(20))
			{
				int num15 = Dust.NewDust(location2, 0, 0, DustType, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 128, default(Color), 1.3f);
				Main.dust[num15].fadeIn = 1.2f;
				Main.dust[num15].noGravity = true;
				Main.dust[num15].velocity *= 2f;
				Main.dust[num15].velocity += vector2 * 5f;
			}
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write7BitEncodedInt(Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage = reader.Read7BitEncodedInt();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage += damageDone;
		Projectile.netUpdate = true;

		if (Main.player[Projectile.owner].GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage >= SoulEdgePlayer.maxSoulEdge)
		{
			for (int i = 0; i < 35; i++)
			{
				Dust d = Dust.NewDustDirect(target.Hitbox.ClosestPointInRect(Projectile.Center) - Projectile.Size / 2f, Projectile.width, Projectile.height, ModContent.DustType<PhantoplasmDust>());
				d.noGravity = true;
				d.velocity *= 2;
				d.scale *= 2;
			}
		}
	}
}
public class SoulEdgeDash : ModProjectile
{
	private const int initialTimeLeft = 20;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(64);
		Projectile.DamageType = DamageClass.Melee;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = initialTimeLeft;
		Projectile.penetrate = -1;
		Projectile.netImportant = true;
	}
	public override void AI()
	{
		Projectile.netUpdate = true;

		Player player = Main.player[Projectile.owner];
		if (Projectile.timeLeft == initialTimeLeft)
		{
			player.immune = true;
			player.AddImmuneTime(ImmunityCooldownID.General, 60);
			player.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage = 0;
		}
		player.velocity = Projectile.velocity * new Vector2(4, 3);
		player.heldProj = Projectile.whoAmI;
		player.GetModPlayer<AvalonPlayer>().TurnOffDownwardsMovementRestrictions = true;
		player.SetDummyItemTime(2);

		Projectile.velocity *= 0.99f;

		if (Projectile.timeLeft == 1)
		{
			Projectile.velocity *= 0.3f;
			player.velocity *= 0.3f;
		}

		if (Main.myPlayer == Projectile.owner && Projectile.timeLeft % 5 == 0)
		{
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, (Projectile.velocity * 0.3f).RotatedBy(MathHelper.PiOver2 + (MathHelper.Pi * i)), ModContent.ProjectileType<SoulEaterFriendly>(), Projectile.damage / 9, Projectile.knockBack / 9, Projectile.owner);
			}
		}

		for (int i = 0; i < Projectile.timeLeft / 5; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<PhantoplasmDust>());
			d.velocity += Projectile.velocity;
			d.noGravity = Main.rand.NextBool();
		}

		Projectile.Center = player.Center + new Vector2(0, player.gfxOffY) + Vector2.Normalize(Projectile.velocity) * 15 * MathF.Sin(Projectile.timeLeft / (float)initialTimeLeft * MathHelper.Pi) - Vector2.Normalize(Projectile.velocity) * 10;
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
		player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * player.gravDir + (player.gravDir == -1 ? MathHelper.Pi : 0));
	}
	public override bool PreDraw(ref Color lightColor)
	{
		float percent = Projectile.timeLeft / (float)initialTimeLeft;
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(0, TextureAssets.Projectile[Type].Height()), Projectile.scale, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition - Projectile.velocity * 3 * percent, null, Color.Red * 0.2f * percent, Projectile.rotation, new Vector2(0, TextureAssets.Projectile[Type].Height()), Projectile.scale + percent, SpriteEffects.None, 0);
		for (int i = 0; i < 2; i++)
		{
			Main.EntitySpriteDraw(TextureAssets.Extra[ExtrasID.SharpTears].Value, Projectile.Center - Main.screenPosition + Vector2.Normalize(Projectile.velocity) * 110, null, new Color(1f, 0.25f, 0.25f, 0f) * 0.8f * percent, MathHelper.PiOver2 * i + Projectile.rotation + MathHelper.PiOver4, TextureAssets.Extra[ExtrasID.SharpTears].Size() / 2, new Vector2(Projectile.scale, (Projectile.scale + 1 - i) * (0.7f + percent)) * 1.2f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(TextureAssets.Extra[ExtrasID.SharpTears].Value, Projectile.Center - Main.screenPosition + Vector2.Normalize(Projectile.velocity) * 110, null, new Color(1f, 1f, 1f, 0f) * 0.4f * percent * percent, MathHelper.PiOver2 * i + Projectile.rotation + MathHelper.PiOver4, TextureAssets.Extra[ExtrasID.SharpTears].Size() / 2, new Vector2(Projectile.scale, (Projectile.scale + 1 - i) * (0.5f + percent)), SpriteEffects.None, 0);
		}
		return false;
	}
}
public class SoulEaterFriendly : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 8;
		ProjectileID.Sets.TrailCacheLength[Type] = 8;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	private const int initialTimeLeft = 3600;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(16);
		Projectile.DamageType = DamageClass.Melee;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.alpha = 64;
		Projectile.scale = 0.75f;
		Projectile.tileCollide = false;
		Projectile.timeLeft = initialTimeLeft;
	}
	public override void AI()
	{
		Projectile.ai[1]++;
		if (Projectile.ai[1] == 1)
		{
			Projectile.ai[0] = Projectile.FindClosestNPC(600, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
		}
		if (Projectile.ai[1] is > 30 and < 60 && Projectile.ai[0] != -1)
		{
			Projectile.velocity = Vector2.Lerp(Main.npc[(int)Projectile.ai[0]].Center.DirectionFrom(Projectile.Center) * 32, Projectile.velocity, 0.98f);
		}
		if (Projectile.velocity.Length() < 8 && Projectile.ai[1] > 60)
		{
			Projectile.velocity *= 1.1f;
			Projectile.velocity.LengthClamp(16);
		}

		Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
		Projectile.frameCounter++;
		Projectile.frame = (Projectile.frameCounter / 7) % 4;
	}
	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 15; i++)
		{
			int DustType = Main.rand.NextFloat() < Utils.Remap(initialTimeLeft - timeLeft, 0, 50, 0, 1, true) ? DustID.DungeonSpirit : ModContent.DustType<PhantoplasmDust>();
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustType);
			d.noGravity = true;
			d.velocity *= 3;
		}
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Projectile.type];
		Rectangle sourceRectangle = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Width(), frameHeight);
		Vector2 frameOrigin = sourceRectangle.Size() / 2f;

		Vector2 drawPos = Projectile.Center;

		float colorFade = Utils.Remap(initialTimeLeft - Projectile.timeLeft, 0, 60, 0, 1, true);
		Color color = new(255, 255, 255, 225);
		Color colorTrail = new(255, 125, 255, 225);
		Color colorTrailFromRed = colorTrail * (1f - colorFade);
		for (int i = 0; i < 1 + (int)MathF.Ceiling(1f - colorFade); i++)
		{
			if (i == 1)
			{
				color *= 1f - colorFade;
				colorTrail = colorTrailFromRed;
				sourceRectangle.Y += Main.projFrames[Type] * frameHeight / 2;
			}
			else
			{
				colorTrail *= colorFade;
			}
			for (int j = 0; j < Projectile.oldPos.Length; j++)
			{
				Vector2 drawPosOld = Projectile.oldPos[j] + (Projectile.Size / 2);
				Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPosOld - Main.screenPosition, sourceRectangle, colorTrail * (1 - (j / 8f)) * 0.2f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos - Main.screenPosition, sourceRectangle, color * 0.3f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.1f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos - Main.screenPosition, sourceRectangle, color * 0.15f * Projectile.Opacity, Projectile.rotation, frameOrigin, Projectile.scale * 1.2f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos - Main.screenPosition, sourceRectangle, color * Projectile.Opacity, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
		}
		return false;
	}
}

