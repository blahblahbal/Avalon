using Avalon;
using Avalon.Common.Players;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Contagion;

public class SnotslimeAlt : Snotslime
{
	public override LocalizedText DisplayName => ModContent.GetInstance<Snotslime>().DisplayName;
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[Type] = ContentSamples.NpcPersistentIdsByNetIds[ModContent.NPCType<Snotslime>()];
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcPersistentIdsByNetIds[ModContent.NPCType<Snotslime>()], quickUnlock: false);
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		Banner = ModContent.NPCType<Snotslime>();
	}
}
public class Snotslime : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 2;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
		Data.Sets.NPCSets.Wicked[NPC.type] = true;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 3, 10));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YuckyBit>(), 3));

		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsNotUp(), ItemID.PigPetItem, 1500)); // Monster Meat
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsUp(), ItemID.PigPetItem, 500));

		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsNotUp(), ItemID.TentacleSpike, 525));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsUp(), ItemID.TentacleSpike, 100));
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		target.AddBuff(BuffID.OgreSpit, 60 * 3);
	}
	public override void SetDefaults()
	{
		NPC.damage = 15;
		NPC.lifeMax = 60;
		NPC.defense = 15;
		NPC.alpha = 55;
		NPC.width = 20;
		NPC.aiStyle = 1;
		NPC.value = 180;
		NPC.height = 20;
		AnimationType = NPCID.BlueSlime;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		Banner = NPC.type;
		BannerItem = ModContent.ItemType<Items.Banners.SnotslimeBanner>();
		SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Snotslime"))
		});
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		return (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion || spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneUndergroundContagion) &&
			!spawnInfo.Player.InPillarZone() ? 0.4f / 2f : 0f;
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			for (int i = 0; i < 30; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, 175, default, Main.rand.NextFloat(1, 1.2f));
				Main.dust[d].color = new Color(215, 225, 162);
				Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1.5f, 5) * MathHelper.Clamp(NPC.velocity.X, -1, 1), Main.rand.NextFloat(-1, -5));
			}
		}
		else
			for (int i = 0; i < Math.Min(hit.Damage / 3, 30) + 1; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, 175, default, Main.rand.NextFloat(1, 1.2f));
				Main.dust[d].color = new Color(215, 225, 162);
				Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1.3f, 4) * MathHelper.Clamp(NPC.velocity.X, -1, 1), Main.rand.NextFloat(-1, -3));
			}
	}
}
