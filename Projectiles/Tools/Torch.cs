using Avalon.Particles;
using Avalon.Particles.OldParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Projectiles.Tools;

public class Torch : ModProjectile
{
	byte counter = 0;
	Vector2 starTorchPos = Vector2.Zero;
	public override void SetDefaults()
	{
		Projectile.width = 6;
		Projectile.height = 6;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.damage = 0;
		Projectile.DamageType = DamageClass.Generic;
	}
	public int ItemType
	{
		get => (int)Projectile.ai[2];
		set => Projectile.ai[2] = value;
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		counter = reader.ReadByte();
		starTorchPos = reader.ReadVector2();
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(counter);
		writer.WriteVector2(starTorchPos);
	}
	public override void OnSpawn(IEntitySource source)
	{
		starTorchPos = Main.MouseWorld;
	}
	public override bool PreDraw(ref Color lightColor)
	{

		Texture2D TEX = ModContent.Request<Texture2D>("Avalon/Projectiles/Tools/Torch").Value;
		Texture2D FlameTEX = ModContent.Request<Texture2D>("Avalon/Projectiles/Tools/Torch").Value;
		Rectangle frame = new Rectangle();
		Rectangle flameFrame = new Rectangle();

		if (Data.Sets.TorchLauncherSets.Texture.TryGetValue(ItemType, out string TexturePath))
		{
			Item torch = ContentSamples.ItemsByType[ItemType];
			if (ItemType <= ItemID.ShimmerTorch)
			{
				TEX = TextureAssets.Tile[TileID.Torches].Value;
				frame = new Rectangle(0, 22 * torch.placeStyle, TileObjectData.GetTileData(TileID.Torches, torch.placeStyle).CoordinateWidth, TileObjectData.GetTileData(TileID.Torches, torch.placeStyle).CoordinateHeights[0]);
			}
			else
			{
				TEX = ModContent.Request<Texture2D>(TexturePath).Value;
				frame = new Rectangle(0, 0, TileObjectData.GetTileData(TileID.Torches, 0).CoordinateWidth, TileObjectData.GetTileData(TileID.Torches, 0).CoordinateHeights[0]);
			}
		}

		if (Data.Sets.TorchLauncherSets.FlameTexture.TryGetValue(ItemType, out string FlameTexturePath))
		{
			Item torch = ContentSamples.ItemsByType[ItemType];
			if (ItemType <= ItemID.ShimmerTorch)
			{
				FlameTEX = TextureAssets.Flames[0].Value;
				flameFrame = new Rectangle(0, 22 * torch.placeStyle, TileObjectData.GetTileData(TileID.Torches, torch.placeStyle).CoordinateWidth, TileObjectData.GetTileData(TileID.Torches, torch.placeStyle).CoordinateHeights[0]);
			}
			else
			{
				FlameTEX = ModContent.Request<Texture2D>(FlameTexturePath).Value;
				flameFrame = new Rectangle(0, 0, TileObjectData.GetTileData(TileID.Torches, 0).CoordinateWidth, TileObjectData.GetTileData(TileID.Torches, 0).CoordinateHeights[0]);
			}
		}

		Vector2 DrawPos = Projectile.position - Main.screenPosition + (Projectile.Size / 2f);
		Color flameColor = Color.White;
		if (ItemType == ItemID.RainbowTorch)
		{
			flameColor = Main.DiscoColor;
		}
		Main.EntitySpriteDraw(TEX, DrawPos, frame, Color.White, Projectile.rotation, new Vector2(22, 22) / 2, 1f, SpriteEffects.None);

		var randSeed = Main.TileFrameSeed ^ (ulong)((long)DrawPos.Y << 32 | (long)(ulong)DrawPos.X);
		for (var k = 0; k < 7; k++)
		{
			var x = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
			var y = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
			Vector2 FlamePos = DrawPos + new Vector2(x, y).RotatedBy(Projectile.rotation);
			Main.EntitySpriteDraw(FlameTEX, FlamePos, flameFrame, new Color(flameColor.R, flameColor.G, flameColor.B, 0) * 0.39215687f /* magic number, the same as remapping 0-255 to 0-100 */, Projectile.rotation, new Vector2(22, 22) / 2, 1f, SpriteEffects.None);
		}
		return false;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Data.Sets.TorchLauncherSets.DebuffType.TryGetValue(ItemType, out int buffType))
		{
			if (buffType == -1)
			{
				buffType = BuffID.OnFire;
			}
			target.AddBuff(buffType, 60 * 3);
		}
	}
	public override void AI()
	{
		if (Projectile.ai[0] == 0)
		{
			// set rotation based on direction on the first frame
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			Projectile.ai[0]++;
		}
		if (ItemType == ModContent.ItemType<Items.Placeable.Furniture.StarTorch>())
		{
			Projectile.velocity = Vector2.Normalize(starTorchPos - Projectile.Center) * Projectile.velocity.Length();
			if (Vector2.Distance(starTorchPos, Projectile.Center) < 16)
			{
				Projectile.Kill();
			}
		}
		else
		{
			Projectile.ai[0]++;
			if (Projectile.ai[0] > 16)
			{
				Projectile.velocity.Y += 0.8f;
				Projectile.ai[0] = 1; // set to 1 instead of 0 because ai[0] == 0 is used to set the initial rotation on the first frame
			}
		}
		Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.velocity.ToRotation() + MathHelper.PiOver2, 0.05f);

		if (ItemType == ItemID.DemonTorch)
		{
			Lighting.AddLight(Projectile.Center, TorchID.Demon);
		}
		else if (ItemType == ItemID.RainbowTorch)
		{
			Lighting.AddLight(Projectile.Center, TorchID.Rainbow);
		}
		else if (ItemType == ItemID.ShimmerTorch)
		{
			Lighting.AddLight(Projectile.Center, TorchID.Shimmer);
		}
		else
		{
			if (Data.Sets.TorchLauncherSets.LightColor.TryGetValue(ItemType, out Vector3 lightColor))
			{
				Lighting.AddLight(Projectile.Center, lightColor.X, lightColor.Y, lightColor.Z);
			}
		}
		if (Data.Sets.TorchLauncherSets.Dust.TryGetValue(ItemType, out int dustType))
		{
			if (dustType > -1)
			{
				Color color = default;
				if (ItemType == ItemID.RainbowTorch)
				{
					color = Main.DiscoColor;
				}
				int posOffset = 2;
				int dust = Dust.NewDust(new Vector2(Projectile.position.X - posOffset, Projectile.position.Y - posOffset) + new Vector2(0, -5f).RotatedBy(Projectile.rotation), 4 + posOffset, 4 + posOffset, dustType, 0f, 0f, 100, color, Main.rand.NextFloat(1.25f, 1.6f));
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 0.1f;
				Main.dust[dust].velocity += Projectile.velocity * 0.1f;
				//Dust D = Dust.NewDustDirect(Projectile.Center, 8, 8, dustType, 0f, 0f, Scale: 0.75f);
				//D.noGravity = true;
			}
			else if (dustType == -2)
			{
				if (Main.rand.NextBool(15) && Main.hasFocus)
				{
					OldParticleSystemDeleteSoon.AddParticle(new StarTorch(),
						Projectile.Center + new Vector2(Main.rand.Next(4, 13), Main.rand.Next(2, 6)), // position
						new Vector2(Main.rand.NextFloat(-0.02f, 0.03f), Main.rand.NextFloat(-0.4f, -0.5f)), // velocity
						Color.White, // color
						default,
						Main.rand.NextFromList(Main.rand.NextFloat(-0.25f, -0.15f), Main.rand.NextFloat(0.15f, 0.25f)),
						scale: Main.rand.NextFloat(0.11f, 0.17f)); // scale
				}
			}
		}
	}
	public override void OnKill(int timeLeft)
	{
		Item item = ContentSamples.ItemsByType[ItemType];

		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		int TileX = Projectile.Center.ToTileCoordinates().X;
		int TileY = Projectile.Center.ToTileCoordinates().Y;

		if (TileX < 0 || TileX >= Main.maxTilesX || TileY < 0 || TileY >= Main.maxTilesY)
		{
			return;
		}

		if (!WorldGen.PlaceObject(TileX, TileY, item.createTile, false, item.placeStyle, 0))
		{
			Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, 8, 8, item.type);
		}
	}
}
