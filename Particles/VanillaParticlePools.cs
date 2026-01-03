using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Drawing;
using Terraria.Graphics.Renderers;

namespace Avalon.Particles;

public class VanillaParticlePools
{
	public static ParticlePool<PrettySparkleParticle> PoolPrettySparkle = new ParticlePool<PrettySparkleParticle>(200, GetNewPrettySparkleParticle);
	private static PrettySparkleParticle GetNewPrettySparkleParticle()
	{
		return new PrettySparkleParticle();
	}
}
