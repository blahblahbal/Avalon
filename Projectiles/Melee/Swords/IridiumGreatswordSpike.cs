using Avalon.Dusts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class IridiumGreatswordSpike : ModProjectile
{
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1f, 1f, 1f, 0.8f) * Projectile.Opacity * (Projectile.scale - 0.3f);
	}
	public override void SetDefaults()
	{
		Projectile.width = 8;
		Projectile.height = 8;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Ranged;
	}
	public override void AI()
	{
		if (Projectile.ai[0] == 0)
		{
			Projectile.rotation = Utils.AngleLerp(Projectile.velocity.ToRotation() - MathHelper.PiOver2, Projectile.rotation + Projectile.velocity.X * 0.05f, Utils.Remap(Projectile.ai[0], 15, 40, 1f, 0, true));
		}
		else
		{
			Projectile.hide = true;
			Projectile.Center = Main.npc[(int)Projectile.ai[1]].Center + Projectile.velocity;
			Projectile.rotation = Projectile.Center.DirectionTo(Main.npc[(int)Projectile.ai[1]].Center).ToRotation() - MathHelper.PiOver2;
			if (!Main.npc[(int)Projectile.ai[1]].active)
			{
				Projectile.Kill();
			}
		}
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		// If attached to an NPC, draw behind tiles (and the npc) if that NPC is behind tiles, otherwise just behind the NPC.
		if (Projectile.ai[0] == 1)
		{
			int npcIndex = (int)Projectile.ai[1];
			if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
			{
				if (Main.npc[npcIndex].behindTiles)
				{
					behindNPCsAndTiles.Add(index);
				}
				else
				{
					behindNPCs.Add(index);
				}
			}
		}
	}

	public override bool ShouldUpdatePosition()
	{
		return Projectile.ai[0] == 0;
	}
	private readonly Point[] _sticking = new Point[4];
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Projectile.timeLeft = 60 * 5;
		Projectile.damage = 0;
		Projectile.ai[0] = 1;
		Projectile.ai[1] = target.whoAmI;
		Projectile.velocity = Projectile.Center - target.Center;
		Projectile.netUpdate = true;
		Projectile.KillOldestJavelin(Projectile.whoAmI, Type, (int)Projectile.ai[1], _sticking);
	}
	public override void OnKill(int timeLeft)
	{
		for(int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<SimpleColorableGlowyDust>());
			d.velocity = Main.rand.NextVector2Circular(3, 3);
			d.color = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0f) * 0.4f;
			d.noGravity = true;
		}
	}
}
