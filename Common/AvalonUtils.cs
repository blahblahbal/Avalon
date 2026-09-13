using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ObjectData;

namespace Avalon.Common;
public static class AvalonUtils
{
	public static void DrawConfusionIcon(this NPC npc, Vector2 screenPos, float heightOffset = 0f)
	{
		if (npc.confused)
		{
			float num36 = Main.NPCAddHeight(npc);
			Vector2 halfSize = new(TextureAssets.Npc[npc.type].Width() / 2, TextureAssets.Npc[npc.type].Height() / Main.npcFrameCount[npc.type] / 2);
			Main.spriteBatch.Draw(TextureAssets.Confuse.Value, new Vector2(npc.position.X - screenPos.X + (float)(npc.width / 2) - (float)TextureAssets.Npc[npc.type].Width() * npc.scale / 2f + halfSize.X * npc.scale, npc.position.Y - screenPos.Y + (float)npc.height - (float)TextureAssets.Npc[npc.type].Height() * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + halfSize.Y * npc.scale + num36 + heightOffset - (float)TextureAssets.Confuse.Height() - 20f), new Rectangle(0, 0, TextureAssets.Confuse.Width(), TextureAssets.Confuse.Height()), npc.GetShimmerColor(new Color(250, 250, 250, 70)), npc.velocity.X * -0.05f, new Vector2(TextureAssets.Confuse.Width() / 2, TextureAssets.Confuse.Height() / 2), Main.essScale + 0.2f, SpriteEffects.None, 0f);
		}
	}
	public static bool SolidCollisionWithFunctionalTopSurfaceDetection(Vector2 Position, int Width, int Height, bool acceptTopSurfaces)
	{
		int value = (int)(Position.X / 16f) - 1;
		int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
		int value3 = (int)(Position.Y / 16f) - 1;
		int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
		int num = Utils.Clamp(value, 0, Main.maxTilesX - 1);
		value2 = Utils.Clamp(value2, 0, Main.maxTilesX - 1);
		value3 = Utils.Clamp(value3, 0, Main.maxTilesY - 1);
		value4 = Utils.Clamp(value4, 0, Main.maxTilesY - 1);
		Vector2 vector = default(Vector2);
		for (int i = num; i < value2; i++)
		{
			for (int j = value3; j < value4; j++)
			{
				Tile tile = Main.tile[i, j];
				if (tile == null || !tile.active() || tile.inActive())
				{
					continue;
				}

				bool flag = Main.tileSolid[tile.type] && !Main.tileSolidTop[tile.type];
				if (acceptTopSurfaces && Main.tileSolidTop[tile.type])
				{
					TileObjectData tileObjectData = TileObjectData.GetTileData(tile);
					if (tileObjectData != null && tileObjectData.Height != 1)
					{
						flag |= tile.TileFrameY == 0;
					}
					else
					{
						flag = true;
					}
				}

				if (flag)
				{
					vector.X = i * 16;
					vector.Y = j * 16;
					int num2 = 16;
					if (tile.halfBrick())
					{
						vector.Y += 8f;
						num2 -= 8;
					}

					if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num2)
					{
						return true;
					}
				}
			}
		}

