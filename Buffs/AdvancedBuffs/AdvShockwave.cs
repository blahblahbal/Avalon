using System.Collections.Generic;
using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;


public class AdvShockwave : ModBuff
{
	private int fall_time = 0;

	public override void Update(Player player, ref int buffIndex)
	{

		if (Main.rand.NextBool(50))
		{
			int D = Dust.NewDust(player.position, player.width, player.height, 9, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 1.2f, 60, new Color(), 1f);
			Main.dust[D].noGravity = true;
			Main.dust[D].velocity.X *= 1.2f;
			Main.dust[D].velocity.X *= 1.2f;
		}
		if (Main.rand.NextBool(50))
		{
			int D2 = Dust.NewDust(player.position, player.width, player.height, 9, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 1.2f, 60, new Color(), 1f);
			Main.dust[D2].noGravity = true;
			Main.dust[D2].velocity.X *= -1.2f;
			Main.dust[D2].velocity.X *= 1.2f;
		}
		if (Main.rand.NextBool(50))
		{
			int D3 = Dust.NewDust(player.position, player.width, player.height, 9, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 1.2f, 60, new Color(), 1f);
			Main.dust[D3].noGravity = true;
			Main.dust[D3].velocity.X *= 1.2f;
			Main.dust[D3].velocity.X *= -1.2f;
		}
		if (Main.rand.NextBool(50))
		{
			int D4 = Dust.NewDust(player.position, player.width, player.height, 9, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 1.2f, 60, new Color(), 1f);
			Main.dust[D4].noGravity = true;
			Main.dust[D4].velocity.X *= -1.2f;
			Main.dust[D4].velocity.X *= -1.2f;
		}

		Vector2 p_pos = player.position + (new Vector2(player.width, player.height) / 2f);
		//int numOfNPCs = 0;
		if (player.gravDir == 1f ? player.velocity.Y > 0f : player.velocity.Y < 0f)
		{
			fall_time++;
			if (fall_time > 76)
			{
				fall_time = 76;
			}
		}

		if ((player.gravDir == 1f ? player.GetModPlayer<AvalonPlayer>().playerOldVelocity[2].Y < 0f : player.GetModPlayer<AvalonPlayer>().playerOldVelocity[2].Y > 0f) || player.GetModPlayer<AvalonPlayer>().playerOldVelocity[2].Y == 0f || player.GetModPlayer<AvalonPlayer>().playerOldVelocity[2].Y == 1E-05f)
		{
			fall_time = 0;
		}
		if (player.IsOnGroundPrecise() && (player.gravDir == 1f ? player.GetModPlayer<AvalonPlayer>().playerOldVelocity[2].Y > 0f : player.GetModPlayer<AvalonPlayer>().playerOldVelocity[2].Y < 0f) && player.velocity.Y == 0f && fall_time > 23) // just fell
		{
			float fall_dist = ((fall_time - 23f) / (76f - 23f) * (21f - 3.5f) + 3.5f) * (player.GetModPlayer<AvalonPlayer>().playerOldVelocity[2].Y * player.gravDir / 10f); // remap fall_time to range from 3.5f to 21f

			float Radius = 8f * fall_time * (player.GetModPlayer<AvalonPlayer>().playerOldVelocity[2].Y * player.gravDir / 10f);
			for (int o = Main.npc.Length - 1; o > 0; o--)
			{
				// iterate through NPCs
				NPC N = Main.npc[o];
				var list = new List<NPC>();
				if (!N.active || N.dontTakeDamage || N.friendly || N.life < 1 || N.type == NPCID.TargetDummy)
				{
					continue;
				}

				if (N.Center.Distance(player.Center) < Radius)
				{
					list.Add(N);
				}

				var n_pos = N.Center; // NPC location
				int HitDir = -1;
				if (n_pos.X > p_pos.X)
				{
					HitDir = 1;
				}

				if (N.Center.Distance(player.Center) < Radius)
				{
					//numOfNPCs++;
					int multiplier = 7;

					//if (player.IsOnGroundPrecise() && numOfNPCs == list.Count - 1)
					//{
					//    break;
					//}

					//if (player.GetModPlayer<ExxoEquipEffectPlayer>().EarthInsignia && Main.hardMode)
					//{
					//    multiplier = 6;
					//}
					//else if (!player.GetModPlayer<ExxoEquipEffectPlayer>().EarthInsignia && Main.hardMode)
					//{
					//    multiplier = 4;
					//}
					//else if (player.GetModPlayer<ExxoEquipEffectPlayer>().EarthInsignia && !Main.hardMode)
					//{
					//    multiplier = 3;
					//}
					//else if (!player.GetModPlayer<ExxoEquipEffectPlayer>().EarthInsignia && !Main.hardMode)
					//{
					//    multiplier = 2;
					//}

					//NPC.HitInfo h = new NPC.HitInfo { Damage = (int)(multiplier * fall_dist * -((N.Center.Distance(player.Center) / Radius) - 1)) * 2, Knockback = fall_dist * -((N.Center.Distance(player.Center) / Radius) - 1) * 2.7f * N.knockBackResist, HitDirection = HitDir };
					//int dmg = N.StrikeNPC(h);
					//player.addDPS(dmg);
					//if (Main.netMode != NetmodeID.SinglePlayer)
					//{
					//	NetMessage.SendStrikeNPC(N, h);
					//}

					int dmg = N.SimpleStrikeNPC(damage: (int)(multiplier * fall_dist * -((N.Center.Distance(player.Center) / Radius) - 1)) * 2, hitDirection: HitDir, knockBack: fall_dist * -((N.Center.Distance(player.Center) / Radius) - 1) * 2.7f);
					player.addDPS(dmg);
					// optionally add debuff here
				} // END on screen
			} // END iterate through NPCs

			var Sound = SoundEngine.PlaySound(SoundID.Item14, player.position);
			ParticleSystem.AddParticle(new ShockwaveParticle(), player.Center + new Vector2(0, (player.height * 0.5f * player.gravDir)), Vector2.Zero, default, Radius);
			if (SoundEngine.TryGetActiveSound(Sound, out ActiveSound sound) && sound != null && sound.IsPlaying)
			{
				sound.Volume = MathHelper.Clamp(fall_dist / 7f, 0.2f, 3);
			}
			if (player.whoAmI == Main.myPlayer && ModContent.GetInstance<AvalonClientConfig>().AdditionalScreenshakes)
			{
				PunchCameraModifier modifier = new PunchCameraModifier(player.Center, new Vector2(Main.rand.NextFloat(-0.3f, 0.3f), fall_dist / 2f), 1f, 3f, 15, 300f, player.name);
				Main.instance.CameraModifiers.Add(modifier);
			}
			fall_time = 0; // just in case the checks above fail for whatever reason
		} // END just fell
	}
}
