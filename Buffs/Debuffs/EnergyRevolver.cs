using Avalon.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class EnergyRevolver : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		if (Main.rand.NextBool(15))
		{
			if (!TextureAssets.Projectile[ProjectileID.ScytheWhipProj].IsLoaded)
				Main.instance.LoadProjectile(ProjectileID.ScytheWhipProj);
			var asset = TextureAssets.Projectile[ProjectileID.ScytheWhipProj];
			asset.Wait();
			var p = VanillaParticles.RequestRandomizedFrameParticle();
			int time = Main.rand.Next(15, 30);
			p.SetBasicInfo(asset, null, Main.rand.NextVector2CircularEdge(1,1) * Main.rand.NextFloat(5,9), Main.rand.NextVector2FromRectangle(npc.Hitbox));
			p.SetTypeInfo(Main.projFrames[ProjectileID.ScytheWhipProj], 2, time);

			p.ColorTint = new Color(0.1f, Main.rand.NextFloat(0.5f, 1f), 1f, 0.5f);
			p.Scale = new Vector2(1.25f, 0.75f);
			p.FadeInNormalizedTime = 0.2f;
			p.FadeOutNormalizedTime = 0.2f;
			p.Rotation = p.Velocity.ToRotation();

			p.AccelerationPerFrame = -p.Velocity / time;
			Main.ParticleSystem_World_BehindPlayers.Add(p);
			for(int i = 0; i < 5; i++)
			{
				Dust d = Dust.NewDustPerfect(p.LocalPosition, DustID.Electric);
				d.noGravity = true;
				d.scale = 0.85f;
				d.velocity = p.Velocity.RotatedByRandom(0.5f) * Main.rand.NextFloat();
			}
		}
		if (Main.rand.NextBool(5))
		{
			Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Electric);
			d.noGravity = true;
			d.scale = 0.5f;
		}
	}
}
