using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode.BacteriumPrime;

[AutoloadBossHead]
public class BacteriumPrimeSmallMelee : BacteriumPrime
{
	public override LocalizedText DisplayName => ModContent.GetInstance<BacteriumPrime>().DisplayName;

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.npcFrameCount[NPC.type] = 4;
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		NPC.width = NPC.height = 45;
		NPC.dontTakeDamage = false;
		SpinModifierTimer = 60;
	}
	public override void Behavior()
	{
		bool outside = Main.tile[NPC.Center.ToTileCoordinates()].WallType == 0;

		if (NPC.ai[0] > 0) // Dash attack
		{
			float dashDuration = 80;
			NPC.ai[1]++;
			if (NPC.ai[1] < 60)
			{
				NPC.velocity *= 0.99f;
			}
			else if (NPC.ai[1] == 60)
			{
				NPC.ai[3] = Main.rand.NextBool() ? -1 : 1;
				NPC.netUpdate = true;
				for (int i = 0; i < NPC.oldPos.Length; i++)
				{
					NPC.oldPos[i] = Vector2.Zero;
				}
				AfterimageOpacity = 1;
				SoundEngine.PlaySound(SoundID.Roar with { pitch = 1.2f}, NPC.Center);
				NPC.velocity = NPC.Center.DirectionTo(Target.Center).RotatedBy(MathHelper.PiOver2 * NPC.ai[3]) * 13;
			}
			else if (NPC.ai[1] < 60 + dashDuration)
			{
				NPC.velocity = NPC.velocity.RotatedBy(NPC.ai[3] * -(MathHelper.Pi / dashDuration));
				if (NPC.ai[1] > 30 + dashDuration)
				{
					NPC.velocity *= 0.95f;
				}
			}
			else if (NPC.ai[1] > 60 + dashDuration)
			{
				NPC.ai[0] = NPC.ai[1] = 0;
			}
		}
		else
		{
			float speedMultiplier = 1f;
			float accelMultiplier = 1f;
			if (NPC.Center.Distance(Target.Center) > 800)
			{
				speedMultiplier = accelMultiplier = 3f;
			}
			if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height) || outside)
			{
				accelMultiplier *= 7f;
			}
			NPC.SimpleFlyMovement(NPC.Center.DirectionTo(Target.Center) * 3f * speedMultiplier, 0.01f * accelMultiplier);
		}
	}
	public override void FindFrame(int frameHeight)
	{
		base.FindFrame(frameHeight);
	}
}
[AutoloadBossHead]
public class BacteriumPrimeSmallRanged : BacteriumPrime
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.npcFrameCount[NPC.type] = 4;
	}
	public override LocalizedText DisplayName => ModContent.GetInstance<BacteriumPrime>().DisplayName;
	public override void SetDefaults()
	{
		base.SetDefaults();
		NPC.width = NPC.height = 60;
		NPC.dontTakeDamage = false;
		SpinModifierTimer = 60;
	}
	public override void OnSpawn(IEntitySource source)
	{
		int tendril = ModContent.NPCType<BacteriumTendril>();
		float iterations = 8;
		for (int i = 0; i < iterations; i++)
		{
			Vector2 spawnLocation = NPC.Center + new Vector2(60, 0).RotatedBy(i / iterations * MathHelper.TwoPi);
			NPC n = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnLocation.X, (int)spawnLocation.Y, tendril, NPC.whoAmI, i / iterations * MathHelper.TwoPi, Main.rand.Next(100), 0, NPC.whoAmI)];
			NetMessage.SendData(MessageID.SyncNPC, number: n.whoAmI);
		}
	}
	public override void Behavior()
	{
		bool outside = Main.tile[NPC.Center.ToTileCoordinates()].WallType == 0;

		int tendrilType = ModContent.NPCType<BacteriumTendril>();
		List<int> tendrilWHOAMIs = new();
		bool hasAnyTendrils = false;

		foreach (NPC n in Main.ActiveNPCs)
		{
			if (n.type == tendrilType && n.ai[3] == NPC.whoAmI)
			{
				tendrilWHOAMIs.Add(n.whoAmI);
			}
		}
		// shooting
		NPC.ai[2] += outside ? 1.5f : 1;
		List<int> tendrilsThatCanAttack = tendrilWHOAMIs.FindAll(validTendrilForAttack);
		if (NPC.ai[2] >= 170 + (tendrilWHOAMIs.Count * 20) && tendrilsThatCanAttack.Count > 0)
		{
			int rand = tendrilsThatCanAttack[Main.rand.Next(tendrilsThatCanAttack.Count)];
			Main.npc[rand].ai[2] = 1;
			Main.npc[rand].netUpdate = true;
			NPC.ai[2] = 0;
		}

		if (NPC.ai[0] > 0) // Dash attack
		{
			float dashDuration = 80;
			NPC.ai[1]++;
			if (NPC.ai[1] < 60)
			{
				NPC.velocity *= 0.99f;
			}
			else if (NPC.ai[1] == 60)
			{
				NPC.ai[3] = Main.rand.NextBool() ? -1 : 1;
				NPC.netUpdate = true;
				for (int i = 0; i < NPC.oldPos.Length; i++)
				{
					NPC.oldPos[i] = Vector2.Zero;
				}
				AfterimageOpacity = 1;
				SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
				NPC.velocity = NPC.Center.DirectionTo(Target.Center).RotatedBy(MathHelper.PiOver2 * NPC.ai[3]) * 13;
			}
			else if (NPC.ai[1] < 60 + dashDuration)
			{
				NPC.velocity = NPC.velocity.RotatedBy(NPC.ai[3] * -(MathHelper.Pi / dashDuration));
				if (NPC.ai[1] > 30 + dashDuration)
				{
					NPC.velocity *= 0.95f;
				}
			}
			else if (NPC.ai[1] > 60 + dashDuration)
			{
				NPC.ai[0] = NPC.ai[1] = 0;
			}
		}
		else
		{
			float speedMultiplier = 1f;
			float accelMultiplier = 1f;
			if (NPC.Center.Distance(Target.Center) > 800)
			{
				speedMultiplier = accelMultiplier = 3f;
			}
			if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height) || outside)
			{
				accelMultiplier *= 7f;
			}
			NPC.SimpleFlyMovement(NPC.Center.DirectionTo(Target.Center) * 1.5f * speedMultiplier, 0.01f * accelMultiplier * Utils.Remap(NPC.Center.Distance(Target.Center),256,512,0.5f,1));
		}
	}
}
