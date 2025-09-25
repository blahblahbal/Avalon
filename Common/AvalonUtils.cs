using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace Avalon.Common;
public static class AvalonUtils
{
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
}
