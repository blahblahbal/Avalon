using Terraria.Graphics.Renderers;

namespace Avalon.Particles;

// this is just a class that takes some stuff from vanillas particle orchestrator to use its particles that was stolen from Guts & Glory which itself stole from Vanillamity because I didn't feel like stealing from vanilla myself.
public class VanillaParticles
{
    public static PrettySparkleParticle RequestPrettySparkleParticle() => _poolPrettySparkle.RequestParticle();
    private static PrettySparkleParticle GetNewPrettySparkleParticle() => new PrettySparkleParticle();
    private static ParticlePool<PrettySparkleParticle> _poolPrettySparkle = new ParticlePool<PrettySparkleParticle>(200, new ParticlePool<PrettySparkleParticle>.ParticleInstantiator(GetNewPrettySparkleParticle));

    public static FadingParticle RequestFadingParticle() => _poolFadingParticle.RequestParticle();
    private static FadingParticle GetNewFadingParticle() => new FadingParticle();
    private static ParticlePool<FadingParticle> _poolFadingParticle = new ParticlePool<FadingParticle>(100, new ParticlePool<FadingParticle>.ParticleInstantiator(GetNewFadingParticle));

	public static RandomizedFrameParticle RequestRandomizedFrameParticle() => _poolRandomizedFrameParticle.RequestParticle();
	private static RandomizedFrameParticle GetNewRandomizedFrameParticle() => new RandomizedFrameParticle();
	private static ParticlePool<RandomizedFrameParticle> _poolRandomizedFrameParticle = new ParticlePool<RandomizedFrameParticle>(100, new ParticlePool<RandomizedFrameParticle>.ParticleInstantiator(GetNewRandomizedFrameParticle));
}