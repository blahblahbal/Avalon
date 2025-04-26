using Avalon.Common.Players;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

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
	public int itemType
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
		starTorchPos = Projectile.Owner().GetModPlayer<AvalonPlayer>().MousePosition;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D flameTex = ModContent.Request<Texture2D>("Avalon/Projectiles/Tools/Torch_Flame").Value;
		Texture2D shimmerFlameTex = ModContent.Request<Texture2D>("Avalon/Projectiles/Tools/Torch_Flame_Shimmer").Value;
		Texture2D stickTex = ModContent.Request<Texture2D>("Avalon/Projectiles/Tools/Torch_Stick").Value;

		Vector2 DrawPos = Projectile.position - Main.screenPosition + (Projectile.Size / 2f);
		Color flameColor = Color.White;
		Color stickColor = Color.White;
		if (Data.Sets.ItemSets.TorchLauncherFlameColors.TryGetValue(itemType, out Color dictFlameColor))
		{
			flameColor = dictFlameColor;
			if (itemType == ItemID.RainbowTorch)
			{
				flameColor = Main.DiscoColor;
			}
		}
		if (Data.Sets.ItemSets.TorchLauncherStickColors.TryGetValue(itemType, out Color dictStickColor))
		{
			stickColor = dictStickColor;
		}
		Main.EntitySpriteDraw(stickTex, DrawPos, new Rectangle(0, 0, stickTex.Width, stickTex.Height), stickColor, Projectile.rotation, new Vector2(stickTex.Width, stickTex.Height) / 2, 1f, SpriteEffects.None);

		Main.EntitySpriteDraw(itemType == ItemID.ShimmerTorch ? shimmerFlameTex : flameTex, DrawPos, new Rectangle(0, 0, flameTex.Width, flameTex.Height), flameColor, Projectile.rotation, new Vector2(flameTex.Width, flameTex.Height) / 2, 1f, SpriteEffects.None);

		var randSeed = Main.TileFrameSeed ^ (ulong)((long)DrawPos.Y << 32 | (long)(ulong)DrawPos.X);
		for (var k = 0; k < 7; k++)
		{
			var x = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
			var y = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
			Vector2 FlamePos = DrawPos + new Vector2(x, y).RotatedBy(Projectile.rotation);
			Main.EntitySpriteDraw(itemType == ItemID.ShimmerTorch ? shimmerFlameTex : flameTex, FlamePos, new Rectangle(0, 0, flameTex.Width, flameTex.Height), new Color(flameColor.R, flameColor.G, flameColor.B, 80), Projectile.rotation, new Vector2(flameTex.Width, flameTex.Height) / 2, 1f, SpriteEffects.None);
		}
		return false;
	}
	public override void AI()
	{
		if (Projectile.ai[0] == 0)
		{
			// set rotation to 0 on the first frame
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			Projectile.ai[0]++;
		}
		if (itemType == ModContent.ItemType<Items.Placeable.Furniture.StarTorch>())
		{
			Projectile.velocity = Vector2.Normalize(starTorchPos - Projectile.Owner().Center) * 8f;
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
				Projectile.ai[0] = 1;
			}
		}
		Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.velocity.ToRotation() + MathHelper.PiOver2, 0.05f);

		if (itemType == ItemID.DemonTorch)
		{
			Lighting.AddLight(Projectile.Center, TorchID.Demon);
		}
		else if (itemType == ItemID.RainbowTorch)
		{
			Lighting.AddLight(Projectile.Center, TorchID.Rainbow);
		}
		else if (itemType == ItemID.ShimmerTorch)
		{
			Lighting.AddLight(Projectile.Center, TorchID.Shimmer);
		}
		else
		{
			if (Data.Sets.ItemSets.TorchLauncherItemToProjColor.TryGetValue(itemType, out Vector3 lightColor))
			{
				Lighting.AddLight(Projectile.Center, lightColor.X, lightColor.Y, lightColor.Z);
			}
		}
		if (Data.Sets.ItemSets.TorchLauncherDust.TryGetValue(itemType, out int dustType))
		{
			if (dustType > -1)
			{
				int posOffset = 2;
				int dust = Dust.NewDust(new Vector2(Projectile.position.X - posOffset, Projectile.position.Y - posOffset) + new Vector2(0, -5f).RotatedBy(Projectile.rotation), 4 + posOffset, 4 + posOffset, dustType, 0f, 0f, 100, default, Main.rand.NextFloat(1.25f, 1.6f));
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
					ParticleSystem.AddParticle(new StarTorch(),
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
	private bool CheckTiles(int x, int y)
	{
		for (int i = x - 1; i <= x + 1; i++)
		{
			for (int j = y - 1; j <= y + 1; j++)
			{

			}
		}
		return false;
	}
	public override void OnKill(int timeLeft)
	{
		Item item = ContentSamples.ItemsByType[itemType];

		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		int TileX = Projectile.Center.ToTileCoordinates().X;
		int TileY = Projectile.Center.ToTileCoordinates().Y;

		if (TileX < 0 || TileX >= Main.maxTilesX || TileY < 0 || TileY >= Main.maxTilesY)
		{
			Projectile.active = false;
			return;
		}

		if ((TileX < 1 || Main.tileNoAttach[Main.tile[TileX - 1, TileY].TileType]) && (TileX >= Main.maxTilesX - 1 || Main.tileNoAttach[Main.tile[TileX + 1, TileY].TileType]) && (TileY < 1 || Main.tileNoAttach[Main.tile[TileX, TileY - 1].TileType]) && (TileY >= Main.maxTilesY - 1 || Main.tileNoAttach[Main.tile[TileX, TileY + 1].TileType]))
		{
			// create dropped torch item
			Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, 8, 8, item.type);
			Projectile.active = false;
			return;
		}

		if (Main.tile[TileX, TileY].WallType != 0 && !Main.tile[TileX, TileY].HasTile)
		{
			goto placeOnWall;
		}
		else if (((Main.tile[TileX - 1, TileY + 1].HasTile && !Main.tile[TileX, TileY + 1].HasTile && !Main.tile[TileX - 1, TileY].HasTile) || // leftdown active, down off, left off
			(Main.tile[TileX + 1, TileY + 1].HasTile && !Main.tile[TileX, TileY + 1].HasTile && !Main.tile[TileX + 1, TileY].HasTile) || // rightdown active, down off, right off
			(Main.tile[TileX - 1, TileY - 1].HasTile && !Main.tile[TileX, TileY - 1].HasTile && !Main.tile[TileX - 1, TileY].HasTile) || // leftup active, up off, left off
			(Main.tile[TileX + 1, TileY - 1].HasTile && !Main.tile[TileX, TileY - 1].HasTile && !Main.tile[TileX + 1, TileY].HasTile) || // rightup active, up off, right off
			(Main.tile[TileX, TileY].HasTile && !Main.tileSolid[Main.tile[TileX, TileY].TileType]) ||                                    // current tile non-solid
																																		 // down on and non-solid, left OR right on and non-solid
			(Main.tile[TileX, TileY + 1].HasTile && !Main.tileSolid[Main.tile[TileX, TileY + 1].TileType] && ((Main.tile[TileX - 1, TileY].HasTile && !Main.tileSolid[Main.tile[TileX - 1, TileY].TileType]) || (Main.tile[TileX + 1, TileY].HasTile && !Main.tileSolid[Main.tile[TileX + 1, TileY].TileType]))) ||
			// (up on, ((left off OR right off) OR (current tile active and non-solid))
			(Main.tile[TileX, TileY - 1].HasTile && ((!Main.tile[TileX - 1, TileY].HasTile || !Main.tile[TileX + 1, TileY].HasTile) || (Main.tile[TileX, TileY].HasTile && !Main.tileSolid[Main.tile[TileX, TileY].TileType])))))
		{
			// create dropped torch item
			Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, 8, 8, item.type);
			Projectile.active = false;
			return;
		}
		else if (Main.tile[TileX, TileY].IsHalfBlock || Main.tile[TileX, TileY].Slope != SlopeType.Solid || (Main.tile[TileX, TileY + 1].Slope != SlopeType.Solid && !Main.tile[TileX, TileY].HasTile))
		{
			Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, 8, 8, item.type);
			Projectile.active = false;
			return;
		}

	placeOnWall:
		if (!Main.tile[TileX, TileY].HasTile || Main.tileCut[Main.tile[TileX, TileY].TileType] || (Main.tile[TileX, TileY].LiquidAmount > 0 && item.type != ItemID.CursedTorch))
		{
			if (itemType != ItemID.None)
			{
				WorldGen.PlaceTile(TileX, TileY, item.createTile, false, true, -1, item.placeStyle);
				if (Main.netMode != NetmodeID.SinglePlayer)
				{
					NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, TileX, TileY, item.createTile);
				}
				if (Main.tile[TileX, TileY].TileType == item.createTile)
				{
					WorldGen.TileFrame(TileX, TileY);
					Main.tile[TileX, TileY].TileFrameY = (short)(22 * item.placeStyle);
					Projectile.active = false;
				}
			}
		}
	}
}
