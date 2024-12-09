using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Avalon.Common.Players;
using Avalon.Items.Accessories.Hardmode;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.ID;
using Terraria.Localization;

namespace Avalon.Projectiles;

public abstract class FishingRiftAbstract : ModProjectile
{
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
		Projectile.aiStyle = -1;
		Projectile.hide = true;
		Projectile.ignoreWater = true;
		Projectile.netImportant = true;
	}
	public int initialTimeLeft
	{
		get => (int)Projectile.ai[1];
		set => Projectile.ai[1] = value;
	}
	public int timeLeft
	{
		get => (int)Projectile.ai[2];
		set => Projectile.ai[2] = value;
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
			float dampeningX = 8f;
			float dampeningY = 12f;
			float scaleX = 1f + (1f / dampeningX) - ((1f + MathF.Sin((float)Main.timeForVisualEffects / 20f)) / dampeningX);
			float scaleY = 1f + (1f / dampeningY * 1.75f) - ((1f + MathF.Cos((float)Main.timeForVisualEffects / 20f)) / dampeningY);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height), color, 0, TextureAssets.Projectile[Projectile.type].Value.Size() / 2, new Vector2(scaleX, scaleY), SpriteEffects.None);
			Lighting.AddLight(Projectile.Center, 40 / 255f, 0, 90 / 255f);
		}
		return false;
	}
	public override void AI()
	{
		Projectile.timeLeft = timeLeft;
		timeLeft--;

		Point pos = (Projectile.Center / 16).ToPoint();
		if (!WorldGen.InWorld(pos.X, pos.Y, 30))
		{
			Projectile.Kill();
		}
		//Main.NewText($"initial: {initialTimeLeft} _ position: {Projectile.position}");
	}
}
public class FishingRiftFront : FishingRiftAbstract
{
	//public override void OnSpawn(IEntitySource source)
	//{
	//	Projectile.timeLeft = (int)Projectile.ai[1];
	//}
	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overWiresUI.Add(index);
	}

	//-1 on other clients, but it's only relevant for owner
	public int RiftWhoAmI
	{
		get => (int)Projectile.localAI[0] - 1;
		set => Projectile.localAI[0] = value + 1;
	}
	public override void AI()
	{
		base.AI();

		//Spawns child
		if (RiftWhoAmI <= -1 && Projectile.owner == Main.myPlayer)
		{
			var source = Projectile.GetSource_FromThis();
			RiftWhoAmI = Projectile.NewProjectile(source, Projectile.position, Vector2.Zero, ModContent.ProjectileType<FishingRiftBack>(), default, default, Projectile.owner, ai1: initialTimeLeft, ai2: timeLeft);
		}

		//Just makes sure if the child disappears, this one does too
		if (Projectile.owner == Main.myPlayer)
		{
			bool kill = true;
			if (RiftWhoAmI > -1 && RiftWhoAmI <= Main.maxProjectiles)
			{
				Projectile riftBack = Main.projectile[RiftWhoAmI];
				if (riftBack.active && riftBack.owner == Projectile.owner && riftBack.type == ModContent.ProjectileType<FishingRiftBack>())
				{
					kill = false;
				}
			}

			if (kill)
			{
				Projectile.Kill();
				return;
			}
		}
	}
}
public class FishingRiftBack : FishingRiftAbstract
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}
	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}
	public int ParentIdentity
	{
		get => (int)Projectile.ai[0] - 1;
		set => Projectile.ai[0] = value + 1;
	}
	//Since the index might be different between clients, using ai[] for it will break stuff
	public int ParentIndex
	{
		get => (int)Projectile.localAI[1] - 1;
		set => Projectile.localAI[1] = value + 1;
	}
	public override void OnSpawn(IEntitySource source)
	{
		if (source is not EntitySource_Parent parentSource)
		{
			return;
		}

		if (parentSource.Entity is not Projectile parent)
		{
			return;
		}

		ParentIdentity = parent.identity;
	}
	public override void AI()
	{
		base.AI();

		if (ParentIdentity <= -1 || ParentIdentity > Main.maxProjectiles)
		{
			Projectile.Kill();
			return;
		}

		Projectile parent = null;
		if (ParentIndex <= -1)
		{
			//Find parent based on identity
			Projectile test = NetGetProjectile(Projectile.owner, ParentIdentity, ModContent.ProjectileType<FishingRiftFront>(), out int index);
			if (test != null)
			{
				//Important not to use test.whoAmI here
				ParentIndex = index;
			}
		}

		if (ParentIndex > -1 && ParentIndex <= Main.maxProjectiles)
		{
			parent = Main.projectile[ParentIndex];
		}

		if (parent == null)
		{
			//If the parent was not found, despawn
			Projectile.Kill();
			return;
		}

		parent = Main.projectile[ParentIndex];
		if (!parent.active || parent.type != ModContent.ProjectileType<FishingRiftFront>())
		{
			Projectile.Kill();
			return;
		}

		Projectile.position = parent.position;
	}
	/// <summary>
	/// Gets a projectile given its owner ID, its identity ID, and its type ID.
	/// </summary>
	/// <param name="owner">The owner to check</param>
	/// <param name="identity">The identity to check</param>
	/// <param name="type">The type to check</param>
	/// <param name="index">The index ("whoAmI") of the found projectile. Main.maxProjectiles if returning null.</param>
	/// <returns>Returns null if not found.</returns>
	public static Projectile NetGetProjectile(int owner, int identity, int type, out int index)
	{
		for (short i = 0; i < Main.maxProjectiles; i++)
		{
			Projectile proj = Main.projectile[i];
			if (proj.active && proj.owner == owner && proj.identity == identity && proj.type == type)
			{
				index = i;
				return proj;
			}
		}
		index = Main.maxProjectiles;
		return null;
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
		bool spawn = false;
		List<Point> spawnPos = new List<Point>();
		//if (Main.rand.NextBool(20))
		if (false)
		{
			if (Player.whoAmI == Main.myPlayer && Player.GetModPlayer<AvalonPlayer>().RiftGoggles && Player.ownedProjectileCounts[ModContent.ProjectileType<FishingRiftFront>()] < 1)
			{
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
					int frontProj = Projectile.NewProjectile(Player.GetSource_Accessory(new Item(ModContent.ItemType<RiftGoggles>())), finalSpawnPos.ToVector2() * 16 + new Vector2(8, offsetY), Vector2.Zero, ModContent.ProjectileType<FishingRiftFront>(), default, default, Main.myPlayer, ai1: timeLeft, ai2: timeLeft);
					Main.projectile[frontProj].timeLeft = timeLeft;
					Main.projectile[frontProj].netUpdate = true;
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, frontProj);
					}
				}
			}
		}
	}
}
