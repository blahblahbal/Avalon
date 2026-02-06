using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Particles;

public class SoulEmbers : Particle
{
	private static Asset<Texture2D> texture;
	int Frame;
	float Opacity = 1;
	float rotation;
	float ai1;
	float ai2;
	int ai3;
	public Vector2 Velocity;
	public SoulEmbers(Vector2 velocity)
	{
		Velocity = velocity;
	}
	public override void OnSpawn()
	{
		Frame = Main.rand.Next(2);
		ai1 = Main.rand.NextFloat(0.7f, 1.1f);
		ai2 = Main.rand.NextFloat(-0.1f, 1.2f);
		ai3 = Main.rand.Next(255);
	}
	public override void Update()
	{
		Position += Velocity - (Main.screenPosition - Main.screenLastPosition) * ai2;
		Velocity.Y -= 0.06f;
		if (TimeInWorld % 2 == 0)
			Velocity = Velocity.RotatedByRandom(Main.rand.NextFloat(0.1f, 0.2f)) * Main.rand.NextFloat(0.97f, 1f);
		Velocity.X += Main.WindForVisuals * 0.2f * Main.rand.NextFloat(-0.4f, 1f);
		Opacity *= Main.rand.NextFloat(0.9f, 1.1f);

		if (TimeInWorld > 500)
			Opacity -= 0.1f;
		if (Opacity <= 0.1f)
			Active = false;

		rotation += 0.1f;

		Opacity = MathHelper.Clamp(Opacity, 0, 1);
	}
	public override void Draw(SpriteBatch spriteBatch, Vector2 screenpos)
	{
		if (texture == null)
			texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/SoulEmbers");
		int frameHeight = texture.Height() / 3;
		Rectangle frame = new Rectangle(0, frameHeight * Frame, texture.Width(), frameHeight);
		Vector2 frameOrigin = new Vector2(texture.Width()) / 2;
		Vector2 DrawPos = Position - screenpos;

		byte Sub = (byte)ai3;
		spriteBatch.Draw(texture.Value, DrawPos, frame, new Color(255 - Sub, 255, 255, 128) * Opacity, rotation, frameOrigin, 0.1f + (Opacity * ai1), SpriteEffects.None, 0);
		Sub = (byte)(ai3 * 0.295);
		for (int i = 0; i < 8; i++)
		{
			spriteBatch.Draw(texture.Value, DrawPos + new Vector2(0, 2 * ai1).RotatedBy(MathHelper.PiOver4 * i), frame, new Color(75, 75 - Sub, 75 - Sub, 0) * Opacity * 0.3f, rotation, frameOrigin, (0.1f + (Opacity * ai1)), SpriteEffects.None, 0);
		}
	}
}
