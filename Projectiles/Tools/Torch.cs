using Avalon.Particles;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class Torch : ModProjectile
{
	byte counter = 0;
	public override void SetDefaults()
	{
		Projectile.width = 6;
		Projectile.height = 14;
		Projectile.aiStyle = 1;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.light = 1f;
		Projectile.damage = 0;
		AIType = ProjectileID.WoodenArrowFriendly;
		Projectile.DamageType = DamageClass.Ranged;
	}
	public int itemType
	{
		get => (int)Projectile.ai[2];
		set => Projectile.ai[2] = value;
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		counter = reader.ReadByte();
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(counter);
	}
	public override void AI()
	{
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
			Vector3 v = Data.Sets.ItemSets.TorchLauncherItemToProjColor[itemType];
			Lighting.AddLight(Projectile.Center, v.X, v.Y, v.Z);
		}
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

		if ((Main.tile[TileX - 1, TileY + 1].HasTile && !Main.tile[TileX, TileY + 1].HasTile && !Main.tile[TileX - 1, TileY].HasTile) ||
			(Main.tile[TileX + 1, TileY + 1].HasTile && !Main.tile[TileX, TileY + 1].HasTile && !Main.tile[TileX + 1, TileY].HasTile) ||
			(Main.tile[TileX - 1, TileY - 1].HasTile && !Main.tile[TileX, TileY - 1].HasTile && !Main.tile[TileX - 1, TileY].HasTile) ||
			(Main.tile[TileX + 1, TileY - 1].HasTile && !Main.tile[TileX, TileY - 1].HasTile && !Main.tile[TileX + 1, TileY].HasTile) ||
			(Main.tile[TileX, TileY].HasTile && !Main.tileSolid[Main.tile[TileX, TileY].TileType]) ||
			(Main.tile[TileX, TileY - 1].HasTile && ((!Main.tile[TileX - 1, TileY].HasTile && !Main.tile[TileX + 1, TileY].HasTile) || (Main.tile[TileX, TileY].HasTile && !Main.tileSolid[Main.tile[TileX, TileY].TileType]))))
		{
			// create dropped torch item
			Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, 8, 8, item.type);
			Projectile.active = false;
			return;
		}

		if (!Main.tile[TileX, TileY].HasTile || Main.tileCut[Main.tile[TileX, TileY].TileType] || (Main.tile[TileX, TileY].LiquidAmount > 0 && item.type != ItemID.CursedTorch))
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
