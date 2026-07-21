using Avalon;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Contagion;

public class IckSlimeVariant : Ickslime, ICustomAutoload
{
	public static void Autoload(Mod mod)
	{
		mod.AddContent(new IckSlimeVariant((int)(30 * 0.8f), (int)(185 * 0.9f), 0.935f, 1.2f, 320, "Small"));
		mod.AddContent(new IckSlimeVariant((int)(30 * 1.1f), (int)(185 * 1.2f), 1.35f, 0.9f, 480, "Big"));
	}
	protected override bool CloneNewInstances => true;
	public override string Texture => ModContent.GetInstance<Ickslime>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<Ickslime>().DisplayName;

	public int Defense;
	public int LifeMax;
	public float Scale;
	public float KnockbackResist;
	public float Value;
	public string Variant;
	public override string Name => base.Name + Variant;
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers() { Hide = true});
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[Type] = ContentSamples.NpcPersistentIdsByNetIds[ModContent.NPCType<Ickslime>()];
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcPersistentIdsByNetIds[ModContent.NPCType<Ickslime>()], quickUnlock: false);
	}
	public IckSlimeVariant(int defense, int lifeMax, float scale, float knockbackResist, float value, string variant)
	{
		Defense = defense;
		LifeMax = lifeMax;
		Scale = scale;
		KnockbackResist = knockbackResist;
		Value = value;
		Variant = variant;
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		Banner = ModContent.NPCType<Ickslime>();
		BannerItem = ModContent.ItemType<Items.Banners.IckslimeBanner>();
		NPC.defense = Defense;
		NPC.lifeMax = LifeMax;
		NPC.scale = Scale;
		NPC.knockBackResist = KnockbackResist;
		NPC.value = Value;
	}
}
public class Ickslime : ModNPC
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
		npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 5, 10));
		npcLoot.Add(ItemDropRule.StatusImmunityItem(ItemID.Vitamins, 90));

		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsNotUp(), ItemID.PigPetItem, 1500));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.DontStarveIsUp(), ItemID.PigPetItem, 500));
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(BuffID.Weak, 360);
		}
		else
		{
			target.AddBuff(BuffID.Weak, 120);
		}
	}
	public override void SetDefaults()
	{
		NPC.damage = 57;
		NPC.lifeMax = 186;
		NPC.defense = 30;
		NPC.alpha = 55;
		NPC.width = 40;
		NPC.aiStyle = 1;
		NPC.scale = 1.1f;
		NPC.value = 400f;
		NPC.height = 30;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		Banner = NPC.type;
		BannerItem = ModContent.ItemType<Items.Banners.IckslimeBanner>();
		SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Ickslime"))
		});
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		return (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion || spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneUndergroundContagion) &&
			!spawnInfo.Player.InPillarZone() && Main.hardMode ? 0.7f / 3f : 0f;
	}
	public override void FindFrame(int frameHeight)
	{
		var num2 = 0;
		if (NPC.aiAction == 0)
		{
			if (NPC.velocity.Y < 0f)
			{
				num2 = 2;
			}
			else if (NPC.velocity.Y > 0f)
			{
				num2 = 3;
			}
			else if (NPC.velocity.X != 0f)
			{
				num2 = 1;
			}
			else
			{
				num2 = 0;
			}
		}
		else if (NPC.aiAction == 1)
		{
			num2 = 4;
		}
		NPC.frameCounter += 1.0;
		if (num2 > 0)
		{
			NPC.frameCounter += 1.0;
		}
		if (num2 == 4)
		{
			NPC.frameCounter += 1.0;
		}
		if (NPC.frameCounter >= 8.0)
		{
			NPC.frame.Y = NPC.frame.Y + frameHeight;
			NPC.frameCounter = 0.0;
		}
		if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
		{
			NPC.frame.Y = 0;
		}
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
