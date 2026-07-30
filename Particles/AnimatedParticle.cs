using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Renderers;

namespace Avalon.Particles;

public class AnimatedParticle : ABasicParticle
{
	public float FadeInNormalizedTime;

	public float FadeOutNormalizedTime = 1f;

	public Color ColorTint = Color.White;

	public float _timeTolive;

	public float _timeSinceSpawn;
	public int _frameCount;
	public static AnimatedParticle Request() => _pool.RequestParticle();
	private static AnimatedParticle GetNewParticle() => new AnimatedParticle();
	private static ParticlePool<AnimatedParticle> _pool = new ParticlePool<AnimatedParticle>(100, new ParticlePool<AnimatedParticle>.ParticleInstantiator(GetNewParticle));
	
	/// <summary>
	/// use this one instead of the other one
	/// </summary>
	public void SetBasicInfo(Asset<Texture2D> textureAsset, int frameCount, Vector2 initialVelocity, Vector2 initialLocalPosition)
	{
		_texture = textureAsset;
		_frameCount = frameCount;
		Velocity = initialVelocity;
		LocalPosition = initialLocalPosition;
		ShouldBeRemovedFromRenderer = false;
	}
	public override void FetchFromPool()
	{
		base.FetchFromPool();
		FadeInNormalizedTime = 0f;
		FadeOutNormalizedTime = 1f;
		ColorTint = Color.White;
		_timeTolive = 0f;
		_timeSinceSpawn = 0f;
	}

	public void SetTypeInfo(float timeToLive)
	{
		_timeTolive = timeToLive;
	}

	public override void Update(ref ParticleRendererSettings settings)
	{
		base.Update(ref settings);
		_timeSinceSpawn += 1f;
		if (_timeSinceSpawn >= _timeTolive)
		{
			base.ShouldBeRemovedFromRenderer = true;
		}
		_frame = _texture.Frame(1, _frameCount, 0, (int)(_timeSinceSpawn / _timeTolive * _frameCount));
	}

	public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
	{
		Color color = ColorTint;
		spritebatch.Draw(_texture.Value, settings.AnchorPosition + LocalPosition, _frame, color, Rotation, _frame.Size() / 2, Scale, SpriteEffects.None, 0f);
	}
}
