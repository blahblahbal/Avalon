using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles;
public class AeonStarburst : Particle
{
	private static Asset<Texture2D> texture;
	public Vector2 Velocity;
	public int BurstTime = 14;
	public Color Color;
	public float Rotation;
	public float Scale;

	public AeonStarburst(Vector2 velocity, Color color, float rotation, float scale, int burstTime = 14)
	{
		Velocity = velocity;
		BurstTime = burstTime;
		Color = color;
		Rotation = rotation;
		Scale = scale;
	}
	public override void Update()
	{
		if (TimeInWorld == 0)
			TimeInWorld += 3;
		Lighting.AddLight(Position, 0, (30 - TimeInWorld) * 0.02f, (30 - TimeInWorld) * 0.03f);
		Position += Velocity;
		Velocity *= 0.95f;
		if (TimeInWorld > BurstTime)
			Active = false;
	}
	public override void Draw(SpriteBatch spriteBatch, Vector2 ScreenPos)
	{
		if(texture == null)
		{
			texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/SparklyAeon");
		}
		Vector2 DrawPos = Position - ScreenPos;
		spriteBatch.Draw(texture.Value, DrawPos, null, Color.Lerp(new Color(Color.R, Color.G, Color.B, 64), new Color(0, 0, 0, 0), (float)TimeInWorld / BurstTime) * 2, Rotation, texture.Size() / 2, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / BurstTime)) * Scale, SpriteEffects.None, 0);
		spriteBatch.Draw(texture.Value, DrawPos, null, Color.Lerp(new Color(255, 255, 255, 64), new Color(0, 0, 0, 0), (float)TimeInWorld / BurstTime * 0.9f) * 2, Rotation, texture.Size() / 2, (float)Math.Sin((TimeInWorld) * (MathHelper.Pi / BurstTime)) * Scale * 0.83f, SpriteEffects.None, 0);
	}
}
