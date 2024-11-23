using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;
using Avalon.Common.Players;
using Avalon.Particles;
using Avalon.Items.Accessories.Hardmode;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.ID;
using ReLogic.Content;

namespace Avalon.Projectiles;

public abstract class FishingRiftAbstract : ModProjectile
{
	public static Asset<Texture2D> textureHalo;
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 1;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.width = dims.Width;
		Projectile.height = dims.Height / Main.projFrames[Projectile.type];
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		//Projectile.timeLeft = Main.rand.Next(60 * 10, 60 * 50);
		//Projectile.alpha = 20;
		Projectile.aiStyle = -1;
		//DrawOriginOffsetX = Projectile.width / 2;
		//DrawOriginOffsetY = Projectile.height / 2;
		Projectile.hide = true;
		Projectile.ignoreWater = true;
	}
	public int initialTimeLeft;
	//public override void OnSpawn(IEntitySource source)
	//{
	//	Projectile.timeLeft = Main.rand.Next(60 * 60, 120 * 60);
	//}
	public override void OnSpawn(IEntitySource source)
	{
		Projectile.timeLeft = (int)Projectile.ai[0];
		initialTimeLeft = Projectile.timeLeft;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Player player = Main.LocalPlayer;
		if (player.GetModPlayer<AvalonPlayer>().RiftGoggles)
		{
			Color color = Color.White;
			if (initialTimeLeft - Projectile.timeLeft < 60)
			{
				color *= 1f - MathF.Pow(1.3f, -10 * (initialTimeLeft - Projectile.timeLeft) / 60f);
			}
			else if (Projectile.timeLeft <= 60)
			{
				color *= 1f - MathF.Pow(1.3f, -10 * Projectile.timeLeft / 60f);
			}
			//if (Projectile.type == ModContent.ProjectileType<FishingRiftBack>())
			//{
			//	Main.EntitySpriteDraw(textureHalo.Value, Projectile.Center - Main.screenPosition - new Vector2(0, 20), new Rectangle(0, 0, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height), color, 0, TextureAssets.Projectile[Projectile.type].Value.Size() / 2, 1, SpriteEffects.None);
			//}
			float dampeningX = 8f;
			float dampeningY = 12f;
			float scaleX = 1f + (1f / dampeningX) - ((1f + MathF.Sin((float)Main.timeForVisualEffects / 20f)) / dampeningX);
			float scaleY = 1f + (1f / dampeningY * 1.75f) - ((1f + MathF.Cos((float)Main.timeForVisualEffects / 20f)) / dampeningY);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height), color, 0, TextureAssets.Projectile[Projectile.type].Value.Size() / 2, new Vector2(scaleX, scaleY), SpriteEffects.None);
			Lighting.AddLight(Projectile.Center, 40 / 255f, 0, 90 / 255f);
		}
		return false;
	}
	public override void PostDraw(Color lightColor)
	{
	}
	public override void AI()
	{
		Point pos = (Projectile.Center / 16).ToPoint();
		if (!WorldGen.InWorld(pos.X, pos.Y, 30))
		{
			Projectile.Kill();
		}
	}
}
public class FishingRiftFront : FishingRiftAbstract
{
	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overWiresUI.Add(index);
	}
	private (byte L, byte M, byte R) liquidAmountOld;
	public override void OnSpawn(IEntitySource source)
	{
		base.OnSpawn(source);
		Point pos = ((Projectile.position + new Vector2(23, 4)) / 16).ToPoint();
		Tile tile = Main.tile[pos];
		Tile tileLeft = Main.tile[pos + new Point(-1, 0)];
		Tile tileRight = Main.tile[pos + new Point(1, 0)];
		liquidAmountOld = (tileLeft.LiquidAmount, tile.LiquidAmount, tileRight.LiquidAmount);
	}
	public override void AI()
	{
		base.AI();

		// too lazy rn to make it able to move downwards while the liquid is draining, so it just despawns if the liquid changes rn

		// if any of these have tiles, then set timeleft to 60
		// needs to have active checks for liquid level rising or falling
		// don't just immediately despawn if the top water layer suddenly becomes 0; check first if it can move down by one tile
		// some checks too for if there is liquid or tiles or a combo above the rift too

		Point pos = ((Projectile.position + new Vector2(23, 4)) / 16).ToPoint();
		Tile tile = Main.tile[pos];
		Tile tileUp = Main.tile[pos + new Point(0, -1)];
		Tile tileLeftUp = Main.tile[pos + new Point(0, -1)];
		Tile tileRightUp = Main.tile[pos + new Point(0, -1)];
		Tile tileLeft = Main.tile[pos + new Point(-1, 0)];
		Tile tileRight = Main.tile[pos + new Point(1, 0)];
		Tile tileDown = Main.tile[pos + new Point(0, 1)];
		Tile tileLeftDown = Main.tile[pos + new Point(-1, 1)];
		Tile tileRightDown = Main.tile[pos + new Point(1, 1)];

		List<Tile> intersectingTiles = [tile, tileLeft, tileRight, tileDown, tileLeftDown, tileRightDown];
		List<Tile> aboveTiles = [tileUp, tileLeftUp, tileRightUp];
		//List<Tile> topTiles = [tile, tileLeft, tileRight];
		//List<Tile> lowerTiles = [tileDown, tileLeftDown, tileRightDown];
		//Main.NewText(liquidAmountOld);
		if (Projectile.timeLeft > 60 && Projectile.timeLeft < initialTimeLeft - 60)
		{
			if (tileUp.HasTile)
			{
				Projectile.timeLeft = 60;
			}
			if (tileLeft.LiquidAmount / 16 != liquidAmountOld.L / 16 || tile.LiquidAmount / 16 != liquidAmountOld.M / 16 || tileRight.LiquidAmount / 16 != liquidAmountOld.R / 16) Projectile.timeLeft = 60;
			foreach (var t in intersectingTiles)
			{
				if (t.HasTile || !(t.LiquidAmount > 0) ) Projectile.timeLeft = 60;
			}
			foreach (var t in aboveTiles)
			{
				if (t.LiquidAmount > 0) Projectile.timeLeft = 60;
			}
		}
		//Main.NewText(tile.LiquidAmount);
		//Dust.QuickDust(pos, Color.Red);
	}
}
public class FishingRiftBack : FishingRiftAbstract
{
	private int parentRift;
	public override void SetStaticDefaults()
	{
		//textureHalo = ModContent.Request<Texture2D>(Texture + "_Halo");
		base.SetStaticDefaults();
	}
	public override void OnSpawn(IEntitySource source)
	{
		parentRift = (int)Projectile.ai[1];
		base.OnSpawn(source);
	}
	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}
	public override void AI()
	{
		base.AI();
		var p = Main.projectile[parentRift];
		if (!p.active || p.type != ModContent.ProjectileType<FishingRiftFront>())
		{
			Projectile.Kill();
		}
		else
		{
			Projectile.position = p.position;
		}
	}
}
public class FishingRiftGlobalProj : GlobalProjectile
{
	public const float MaxDistance = 50f;
	public override bool PreAI(Projectile projectile)
	{
		if (projectile.aiStyle == ProjAIStyleID.Bobber)
		{
			foreach (var riftProj in Main.ActiveProjectiles)
			{
				if (riftProj.type != ModContent.ProjectileType<FishingRiftFront>())
				{
					continue;
				}
				else
				{
					Vector2 projVisualCenter = projectile.Center + new Vector2(projectile.width / 2, 0);
					if (projVisualCenter.Distance(riftProj.Center) < MaxDistance)
					{
						if (projVisualCenter != riftProj.Center)
						{
							projectile.velocity.X += Math.Clamp(projVisualCenter.DirectionTo(riftProj.Center).X * (((MaxDistance + 10f) - projVisualCenter.Distance(riftProj.Center)) / 200f), -1f, 1f);
						}
					}
				}
			}
		}
		return base.PreAI(projectile);
	}
}
public class FishingRiftSpawning : ModPlayer
{
	public override void PostUpdate()
	{
		if (Player.whoAmI == Main.myPlayer && Player.GetModPlayer<AvalonPlayer>().RiftGoggles && Player.ownedProjectileCounts[ModContent.ProjectileType<FishingRiftFront>()] < 1)
		{
			//if (Main.rand.NextBool(20))
			if (false)
			{
				bool spawn = false;
				List<Point> spawnPos = new List<Point>();
				for (int i = 0; i < 51; i++)
				{
					for (int j = 0; j < 51; j++)
					{
						Point pos = (Player.Center / 16).ToPoint() + new Point(-25 + i, -25 + j);
						if (!WorldGen.InWorld(pos.X, pos.Y, 30)) continue;

						Tile tile = Main.tile[pos];
						Tile tileUp = Main.tile[pos + new Point(0, -1)];
						Tile tileLeft = Main.tile[pos + new Point(-1, 0)];
						Tile tileRight = Main.tile[pos + new Point(1, 0)];
						Tile tileDown = Main.tile[pos + new Point(0, 1)];
						Tile tileLeftDown = Main.tile[pos + new Point(-1, 1)];
						Tile tileRightDown = Main.tile[pos + new Point(1, 1)];
						if (tile.LiquidAmount < 1 || tile.HasTile ||
							tileUp.HasTile || !(tileUp.LiquidAmount < 1) ||
							tileLeft.LiquidAmount < 1 || tileLeft.HasTile ||
							tileRight.LiquidAmount < 1 || tileRight.HasTile ||
							tileDown.LiquidAmount < 1 || tileDown.HasTile ||
							tileLeftDown.LiquidAmount < 1 || tileLeftDown.HasTile ||
							tileRightDown.LiquidAmount < 1 || tileRightDown.HasTile) continue;
						spawn = true;
						spawnPos.Add(pos);
					}
				}
				if (spawn)
				{
					Point finalSpawnPos = Main.rand.NextFromCollection(spawnPos);
					float offsetY = Math.Clamp(256 - (Main.tile[finalSpawnPos].LiquidAmount + 1), 0, 256 - 64) / 16f + 12f;
					//Dust.QuickDust(finalSpawnPos.ToVector2() * 16, Color.Red);
					int timeLeft = Main.rand.Next(60 * 10, 60 * 50);
					var frontProj = Projectile.NewProjectile(Player.GetSource_Accessory(new Item(ModContent.ItemType<RiftGoggles>())), finalSpawnPos.ToVector2() * 16 + new Vector2(8, offsetY), Vector2.Zero, ModContent.ProjectileType<FishingRiftFront>(), default, default, Main.myPlayer, timeLeft);
					Projectile.NewProjectile(Player.GetSource_Accessory(new Item(ModContent.ItemType<RiftGoggles>())), finalSpawnPos.ToVector2() * 16 + new Vector2(8, offsetY), Vector2.Zero, ModContent.ProjectileType<FishingRiftBack>(), default, default, Main.myPlayer, timeLeft, frontProj);
				}
			}
		}
	}
}
