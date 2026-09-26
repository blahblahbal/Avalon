using Avalon.Projectiles.Hostile.BacteriumPrime;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode.BacteriumPrime;

public class BacteriumTendrilMelee : BacteriumTendril 
{
	public override void AI()
	{
		NPC.localAI[2] += 0.04f;
		var seed = Utils.RandomNextSeed((ulong)NPC.whoAmI);
		int randDirection = Utils.RandomInt(ref seed, 2) == 0 ? -1 : 1;
		NPC.rotation = (Utils.RandomFloat(ref seed) * MathHelper.TwoPi) + NPC.localAI[2] * randDirection;
		
		NPC.alpha = Owner.alpha;
		NPC.ai[0] += 0.002f;
		NPC.ai[1]++;
		if (!Owner.active && Main.netMode != NetmodeID.MultiplayerClient)
		{
			NPC.StrikeInstantKill();
		}
		float ownerDist = NPC.Center.Distance(Owner.Center);
		if (ownerDist < 40)
		{
			NPC.position -= NPC.Center.DirectionTo(Owner.Center) * (40 - ownerDist);
		}

		NPC.position += Owner.velocity;
		NPC.TargetClosest();
		if (!NPC.dontTakeDamage && (!NPC.HasValidTarget || !Target.Hitbox.IntersectsConeFastInaccurate(Owner.Center, 400, NPC.ai[0], 1f)))
		{
			NPC.frame.Y = (int)((Math.Abs(NPC.ai[2]) / 60f) * 4) * 32;
			Vector2 targetPos = ConnectionPoint + new Vector2(15, 0).RotatedBy(NPC.ai[0]) + new Vector2(5).RotatedBy(NPC.ai[1] * 0.05f);
			NPC.SimpleFlyMovement(NPC.Center.DirectionTo(targetPos) * 3f, 0.08f);
			NPC.localAI[0] = 1;
			NPC.ai[1] = 0;
			return;
		}
		else
		{
			var targetDist = NPC.Center.Distance(Target.Center);
			NPC.localAI[2] += Utils.Remap(targetDist, 32, 400, 0.08f, 0f);
			NPC.frameCounter++;
			if (NPC.frameCounter > MathHelper.Clamp(targetDist / 16, 2, 7))
			{
				NPC.frame.Y += 32;
				NPC.frameCounter = Main.rand.Next(-4, 1);
				if (NPC.frame.Y == 128)
				{
					SoundEngine.PlaySound(_chompSound, NPC.position);
					NPC.frame.Y = 0;
				}
			}
		}
		NPC.localAI[0] = 0;

		float timerForSin = MathF.Pow((NPC.ai[1] % 180) / 180, 3) * MathHelper.TwoPi;
		float sin = MathF.Sin(timerForSin);
		float cos = MathF.Cos(timerForSin);

		Vector2 whipTargetPos = Vector2.Lerp(Target.Center, ConnectionPoint, 0.5f);
		float connectionDist = Target.Center.Distance(ConnectionPoint);
		whipTargetPos += new Vector2(connectionDist / 2 * -cos, -sin * connectionDist * 0.5f * randDirection).RotatedBy(ConnectionPoint.DirectionTo(Target.Center).ToRotation());

		NPC.SimpleFlyMovement(NPC.Center.DirectionTo(whipTargetPos) * Utils.Remap(connectionDist, 0, 128, 3f, 8f), Utils.Remap(NPC.Center.Distance(whipTargetPos), 0, 64, 0.1f, 1f));

		//Dust.QuickDust(whipTargetPos, Color.Red);
	}
}
