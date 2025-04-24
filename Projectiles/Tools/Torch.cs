using Avalon.Common.Players;
using Avalon.Items.Placeable.Furniture;
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
		Projectile.height = 14;
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

		Vector2 DrawPos = Projectile.Center - Main.screenPosition;
		Color flameColor = Color.White;
		Color stickColor = Color.White;
		if (Data.Sets.ItemSets.TorchLauncherFlameColors.ContainsKey(itemType))
		{
			flameColor = Data.Sets.ItemSets.TorchLauncherFlameColors[itemType];
			stickColor = Data.Sets.ItemSets.TorchLauncherStickColors[itemType];
			if (itemType == ItemID.RainbowTorch)
			{
				flameColor = Main.DiscoColor;
			}
		}
		Main.EntitySpriteDraw(itemType == ItemID.ShimmerTorch ? shimmerFlameTex : flameTex, DrawPos, new Rectangle(0, 0, flameTex.Width, flameTex.Height), flameColor, Projectile.rotation, new Vector2(flameTex.Width, flameTex.Height) / 2, 1f, SpriteEffects.None);
		Main.EntitySpriteDraw(stickTex, DrawPos, new Rectangle(0, 0, stickTex.Width, stickTex.Height), stickColor, Projectile.rotation, new Vector2(stickTex.Width, stickTex.Height) / 2, 1f, SpriteEffects.None);
		return false;
	}
	public override void AI()
	{
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
			if (Projectile.ai[0] > 15)
			{
				Projectile.velocity.Y += 0.8f;
				Projectile.ai[0] = 0;
			}
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

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
			if (Data.Sets.ItemSets.TorchLauncherItemToProjColor.ContainsKey(itemType))
			{
				Vector3 v = Data.Sets.ItemSets.TorchLauncherItemToProjColor[itemType];
				Lighting.AddLight(Projectile.Center, v.X, v.Y, v.Z);
			}
		}
		if (Data.Sets.ItemSets.TorchLauncherDust.ContainsKey(itemType))
		{
			if (Data.Sets.ItemSets.TorchLauncherDust[itemType] > -1)
			{
				Dust D = Dust.NewDustDirect(Projectile.Center, 8, 8, Data.Sets.ItemSets.TorchLauncherDust[itemType], 0f, 0f, Scale: 0.75f);
				D.noGravity = true;
			}
			else if (Data.Sets.ItemSets.TorchLauncherDust[itemType] == -2)
			{
				if (counter > 0) counter--;
				if (Main.rand.NextBool(2) && Main.hasFocus && counter < 25)
				{
					counter += 26;
					ParticleSystem.AddParticle(new Particles.StarTorch(),
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

		if ((Main.tile[TileX - 1, TileY + 1].HasTile && !Main.tile[TileX, TileY + 1].HasTile && !Main.tile[TileX - 1, TileY].HasTile) || // leftdown active, down off, left off
			(Main.tile[TileX + 1, TileY + 1].HasTile && !Main.tile[TileX, TileY + 1].HasTile && !Main.tile[TileX + 1, TileY].HasTile) || // rightdown active, down off, right off
			(Main.tile[TileX - 1, TileY - 1].HasTile && !Main.tile[TileX, TileY - 1].HasTile && !Main.tile[TileX - 1, TileY].HasTile) || // leftup active, up off, left off
			(Main.tile[TileX + 1, TileY - 1].HasTile && !Main.tile[TileX, TileY - 1].HasTile && !Main.tile[TileX + 1, TileY].HasTile) || // rightup active, up off, right off
			(Main.tile[TileX, TileY].HasTile && !Main.tileSolid[Main.tile[TileX, TileY].TileType]) ||									 // current tile non-solid
			// (up on, ((left off OR right off) OR (current tile active and non-solid))
			(Main.tile[TileX, TileY - 1].HasTile && ((!Main.tile[TileX - 1, TileY].HasTile || !Main.tile[TileX + 1, TileY].HasTile) || (Main.tile[TileX, TileY].HasTile && !Main.tileSolid[Main.tile[TileX, TileY].TileType]))))
		{
			// create dropped torch item
			Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, 8, 8, item.type);
			Projectile.active = false;
			return;
		}

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
