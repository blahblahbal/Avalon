using Avalon.ModSupport.MLL.Liquids;
using Microsoft.Xna.Framework;
using ModLiquidLib.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Projectiles;

public abstract class LiquidSpawningExplosionBase : ModProjectile
{
	public abstract int DustType();
	public abstract int LiquidType();
	public virtual void LiquidExplosiveKill(Projectile proj, bool isGrenade = false)
	{
		proj.Resize(22, 22);
		if (isGrenade)
		{
			SoundEngine.PlaySound(in SoundID.Item62, proj.position);
		}
		else
		{
			SoundEngine.PlaySound(in SoundID.Item14, proj.position);
		}
		Color color = Color.Transparent;
		int type = DustType();
		for (int i = 0; i < 30; i++)
		{
			Dust dust = Dust.NewDustDirect(proj.position, proj.width, proj.height, DustID.Smoke, 0f, 0f, 100, color, 1.5f);
			dust.velocity *= 1.4f;
		}
		for (int i = 0; i < 80; i++)
		{
			Dust dust = Dust.NewDustDirect(proj.position, proj.width, proj.height, type, 0f, 0f, 100, color, 2.2f);
			dust.velocity *= 7f;
			Dust dust2 = Dust.NewDustDirect(proj.position, proj.width, proj.height, type, 0f, 0f, 100, color, 1.3f);
			dust2.velocity *= 4f;
		}
		for (int i = 1; i <= 2; i++)
		{
			for (int x = -1; x <= 1; x += 2)
			{
				for (int y = -1; y <= 1; y += 2)
				{
					Gore gore = Gore.NewGoreDirect(new EntitySource_Death(proj), proj.position, Vector2.Zero, Main.rand.Next(61, 64));
					gore.velocity *= ((i == 1) ? 0.4f : 0.8f);
					gore.velocity += new Vector2(x, y);
				}
			}
		}
		if (Main.netMode != NetmodeID.MultiplayerClient)
		{
			Point projPos = proj.Center.ToTileCoordinates();
			proj.Kill_DirtAndFluidProjectiles_RunDelegateMethodPushUpForHalfBricks(projPos, 3f, SpreadLiquid);
		}
	}
	public virtual bool SpreadLiquid(int x, int y)
	{
		if (Vector2.Distance(DelegateMethods.v2_1, new Vector2(x, y)) > DelegateMethods.f_1)
			return false;

		if (LiquidType() == LiquidLoader.LiquidType<Acid>() ? PlaceLiquidAcidTempMethod(x, y, byte.MaxValue) : WorldGen.PlaceLiquid(x, y, (byte)LiquidType(), byte.MaxValue))
		{
			Vector2 position = new Vector2(x * 16, y * 16);
			int type = (int)DustType()!;
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 2.2f);
				dust.velocity.Y -= 1.2f;
				dust.velocity *= 7f;
				Dust dust2 = Dust.NewDustDirect(position, 16, 16, type, 0f, 0f, 100, Color.Transparent, 1.3f);
				dust2.velocity.Y -= 1.2f;
				dust2.velocity *= 4f;
			}
			return true;
		}
		return false;
	}
	public static bool PlaceLiquidAcidTempMethod(int x, int y, byte amount)
	{
		if (!WorldGen.InWorld(x, y))
			return false;

		Tile tile = Main.tile[x, y];
		if (tile == null)
		{
			return false;
		}

		if (tile.HasUnactuatedTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType])
		{
			return false;
		}

		tile.LiquidType = LiquidLoader.LiquidType<Acid>();
		if (amount + tile.LiquidAmount > 255)
		{
			amount = (byte)(255 - tile.LiquidAmount);
		}

		tile.LiquidAmount += amount;
		WorldGen.SquareTileFrame(x, y);
		if (Main.netMode != NetmodeID.SinglePlayer)
		{
			NetMessage.sendWater(x, y);
		}

		return true;
	}
}
