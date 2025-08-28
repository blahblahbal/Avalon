using Avalon.Common;
using Avalon.Common.Players;
using Avalon.ModSupport.MLL.Liquids;
using Microsoft.Xna.Framework;
using ModLiquidLib.ModLoader;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class CollisionHooks : ModHook
{
	protected override void Apply()
	{
		On_Collision.StickyTiles += On_Collision_StickyTiles;
	}

	/// <summary>
	/// Preferably collision logic would be done in one of the xLiquidMovement overrides in <see cref="Acid"/>, leaving this in case that somehow doesn't work.
	/// </summary>
	/// <param name="Position"></param>
	/// <param name="Width"></param>
	/// <param name="Height"></param>
	/// <returns></returns>
	public static bool AcidCollision(Vector2 Position, int Width, int Height)
	{
		int value = (int)(Position.X / 16f) - 1;
		int value2 = (int)((Position.X + Width) / 16f) + 2;
		int value3 = (int)(Position.Y / 16f) - 1;
		int value4 = (int)((Position.Y + Height) / 16f) + 2;
		int num = Utils.Clamp(value, 0, Main.maxTilesX - 1);
		value2 = Utils.Clamp(value2, 0, Main.maxTilesX - 1);
		value3 = Utils.Clamp(value3, 0, Main.maxTilesY - 1);
		value4 = Utils.Clamp(value4, 0, Main.maxTilesY - 1);
		Vector2 vector = default;
		for (int i = num; i < value2; i++)
		{
			for (int j = value3; j < value4; j++)
			{
				if (Main.tile[i, j] != null && Main.tile[i, j].LiquidAmount > 0 && Main.tile[i, j].LiquidType == LiquidLoader.LiquidType<Acid>())
				{
					vector.X = i * 16;
					vector.Y = j * 16;
					int num2 = 16;
					float num3 = 256 - Main.tile[i, j].LiquidAmount;
					num3 /= 32f;
					vector.Y += num3 * 2f;
					num2 -= (int)(num3 * 2f);
					if (Position.X + Width > vector.X && Position.X < vector.X + 16f && Position.Y + Height > vector.Y && Position.Y < vector.Y + num2)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public static Vector2 BrambleTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
	{
		Vector2 vector = Position;
		int num = (int)(Position.X / 16f) - 1;
		int num2 = (int)((Position.X + Width) / 16f) + 2;
		int num3 = (int)(Position.Y / 16f) - 1;
		int num4 = (int)((Position.Y + Height) / 16f) + 2;
		if (num < 0)
		{
			num = 0;
		}
		if (num2 > Main.maxTilesX)
		{
			num2 = Main.maxTilesX;
		}
		if (num3 < 0)
		{
			num3 = 0;
		}
		if (num4 > Main.maxTilesY)
		{
			num4 = Main.maxTilesY;
		}
		Vector2 vector2 = default;
		for (int i = num; i < num2; i++)
		{
			for (int j = num3; j < num4; j++)
			{
				if (Main.tile[i, j].TileType == ModContent.TileType<Tiles.Savanna.Bramble>())
				{
					int num5 = 0;
					vector2.X = i * 16;
					vector2.Y = j * 16;
					if (vector.X + Width > vector2.X - num5 && vector.X < vector2.X + 16f + num5 && vector.Y + Height > vector2.Y && vector.Y < vector2.Y + 16.01)
					{
						if (Main.tile[i, j].TileType == ModContent.TileType<Tiles.Savanna.Bramble>() && (double)(Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)) > 0.7 && Main.rand.NextBool(30))
							Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.CorruptGibs);
						return new Vector2(i, j);
					}
				}
			}
		}
		return new Vector2(-1, -1);
	}

	private Vector2 On_Collision_StickyTiles(On_Collision.orig_StickyTiles orig, Vector2 Position, Vector2 Velocity, int Width, int Height)
	{
		if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().NoSticky)
		{
			return new Vector2(-1f, -1f);
		}
		return orig.Invoke(Position, Velocity, Width, Height);
	}
}
