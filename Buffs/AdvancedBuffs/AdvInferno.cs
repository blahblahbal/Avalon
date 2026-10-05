using Avalon.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvInferno : ModBuff
{
	public override void Load()
	{
		On_Main.DrawInfernoRings += On_Main_DrawInfernoRings;
	}
	private void On_Main_DrawInfernoRings(On_Main.orig_DrawInfernoRings orig, Main self)
	{
		orig(self);
		//bool allowPlayerToEmitEffects = Main.FocusHelper.AllowPlayerToEmitEffects;
		foreach (Player p in Main.ActivePlayers)
		{
			if (p.HasBuff<AdvInferno>() && ! p.outOfRange)
			{
				if (!p.inferno)
				{
					//if (allowPlayerToEmitEffects)
					//{
						p.flameRingScale += 0.004f;
					//}
					if(p.flameRingScale >= 1)
					{
						p.flameRingScale = 0.8f;
					}

					//if (allowPlayerToEmitEffects)
					//{
						p.flameRingRot += 0.05f;
					//}
					if (p.flameRingRot > (float)Math.PI * 2f)
					{
						p.flameRingRot -= (float)Math.PI * 2f;
					}
					if (p.flameRingRot < (float)Math.PI * -2f)
					{
						p.flameRingRot += (float)Math.PI * 2f;
					}
				}
				for (int j = 0; j < 3; j++)
				{
					float num4 = p.flameRingScale + 0.1f * (float)j;
					if (num4 > 1f)
					{
						num4 -= 0.2f;
					}
					float num5 = MathHelper.Lerp(0.8f, 0f, Math.Abs(num4 - 0.9f) * 10f);
					Main.spriteBatch.Draw(AssetReferences.Assets.Textures.FrostFlameRing.Asset.Value, p.Center - Main.screenPosition, new Rectangle(0, 400 * j, 400, 400), new Color(num5, num5, num5, num5 * 0.7f), p.flameRingRot + (float)Math.PI / 3f * (float)j, new Vector2(200f, 200f), num4, SpriteEffects.None, 0f);
				}
			}
		}
	}

	public override void Update(Player player, ref int buffIndex)
    {
		Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.5f, 0.8f, 1f);
		int num3 = BuffID.Frostburn2;
		float num4 = 200f;
		bool flag = player.infernoCounter % 60 == 0;
		int damage = 20;

		//for(int i = 0; i < 3; i++)
		//{
		//	float rot = Main.rand.NextFloat(MathHelper.TwoPi);
		//	Vector2 pos = player.Center + new Vector2(0, Main.rand.NextFloat(num4 * 0.4f, num4)).RotatedBy(rot);
		//	if (Collision.IsWorldPointSolid(pos, true))
		//		continue;
		//	Dust d = Dust.NewDustPerfect(pos, DustID.IceTorch);
		//	d.velocity = new Vector2(-Main.rand.NextFloat(3,6), 0).RotatedBy(rot) + player.velocity;
		//	d.noGravity = false;
		//	d.customData = 1;
		//	d.scale += Main.rand.NextFloat(0.5f);
		//}

		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		foreach (NPC nPC in Main.ActiveNPCs)
		{
			if (!nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num3] && player.CanNPCBeHitByPlayerOrPlayerProjectile(nPC) && Vector2.Distance(player.Center, nPC.Center) <= num4)
			{
				if (nPC.FindBuffIndex(num3) == -1)
				{
					nPC.AddBuff(num3, 120);
				}
				if (flag)
				{
					player.ApplyDamageToNPC(nPC, damage, 0f, 0, crit: false, null);
				}
			}
		}
		if (!player.hostile)
		{
			return;
		}
		foreach(Player l in Main.ActivePlayers)
		{
			if (player == l || !player.active || player.dead || !player.hostile || player.buffImmune[num3] || (player.team == l.team && player.team != 0) || !(Vector2.Distance(l.Center, player.Center) <= num4))
			{
				continue;
			}
			if (player.FindBuffIndex(num3) == -1)
			{
				player.AddBuff(num3, 120);
			}
			if (flag)
			{
				PlayerDeathReason playerDeathReason = PlayerDeathReason.ByOther(16);
				player.Hurt(playerDeathReason, damage, 0, pvp: true);
				if (Main.netMode != 0)
				{
					NetMessage.SendPlayerHurt(l.whoAmI, playerDeathReason, damage, 0, critical: false, pvp: true, ImmunityCooldownID.General);
				}
			}
		}
	}
}