		return false;
	}
	public static Vector2 FindVelocityForGravityAffectedThing(Vector2 StartPosition, Vector2 TargetPosition, float gravity, int TimeUntilHit)
	{
		return new Vector2(
			(TargetPosition.X - StartPosition.X) / TimeUntilHit,
			((TargetPosition.Y - StartPosition.Y) / TimeUntilHit) - (gravity / 2f * TimeUntilHit)
			);
	}
	/// <summary>
	/// Finds the closest floor below a point in the world. If it can't find anything it'll return the lowest spot it could check.
	/// </summary>
	public static Vector2 FindFloorBelowIgnoringSolidTops(Vector2 pointPoisition, int maxCheckTileDistance)
	{
		int num = (int)pointPoisition.X / 16;
		int num2 = (int)pointPoisition.Y / 16;
		for (int i = 0; i < maxCheckTileDistance; i++)
		{
			if ((Main.tileSolid[Main.tile[num, num2 + i].TileType] && !Main.tileSolidTop[Main.tile[num, num2 + i].TileType] && Main.tile[num, num2 + i].HasTile && !Main.tile[num, num2 + i].IsActuated) || !WorldGen.InWorld(num, num2 + i, 5))
			{
				return new Vector2(num * 16 + 8, (num2 + i) * 16);
			}
		}
		return new Vector2(num * 16 + 8, (num2 + maxCheckTileDistance) * 16);
	}
	public static Vector2 FindFloorBelow(Vector2 pointPoisition, int maxCheckTileDistance)
	{
		int num = (int)pointPoisition.X / 16;
		int num2 = (int)pointPoisition.Y / 16;
		for (int i = 0; i < maxCheckTileDistance; i++)
		{
			if ((Main.tileSolid[Main.tile[num, num2 + i].TileType] && Main.tile[num, num2 + i].HasTile && !Main.tile[num, num2 + i].IsActuated) || !WorldGen.InWorld(num, num2 + i, 5))
			{
				return new Vector2(num * 16 + 8, (num2 + i) * 16);
			}
		}
		return new Vector2(num * 16 + 8, (num2 + maxCheckTileDistance) * 16);
	}

	/// <param name="velocity"></param>
	/// <param name="position"></param>
	/// <param name="baseSpeed"></param>
	/// <param name="rotation"></param>
	/// <param name="maxRotUnsigned"></param>
	/// <param name="ammoExtraShootSpeed"></param>
	/// <param name="random"></param>
	/// <returns>
	/// <paramref name="velocity"/> rotated by the specified <paramref name="rotation"/>, adjusted based on the magnitude of <paramref name="velocity"/> in comparison to the provided <paramref name="baseSpeed"/> + <paramref name="ammoExtraShootSpeed"/>.
	/// </returns>
	public static Vector2 GetShootSpread(Vector2 velocity, Vector2 position, float baseSpeed, double rotation, float addMagnitude = 0, float ammoExtraShootSpeed = 0, bool random = false, double maxRotUnsigned = Math.PI / 4)
	{
		if (velocity == Vector2.Zero || velocity.Length() + addMagnitude <= 0) return Vector2.Zero;

		Vector2 rotatedBy(Vector2 input, double angle) => random ? input.RotatedByRandom(angle) : input.RotatedBy(angle);

		baseSpeed += ammoExtraShootSpeed;

		Vector2 m = Main.MouseWorld - position;
		float radius = velocity.Length();
		float baseRadius = baseSpeed;
		float distModBase = baseRadius / m.Length();
		Vector2 velBase = m * distModBase;
		Vector2 velBaseRotated = velBase.RotatedBy(rotation);

		// intersect stuff
		float velBaseRotatedDistSquared = Vector2.DistanceSquared(velBase, velBaseRotated);
		float a = (velBaseRotatedDistSquared - MathF.Pow(radius, 2f) + MathF.Pow(-radius, 2f)) / (2f * -radius); // dunno what to name this
		float h = MathF.Sqrt(velBaseRotatedDistSquared - MathF.Pow(a, 2f)); // dunno what to name this either
		float x = radius + a;
		float y = -h;

		if (float.IsNaN(x) || float.IsNaN(y)) // the intersection can be NaN if baseSpeed is sufficiently larger than the velocity
		{
			return rotatedBy(velocity + (Vector2.Normalize(velocity) * addMagnitude), maxRotUnsigned * -Math.Sign(rotation));
		}

		double newAngle = Math.Atan2(y, x) * -Math.Sign(rotation);
		Vector2 velFinal = rotatedBy(velocity + (Vector2.Normalize(velocity) * addMagnitude), (newAngle > 0 ? Math.Min(newAngle, maxRotUnsigned) : Math.Max(newAngle, -maxRotUnsigned)));
		return velFinal;
	}
	/// <summary>
	/// <see cref="float"/> <paramref name="baseSpeed"/> = <see cref="ContentSamples.ItemsByType"/>[<paramref name="baseSpeedItemID"/>].shootSpeed<br></br>
	/// <see cref="float"/> <paramref name="ammoExtraShootSpeed"/> = <paramref name="ammoExtraShootSpeedItemID"/> > 0 ? <see cref="ContentSamples.ItemsByType"/>[<paramref name="ammoExtraShootSpeedItemID"/>].shootSpeed : 0
	/// </summary>
	/// <returns>
	/// <inheritdoc cref="GetShootSpread(Vector2, Vector2, float, double, float, float, bool, double)"/><para></para>
	/// </returns>
	public static Vector2 GetShootSpread(Vector2 velocity, Vector2 position, int baseSpeedItemID, double rotation, float addMagnitude = 0, int ammoExtraShootSpeedItemID = 0, bool random = false, double maxRotUnsigned = Math.PI / 4)
	{
		return GetShootSpread(velocity, position, ContentSamples.ItemsByType[baseSpeedItemID].shootSpeed, rotation, addMagnitude, ammoExtraShootSpeedItemID > 0 ? ContentSamples.ItemsByType[ammoExtraShootSpeedItemID].shootSpeed : 0, random, maxRotUnsigned);
	}
	public static void NewTextRainbow(object o)
	{
		Main.NewText(o, Main.DiscoColor);
	}
}
