using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Renderers;

namespace Avalon.Particles;
public abstract class BaseParticle : IParticle
{
	public Vector2 Position;
	public int TimeInWorld;
	public bool Active = true;
	public bool ShouldBeRemovedFromRenderer => !Active;

	public virtual void Update(ref ParticleRendererSettings settings)
	{
		TimeInWorld++;
	}
	public virtual void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
	{
	}
}