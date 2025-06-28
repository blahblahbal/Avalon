using System;

namespace Avalon.Common;
/// <summary>
/// <see href="https://easings.net/"/>
/// </summary>
public class Easings
{
	public static float SineIn(float x)
	{
		return 1f - MathF.Cos((x * MathF.PI) / 2f);
	}
	public static float SineOut(float x)
	{
		return MathF.Sin((x * MathF.PI) / 2f);
	}
	public static float SineInOut(float x)
	{
		return -(MathF.Cos(MathF.PI * x) - 1f) / 2f;
	}
	public static float PowIn(float x, float exponent)
	{
		return MathF.Pow(x, exponent);
	}
	public static float PowOut(float x, float exponent)
	{
		return 1f - MathF.Pow(1f - x, exponent);
	}
	public static float PowInOut(float x, float exponent)
	{
		return x < 0.5f ? MathF.Pow(2f, exponent - 1f) * MathF.Pow(x, exponent) : 1f - MathF.Pow(-2f * x + 2f, exponent) / 2f;
	}
	public static float ExpoIn(float x)
	{
		return x == 0f ? 0f : MathF.Pow(2f, 10f * x - 10f);
	}
	public static float ExpoOut(float x)
	{
		return x == 1f ? 1f : 1f - MathF.Pow(2f, -10f * x);
	}
	public static float ExpoInOut(float x)
	{
		return x == 0f ? 0f : x == 1f ? 1f : x < 0.5f ? MathF.Pow(2f, 20f * x - 10f) / 2f : (2f - MathF.Pow(2f, -20f * x + 10f)) / 2f;
	}
	public static float CircIn(float x)
	{
		return 1f - MathF.Sqrt(1f - MathF.Pow(x, 2f));
	}
	public static float CircOut(float x)
	{
		return MathF.Sqrt(1f - MathF.Pow(x - 1f, 2f));
	}
	public static float CircInOut(float x)
	{
		return x < 0.5f ? (1f - MathF.Sqrt(1f - MathF.Pow(2f * x, 2f))) / 2f : (MathF.Sqrt(1f - MathF.Pow(-2f * x + 2f, 2f)) + 1f) / 2f;
	}
	const float c1 = 1.70158f;
	const float c2 = c1 * 1.525f;
	const float c3 = c1 + 1f;
	public static float BackIn(float x)
	{
		return c3 * x * x * x - c1 * x * x;
	}
	public static float BackOut(float x)
	{
		return 1f + c3 * MathF.Pow(x - 1f, 3f) + c1 * MathF.Pow(x - 1f, 2f);
	}
	public static float BackInOut(float x)
	{
		return x < 0.5f ? (MathF.Pow(2f * x, 2f) * ((c2 + 1f) * 2f * x - c2)) / 2f : (MathF.Pow(2f * x - 2f, 2f) * ((c2 + 1f) * (x * 2f - 2f) + c2) + 2f) / 2f;
	}
	const float c4 = (2f * MathF.PI) / 3f;
	const float c5 = (2f * MathF.PI) / 4.5f;
	public static float ElasticIn(float x)
	{
		return x == 0f ? 0f : x == 1f ? 1f : -MathF.Pow(2f, 10f * x - 10f) * MathF.Sin((x * 10f - 10.75f) * c4);
	}
	public static float ElasticOut(float x)
	{
		return x == 0f ? 0f : x == 1f ? 1f : MathF.Pow(2f, -10f * x) * MathF.Sin((x * 10f - 0.75f) * c4) + 1f;
	}
	public static float ElasticInOut(float x)
	{
		return x == 0f ? 0f : x == 1f ? 1f : x < 0.5f ? -(MathF.Pow(2f, 20f * x - 10f) * MathF.Sin((20f * x - 11.125f) * c5)) / 2f : (MathF.Pow(2f, -20f * x + 10f) * MathF.Sin((20f * x - 11.125f) * c5)) / 2f + 1f;
	}
	const float n1 = 7.5625f;
	const float d1 = 2.75f;
	public static float BounceIn(float x)
	{
		return 1f - BounceOut(1f - x);
	}
	public static float BounceOut(float x)
	{
		if (x < 1 / d1)
		{
			return n1 * x * x;
		}
		else if (x < 2 / d1)
		{
			return n1 * (x -= 1.5f / d1) * x + 0.75f;
		}
		else if (x < 2.5 / d1)
		{
			return n1 * (x -= 2.25f / d1) * x + 0.9375f;
		}
		else
		{
			return n1 * (x -= 2.625f / d1) * x + 0.984375f;
		}
	}
	public static float BounceInOut(float x)
	{
		return x < 0.5f ? (1f - BounceOut(1f - 2f * x)) / 2f : (1f + BounceOut(2f * x - 1f)) / 2f;
	}
}