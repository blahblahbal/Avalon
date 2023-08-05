using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using Terraria;

namespace Avalon.Common;

public class HellcastleFogSystem
{
	private UnifiedRandom _random = new UnifiedRandom();

	private List<Point> _spotsForAirboneWind = new List<Point>();

	private int _updatesCounter;

	public void Update()
	{
		if (!Main.LocalPlayer.InModBiome<Biomes.NearHellcastle>() && !Main.LocalPlayer.InModBiome<Biomes.Hellcastle>())
		{
			return;
		}
		_updatesCounter++;
		Rectangle tileWorkSpace = GetTileWorkSpace();
		int num = tileWorkSpace.X + tileWorkSpace.Width;
		int num2 = tileWorkSpace.Y + tileWorkSpace.Height;
		for (int i = tileWorkSpace.X; i < num; i++)
		{
			for (int j = tileWorkSpace.Y; j < num2; j++)
			{
				TrySpawningWind(i, j);
			}
		}
		if (_updatesCounter % 30 == 0)
		{
			SpawnAirborneWind();
		}
	}

	private void SpawnAirborneWind()
	{
		foreach (Point item in _spotsForAirboneWind)
		{
			SpawnAirborneCloud(item.X, item.Y);
		}
		_spotsForAirboneWind.Clear();
	}

	private Rectangle GetTileWorkSpace()
	{
		Point point = Main.LocalPlayer.Center.ToTileCoordinates();
		int num = 120;
		int num2 = 30;
		return new Rectangle(point.X - num / 2, point.Y - num2 / 2, num, num2);
	}

	private void TrySpawningWind(int x, int y)
	{
		if (!WorldGen.InWorld(x, y, 10) || Main.tile[x, y] == null)
		{
			return;
		}
		TestAirCloud(x, y);
		Tile tile = Main.tile[x, y];
		if (!tile.HasTile || tile.Slope > 0 || tile.IsHalfBlock || !Main.tileSolid[tile.TileType])
		{
			return;
		}
		tile = Main.tile[x, y - 1];
		if (!WorldGen.SolidTile(tile) && _random.NextBool(120))
		{
			SpawnFloorCloud(x, y);
			if (_random.NextBool(3))
			{
				SpawnFloorCloud(x, y - 1);
			}
		}
	}

	private void SpawnAirborneCloud(int x, int y)
	{
		int num = _random.Next(2, 6);
		float num2 = 1.1f;
		float num3 = 2.2f;
		float num4 = 0.0235619452f * _random.NextFloatDirection();
		float num5 = 0.0235619452f * _random.NextFloatDirection();
		while (num5 > -0.0117809726f && num5 < 0.0117809726f)
		{
			num5 = 0.0235619452f * _random.NextFloatDirection();
		}
		if (_random.NextBool(4))
		{
			num = _random.Next(9, 16);
			num2 = 1.1f;
			num3 = 1.2f;
		}
		else if (_random.NextBool(4))
		{
			num = _random.Next(9, 16);
			num2 = 1.1f;
			num3 = 0.2f;
		}
        Vector2 vector = new(-10, 0);
		Vector2 vector2 = Utils.ToWorldCoordinates(new Point(x, y), 8f, 8f);
		num4 -= num5 * (float)num * 0.5f;
		float num6 = num4;
		for (int i = 0; i < num; i++)
		{
			if (Main.rand.Next(10) == 0)
			{
				num5 *= _random.NextFloatDirection();
			}
			Vector2 vector3 = _random.NextVector2Circular(4f, 4f);
			int type = 1091 + _random.Next(2) * 2;
			float num7 = 1.4f;
			float num8 = num2 + _random.NextFloat() * num3;
			float num9 = num6 + num5;
			Vector2 vector4 = Vector2.UnitX.RotatedBy(num9) * num7;
			Gore.NewGorePerfect(Main.LocalPlayer.GetSource_FromThis(), vector2 + vector3 - vector, vector4 * Main.WindForVisuals, type, num8);
			vector2 += vector4 * 6.5f * num8;
			num6 = num9;
		}
	}

	private void SpawnFloorCloud(int x, int y)
	{
		Vector2 position = Utils.ToWorldCoordinates(new Point(x, y - 1), 8f, 8f);
		int type = _random.Next(1087, 1090);
		float num = 16f * _random.NextFloat();
		position.Y -= num;
		if (num < 4f)
		{
			type = 1090;
		}
		float num2 = 0.4f;
		float scale = 0.8f + _random.NextFloat() * 0.2f;
		Gore.NewGorePerfect(Main.LocalPlayer.GetSource_FromThis(), position, Vector2.UnitX * num2 * Main.WindForVisuals, type, scale);
	}

	private void TestAirCloud(int x, int y)
	{
		if (!_random.NextBool(120000))
		{
			return;
		}
		for (int i = -2; i <= 2; i++)
		{
			if (i != 0)
			{
				Tile t = Main.tile[x + i, y];
				if (!DoesTileAllowWind(t))
				{
					return;
				}
				t = Main.tile[x, y + i];
				if (!DoesTileAllowWind(t))
				{
					return;
				}
			}
		}
		_spotsForAirboneWind.Add(new Point(x, y));
	}

	private bool DoesTileAllowWind(Tile t)
	{
		if (t.HasTile)
		{
			return !Main.tileSolid[t.TileType];
		}
		return true;
	}
}
