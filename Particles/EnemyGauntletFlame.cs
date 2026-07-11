using Avalon.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Renderers;
using Terraria.ModLoader;

namespace Avalon.Particles;

public class EnemyGauntletFlame : BaseParticle
{
	bool flip = false;
	public EnemyGauntletFlame(Vector2 position)
	{
		flip = Main.rand.NextBool();
		Position = position;
	}
	public override void Update(ref ParticleRendererSettings settings)
	{
		base.Update(ref settings);
		if (TimeInWorld > 14 * 4)
			Active = false;
	}
	public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
	{
		var texture = AssetReferences.Assets.Textures.EnemyGauntletFlame.Asset;
		spritebatch.Draw(texture.Value, Position + settings.AnchorPosition, texture.Frame(1, 14, 0, TimeInWorld / 4), new Color(1f, 1f, 1f, 0.35f), 0, new Vector2(53, 54), MathHelper.Clamp(TimeInWorld / 10f, 0, 1f), flip ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
	}
}
