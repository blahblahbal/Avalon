using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Particles;

public class ParticleSystem : ModSystem
{
	private static List<Particle> Particles = new();
	public override void Load()
	{
		On_Main.DrawDust += On_Main_DrawDust;
	}
	public override void Unload()
	{
		Particles.Clear();
	}
	private void On_Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
	{
		if (Main.netMode != NetmodeID.Server)
		{
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
			for (int i = 0; i < Particles.Count; i++)
			{
				Particles[i].Draw(Main.spriteBatch, Main.screenPosition);
			}
			Main.spriteBatch.End();
		}
		orig.Invoke(self);
	}
	public override void PostUpdateDusts()
	{
		if (Main.netMode == NetmodeID.Server)
			return;
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].Update();
			Particles[i].TimeInWorld++;
			if (Particles[i].Active == false)
			{
				Particles.RemoveAt(i);
				i--;
			}
		}
	}
	public static void NewParticle(Particle particle, Vector2 position)
	{
		if (Main.netMode == NetmodeID.Server)
			return;
		particle.Position = position;
		Particles.Add(particle);
		particle.OnSpawn();
	}
}
public abstract class Particle : ILoadable
{
	public Vector2 Position;
	public int TimeInWorld;
	public bool Active = true;
	public virtual void Update()
	{
	}
	public virtual void OnSpawn()
	{
	}
	public virtual void Draw(SpriteBatch spriteBatch, Vector2 ScreenPos)
	{
	}
	public void Load(Mod mod)
	{
	}
	public void Unload()
	{
	}
}
