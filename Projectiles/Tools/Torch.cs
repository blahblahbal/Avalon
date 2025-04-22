using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Chat;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class Torch : ModProjectile
{
	int itemType = 0;
	public override void SetDefaults()
	{
		Projectile.width = 6;
		Projectile.height = 14;
		Projectile.aiStyle = 1;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.light = 1f;
		Projectile.damage = 0;
		Projectile.DamageType = DamageClass.Ranged;
	}
	public override void OnSpawn(IEntitySource source)
	{
		itemType = (int)Projectile.ai[2];
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		itemType = reader.ReadInt32();
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(itemType);
	}
	public override void AI()
	{
		Item item = new Item();
		item.SetDefaults(itemType);
		Main.NewText(itemType);
		if (item.type == ItemID.DemonTorch)
		{
			Lighting.AddLight(Projectile.Center, TorchID.Demon);
		}
		else if (item.type == ItemID.RainbowTorch)
		{
			Lighting.AddLight(Projectile.Center, TorchID.Rainbow);
		}
		else if (item.type == ItemID.ShimmerTorch)
		{
			Lighting.AddLight(Projectile.Center, TorchID.Shimmer);
		}
		else
		{
			Vector3 v = Data.Sets.ItemSets.TorchLauncherItemToProjColor[itemType];
			Main.NewText(v);
			Lighting.AddLight(Projectile.Center, v.X, v.Y, v.Z);
		}		
	}
	public override void OnKill(int timeLeft)
	{
		Item item = new Item();
		item.SetDefaults((int)Projectile.ai[2]);

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

		if (!Main.tile[TileX, TileY].HasTile || Main.tileCut[Main.tile[TileX, TileY].TileType] || (Main.tile[TileX, TileY].LiquidAmount > 0 && item.type != ItemID.CursedTorch))
		{
			WorldGen.PlaceTile(TileX, TileY, item.createTile, false, true, -1, item.placeStyle);
			WorldGen.TileFrame(TileX, TileY);
			Main.tile[TileX, TileY].TileFrameY = (short)(22 * item.placeStyle);
			Projectile.active = false;
		}
	}
}
