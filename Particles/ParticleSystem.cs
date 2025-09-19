using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Particles
{
	// We should replace this with Terrafirma's at some point.
	public enum ParticleType : byte
	{
		SanguineCuts = 0,
		CrystalSparkle = 1,
		MidnightRazorSlash = 2
	}
	public class ParticleSystem : ModSystem
	{
		static List<Particle> Particles;
		const int MaxParticles = 3000;
		public static void ParticleIDtoType(in byte particleID, out Particle particle)
		{
			particle = particleID switch
			{
				0 => new SanguineCuts(),
				1 => new CrystalSparkle(),
				2 => new MidnightRazorSlash(),
				_ => throw new NotImplementedException()
			};
		}

		public override void Load()
		{
			Particles = new List<Particle>(MaxParticles);
			On_Main.DrawCapture += On_Main_DrawCapture;
			On_Main.DrawDust += On_Main_DrawDust;
			On_Main.Draw += On_Main_Draw;
		}

		private void On_Main_Draw(On_Main.orig_Draw orig, Main self, GameTime gameTime)
		{
			orig.Invoke(self, gameTime);
			DrawParticles(true);
		}

		private void On_Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
		{
			orig.Invoke(self);
			DrawParticles();
		}
		private void On_Main_DrawCapture(On_Main.orig_DrawCapture orig, Main self, Rectangle area, Terraria.Graphics.Capture.CaptureSettings settings)
		{
			orig.Invoke(self, area, settings);
			DrawParticles();
		}
		public override void Unload()
		{
			Particles.Clear();
			On_Main.DrawCapture -= On_Main_DrawCapture;
			On_Main.DrawDust -= On_Main_DrawDust;
			On_Main.Draw -= On_Main_Draw;
		}
		public override void PostUpdateDusts()
		{
			for(int i = 0; i < Particles.Count; i++)
			{
				Particle particle = Particles[i];
				if (particle.Active)
				{
					if (particle.TimeInWorld == 0)
					{
						particle.OnSpawn();
					}
					particle.Update();
				}
				else
				{
					Particles.Remove(Particles[i]);
				}
			}
		}
		public static void DrawParticles(bool FrontLayer = false)
		{
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

			SpriteBatch spriteBatch = Main.spriteBatch;

			for (int i = 0; i < Particles.Count; i++)
			{
				Particle particle = Particles[i];
				if (particle.Active && particle.FrontLayer == FrontLayer)
				{
					particle.Draw(spriteBatch);
				}
			}

			for (int i = 0; i < Particles.Count; i++)
			{
				Particle particle = Particles[i];
				if (particle.Active)
				{
					particle.PostDraw(spriteBatch);
				}
			}

			Main.spriteBatch.End();
		}
		public static Particle AddParticle(Particle type, Vector2 position)
		{
			if (Particles.Count == MaxParticles)
			{
				Particles.Remove(Particles.First());
			}
			Particles.Add(type);
			Particles.Last().Position = position;
			return Particles.Last();
		}
		public static Particle AddParticle(Particle type, Vector2 position, Vector2 velocity, Color color, float AI1 = 0, float AI2 = 0, float AI3 = 0, float scale = 1f)
		{
			if (Particles.Count == MaxParticles)
			{
				Particles.Remove(Particles.First());
			}
			Particles.Add(type);
			Particles.Last().Position = position;
			Particles.Last().Velocity = velocity;
			Particles.Last().Color = color;
			Particles.Last().ai1 = AI1;
			Particles.Last().ai2 = AI2;
			Particles.Last().ai3 = AI3;
			Particles.Last().Scale = scale;

			return Particles.Last();
		}
		/// <summary>
		/// Use the other overload to spawn on the current client, this overload is used purely when syncing particles
		/// ID must be defined manually in enum ParticleType and ParticleIDtoType at the top of this file
		/// </summary>
		/// <returns></returns>
		public static Particle AddParticle(byte particleID, Vector2 position, Vector2 velocity, Color color, float AI1 = 0, float AI2 = 0, float AI3 = 0, float scale = 1f)
		{
			ParticleIDtoType(particleID, out Particle type);
			if (Particles.Count == MaxParticles)
			{
				Particles.Remove(Particles.First());
			}
			Particles.Add(type);
			Particles.Last().Position = position;
			Particles.Last().Velocity = velocity;
			Particles.Last().Color = color;
			Particles.Last().ai1 = AI1;
			Particles.Last().ai2 = AI2;
			Particles.Last().ai3 = AI3;
			Particles.Last().Scale = scale;

			return Particles.Last();
		}
	}
	public abstract class Particle
	{
		public Vector2 Position;
		public Vector2 Velocity;
		public int TimeInWorld;
		public float ai1;
		public float ai2;
		public float ai3;
		public bool Active = true;
		public Color Color;
		public bool FrontLayer;
		public float Scale = 1f;
		public virtual void Update()
		{
		}
		public virtual void OnSpawn()
		{

		}
		public virtual void Draw(SpriteBatch spriteBatch)
		{
		}
		public virtual void PostDraw(SpriteBatch spriteBatch)
		{
		}
	}
}
