using Avalon.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Renderers;
using Terraria.ModLoader;

namespace Avalon.Particles;

public class SoulEmbers : BaseParticle
{
	public int Frame;
	public float Opacity = 1;
	public float rotation;
	public float ai1;
	public float ai2;
	public int ai3;
	public Vector2 Velocity;

	public SoulEmbers()
	{
		Frame = Main.rand.Next(2);
		ai1 = Main.rand.NextFloat(0.7f, 1.1f);
		ai2 = Main.rand.NextFloat(-0.1f, 1.2f);
		ai3 = Main.rand.Next(255);
		rotation = 0;
		Opacity = 1;
		Active = true;
	}
	public override void Update(ref ParticleRendererSettings settings)
	{
		base.Update(ref settings);
		Position += Velocity * ai2; //- (Main.screenPosition - Main.screenLastPosition) * ai2;
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

	public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
	{
		var texture = AssetReferences.Assets.Textures.SoulEmbers.Asset;
		int frameHeight = texture.Height() / 3;
		Rectangle frame = new Rectangle(0, frameHeight * Frame, texture.Width(), frameHeight);
		Vector2 frameOrigin = new Vector2(texture.Width()) / 2;
		Vector2 DrawPos = Position + settings.AnchorPosition;

		byte Sub = (byte)ai3;
		spritebatch.Draw(texture.Value, DrawPos, frame, new Color(255 - Sub, 255, 255, 128) * Opacity, rotation, frameOrigin, 0.1f + (Opacity * ai1), SpriteEffects.None, 0);
		Sub = (byte)(ai3 * 0.295);
		for (int i = 0; i < 8; i++)
		{
			spritebatch.Draw(texture.Value, DrawPos + new Vector2(0, 2 * ai1).RotatedBy(MathHelper.PiOver4 * i), frame, new Color(75, 75 - Sub, 75 - Sub, 0) * Opacity * 0.3f, rotation, frameOrigin, (0.1f + (Opacity * ai1)), SpriteEffects.None, 0);
		}
	}
}
