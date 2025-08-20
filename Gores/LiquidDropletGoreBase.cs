using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Gores;
public abstract class LiquidDropletGoreBase : ModGore
{
	/// <summary>
	/// The vanilla droplet to copy the behaviour from.<para></para>
	/// Defaults to <see cref="GoreID.WaterDrip"/>, though this value itself does not do anything special.<br></br>
	/// Values that have an effect are:<br></br>
	/// <see cref="GoreID.LavaDrip"/><br></br>
	/// <see cref="GoreID.HoneyDrip"/><br></br>
	/// <see cref="GoreID.SandDrip"/><br></br>
	/// <see cref="GoreID.EbonsandDrip"/><br></br>
	/// <see cref="GoreID.CrimsandDrip"/><br></br>
	/// <see cref="GoreID.PearlsandDrip"/><para></para>
	/// Does not work for cloning modded drips, just manually clone them.
	/// </summary>
	public virtual int CloneType => GoreID.WaterDrip;
	/// <summary>
	/// <see cref="GoreID.LavaDrip"/> = (1, 0.5, 0.1)
	/// </summary>
	public virtual Vector3? LightColor => null;
	/// <summary>
	/// Defaults to 0, all vanilla droplets use this value.
	/// </summary>
	public virtual byte? SurfaceAlpha => null;
	/// <summary>
	/// <see cref="GoreID.WaterDrip"/> = 100<br></br>
	/// <see cref="GoreID.LavaDrip"/> = 100<br></br>
	/// <see cref="GoreID.HoneyDrip"/> = 100<br></br>
	/// <see cref="GoreID.SandDrip"/> = 0<br></br>
	/// <see cref="GoreID.EbonsandDrip"/> = 0<br></br>
	/// <see cref="GoreID.CrimsandDrip"/> = 0<br></br>
	/// <see cref="GoreID.PearlsandDrip"/> = 0
	/// </summary>
	public virtual byte? UndergroundAlpha => null;
	/// <summary>
	/// Whether or not this droplet will play water drip/splash sounds on collisions with tiles/liquids.<para></para>
	/// <see cref="GoreID.WaterDrip"/> = true
	/// </summary>
	public virtual bool? HasSound => null;
	/// <summary>
	/// <see cref="GoreID.LavaDrip"/> = 2<br></br>
	/// <see cref="GoreID.HoneyDrip"/> = 4<para></para>
	/// Has no effect if <see cref="AccumulatingFrameTimeStatic"/> has been set.
	/// </summary>
	public virtual float? AccumulatingFrameTimeMult => null;
	/// <summary>
	/// <see cref="GoreID.SandDrip"/> = 4<br></br>
	/// <see cref="GoreID.EbonsandDrip"/> = 4<br></br>
	/// <see cref="GoreID.CrimsandDrip"/> = 4<br></br>
	/// <see cref="GoreID.PearlsandDrip"/> = 4
	/// </summary>
	public virtual int? AccumulatingFrameTimeStatic => null;
	/// <summary>
	/// <see cref="GoreID.LavaDrip"/> = 2<br></br>
	/// <see cref="GoreID.HoneyDrip"/> = 3
	/// </summary>
	public virtual float? DissipatingFrameTimeMult => null;
	/// <summary>
	/// Defaults to 0.5, all vanilla droplets use this value.
	/// </summary>
	public virtual float FallingVelocityMin => 0.5f;
	/// <summary>
	/// Defaults to 12, all vanilla droplets use this value.
	/// </summary>
	public virtual float FallingVelocityMax => 12f;
	/// <summary>
	/// <see cref="GoreID.LavaDrip"/> = 0.175<br></br>
	/// <see cref="GoreID.HoneyDrip"/> = 0.15<br></br>
	/// default = 0.2
	/// </summary>
	public virtual float? FallingAccel => null;
	/// <summary>
	/// <see cref="GoreID.LavaDrip"/> = 1.5<br></br>
	/// <see cref="GoreID.HoneyDrip"/> = 2<br></br>
	/// <see cref="GoreID.SandDrip"/> = 1.5
	/// </summary>
	public virtual float? FallingFrameTimeMult => null;
	/// <summary>
	/// <see cref="GoreID.LavaDrip"/> = 2<br></br>
	/// <see cref="GoreID.HoneyDrip"/> = 6
	/// </summary>
	public virtual float? SplashFrameTimeMult => null;
	public override void SetStaticDefaults()
	{
		ChildSafety.SafeGore[Type] = true;
		GoreID.Sets.LiquidDroplet[Type] = true;
	}
	public override bool Update(Gore gore)
	{
		bool sandDrips = (CloneType is GoreID.SandDrip or (>= GoreID.EbonsandDrip and <= GoreID.PearlsandDrip));
		if (sandDrips)
		{
			gore.alpha = 0;
		}
		if ((SurfaceAlpha.HasValue || !sandDrips) && gore.position.Y < Main.worldSurface * 16.0 + 8.0)
		{
			gore.alpha = SurfaceAlpha ?? 0;
		}
		else if ((UndergroundAlpha.HasValue || !sandDrips))
		{
			gore.alpha = UndergroundAlpha ?? 100;
		}

		int frameTime = 4;
		gore.frameCounter++;
		if (gore.frame <= 4) // Accumulating
		{
			int supportTilePosX = (int)(gore.position.X / 16f);
			int supportTilePosY = (int)(gore.position.Y / 16f) - 1;
			if (WorldGen.InWorld(supportTilePosX, supportTilePosY) && !Main.tile[supportTilePosX, supportTilePosY].HasTile)
			{
				gore.active = false;
			}
			if (gore.frame is 0 or 1 or 2)
			{
				frameTime = 24 + Main.rand.Next(256);
			}
			if (gore.frame == 3)
			{
				frameTime = 24 + Main.rand.Next(96);
			}
			if (gore.frame == 5)
			{
				frameTime = 16 + Main.rand.Next(64);
			}
			if (AccumulatingFrameTimeStatic.HasValue)
			{
				frameTime = AccumulatingFrameTimeStatic.Value;
			}
			else if ((CloneType is GoreID.SandDrip or (>= GoreID.EbonsandDrip and <= GoreID.PearlsandDrip)) && gore.frame < 6)
			{
				frameTime = 4;
			}
			else if (AccumulatingFrameTimeMult.HasValue)
			{
				frameTime = (int)(frameTime * AccumulatingFrameTimeMult.Value);
			}
			else
			{
				if (CloneType is GoreID.LavaDrip)
				{
					frameTime *= 2;
				}
				if (CloneType is GoreID.HoneyDrip)
				{
					frameTime *= 4;
				}
			}
			if (gore.frameCounter >= frameTime)
			{
				gore.frameCounter = 0;
				gore.frame++;
				if (gore.frame == 5)
				{
					Gore fallingDrop = Gore.NewGoreDirect(new EntitySource_Misc("SpawnFinalGoreFrames"), gore.position, gore.velocity, Type);
					fallingDrop.frame = 9;
					fallingDrop.velocity *= 0f;
				}
			}
		}
		else if (gore.frame <= 6) // Dissipating after spawning falling gore
		{
			frameTime = 8;
			if (DissipatingFrameTimeMult.HasValue)
			{
				frameTime = (int)(frameTime * DissipatingFrameTimeMult.Value);
			}
			else
			{
				if (CloneType is GoreID.LavaDrip)
				{
					frameTime *= 2;
				}
				if (CloneType is GoreID.HoneyDrip)
				{
					frameTime *= 3;
				}
			}
			if (gore.frameCounter >= frameTime)
			{
				gore.frameCounter = 0;
				gore.frame++;
				if (gore.frame == 7)
				{
					gore.active = false;
				}
			}
		}
		else if (gore.frame <= 9) // Falling
		{
			frameTime = 6;
			if (FallingFrameTimeMult.HasValue)
			{
				frameTime = (int)(frameTime * FallingFrameTimeMult.Value);
			}
			else
			{
				if (CloneType is GoreID.LavaDrip)
				{
					frameTime = (int)(frameTime * 1.5);
				}
				else if (CloneType is GoreID.HoneyDrip)
				{
					frameTime *= 2;
				}
				else if (CloneType is GoreID.SandDrip)
				{
					frameTime = (int)(frameTime * 1.5);
				}
			}

			if (FallingAccel.HasValue)
			{
				gore.velocity.Y += FallingAccel.Value;
			}
			else
			{
				if (CloneType is GoreID.LavaDrip)
				{
					gore.velocity.Y += 0.175f;
				}
				else if (CloneType is GoreID.HoneyDrip)
				{
					gore.velocity.Y += 0.15f;
				}
				else
				{
					gore.velocity.Y += 0.2f;
				}
			}

			if (gore.velocity.Y < FallingVelocityMin)
			{
				gore.velocity.Y = FallingVelocityMin;
			}
			if (gore.velocity.Y > FallingVelocityMax)
			{
				gore.velocity.Y = FallingVelocityMax;
			}
			if (gore.frameCounter >= frameTime)
			{
				gore.frameCounter = 0;
				gore.frame++;
			}
			if (gore.frame > 9)
			{
				gore.frame = 7;
			}
		}
		else // Splash
		{
			if (SplashFrameTimeMult.HasValue)
			{
				frameTime = (int)(frameTime * SplashFrameTimeMult.Value);
			}
			else
			{
				if (CloneType is GoreID.LavaDrip)
				{
					frameTime *= 2;
				}
				else if (CloneType is GoreID.HoneyDrip)
				{
					frameTime *= 6;
				}
			}
			gore.velocity.Y += 0.1f;
			if (gore.frameCounter >= frameTime)
			{
				gore.frameCounter = 0;
				gore.frame++;
			}
			gore.velocity *= 0f;
			if (gore.frame > 14)
			{
				gore.active = false;
			}
		}

		if (LightColor.HasValue || CloneType is GoreID.LavaDrip) // Emit light
		{
			float lightMult = 0.6f;
			lightMult = gore.frame switch
			{
				0 or 14 => lightMult * 0.1f,
				1 or 6 or 13 => lightMult * 0.2f,
				2 or 12 => lightMult * 0.3f,
				3 or 5 or 11 => lightMult * 0.4f,
				4 or (<= 9) or 10 => lightMult * 0.5f,
				_ => 0f
			};
			float r = (LightColor?.X ?? 1f) * lightMult;
			float g = (LightColor?.Y ?? 0.5f) * lightMult;
			float b = (LightColor?.Z ?? 0.1f) * lightMult;
			Lighting.AddLight(gore.position + new Vector2(8f, 8f), r, g, b);
		}

		Vector2 preCollisionVel = gore.velocity;
		gore.velocity = Collision.TileCollision(gore.position, gore.velocity, 16, 14);
		if (gore.velocity != preCollisionVel) // Trigger splash when colliding with a tile
		{
			if (gore.frame < 10)
			{
				gore.frame = 10;
				gore.frameCounter = 0;
				if (HasSound ?? (CloneType is not (GoreID.LavaDrip or GoreID.HoneyDrip or GoreID.SandDrip or (>= GoreID.EbonsandDrip and <= GoreID.PearlsandDrip))))
				{
					SoundEngine.PlaySound(SoundID.Drip, new Vector2((int)gore.position.X + 8, (int)gore.position.Y + 8));
				}
			}
		}
		else if (Collision.WetCollision(gore.position + gore.velocity, 16, 14)) // Trigger splash when colliding with a liquid
		{
			if (gore.frame < 10)
			{
				gore.frame = 10;
				gore.frameCounter = 0;
				if (HasSound ?? (CloneType is not (GoreID.LavaDrip or GoreID.HoneyDrip or GoreID.SandDrip or (>= GoreID.EbonsandDrip and <= GoreID.PearlsandDrip))))
				{
					SoundEngine.PlaySound(SoundID.DripSplash, new Vector2((int)gore.position.X + 8, (int)gore.position.Y + 8));
				}
				((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(gore.position + new Vector2(8f, 8f));
			}
			int middleTilePosX = (int)(gore.position.X + 8f) / 16;
			int bottomTilePosY = (int)(gore.position.Y + 14f) / 16;
			if (Main.tile[middleTilePosX, bottomTilePosY] != null && Main.tile[middleTilePosX, bottomTilePosY].LiquidAmount > 0)
			{
				gore.velocity *= 0f;
				gore.position.Y = bottomTilePosY * 16 - Main.tile[middleTilePosX, bottomTilePosY].LiquidAmount / 16;
			}
		}

		gore.position += gore.velocity;

		return false;
	}
	public override Color? GetAlpha(Gore gore, Color lightColor)
	{
		if (CloneType is GoreID.LavaDrip)
		{
			return new Color(255, 255, 255, 200);
		}
		return base.GetAlpha(gore, lightColor);
	}
}
