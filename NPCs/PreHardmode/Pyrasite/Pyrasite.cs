using Avalon;
using Avalon.Common.Players;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.PreHardmode.Pyrasite;

public class PyrasiteHead : WormHead
{
	public override int BodyType => ModContent.NPCType<PyrasiteBody>();

	public override int TailType => ModContent.NPCType<PyrasiteTail>();

	public override void SetStaticDefaults()
	{
		var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			CustomTexturePath = Texture + "_Bestiary",
			Position = new Vector2(55f, 18f),
			PortraitPositionXOverride = 10f,
			PortraitPositionYOverride = 11f
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		Data.Sets.NPCSets.Wicked[NPC.type] = true;
	}
	public override void SetDefaults()
	{
		NPC.damage = 15;
		NPC.netAlways = true;
		NPC.noTileCollide = true;
		NPC.lifeMax = 70;
		NPC.defense = 0;
		NPC.noGravity = true;
		NPC.width = 26;
		NPC.aiStyle = -1;
		NPC.behindTiles = true;
		NPC.value = 500f;
		NPC.height = 26;
		NPC.knockBackResist = 0f;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		Banner = NPC.type;
		BannerItem = ModContent.ItemType<Items.Banners.PyrasiteBanner>();
		SpawnModBiomes = [ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type];
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(
		[
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Pyrasite"))
		]);
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if ((spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion || spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneUndergroundContagion) && !spawnInfo.Player.InPillarZone())
			return 0.1f;
		return 0;
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PyrasiteGoreHead").Type, 1f);
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 128, default, Main.rand.NextFloat(1, 1.5f));
			}
		}
		for (int i = 0; i < 5; i++)
		{
			Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 128, default, Main.rand.NextFloat(1, 1.5f));
		}
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YuckyBit>(), 3));
	}
	public override void Init()
	{
		MinSegmentLength = 10;
		MaxSegmentLength = 18;

		CommonWormInit(this);
	}
	internal static void CommonWormInit(Worm worm)
	{
		// These two properties handle the movement of the worm
		worm.MoveSpeed = 9.5f;
		worm.Acceleration = 0.075f;
	}
	public class PyrasiteBody : WormBody
	{
		public override void SetStaticDefaults()
		{
			NPCID.Sets.RespawnEnemyID.Add(Type, ModContent.NPCType<PyrasiteHead>());

			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
		}
		public override void Init()
		{
			CommonWormInit(this);
		}
		public override void SetDefaults()
		{
			NPC.damage = 8;
			NPC.netAlways = true;
			NPC.noTileCollide = true;
			NPC.lifeMax = 70;
			NPC.defense = 4;
			NPC.noGravity = true;
			NPC.width = 26;
			NPC.aiStyle = -1;
			NPC.behindTiles = true;
			NPC.value = 500f;
			NPC.height = 26;
			NPC.knockBackResist = 0f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			SpawnModBiomes = [ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type];

			NPC.dontCountMe = true;
			NPC.npcSlots = 0;
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
			{
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PyrasiteGoreBody").Type, 1f);
				for (int i = 0; i < 10; i++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 128, default, Main.rand.NextFloat(1, 1.5f));
				}
			}
			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 128, default, Main.rand.NextFloat(1, 1.5f));
			}
		}
	}
}
public class PyrasiteTail : WormTail
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.RespawnEnemyID.Add(Type, ModContent.NPCType<PyrasiteHead>());

		var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Hide = true
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
	}
	public override void SetDefaults()
	{
		NPC.damage = 8;
		NPC.netAlways = true;
		NPC.noTileCollide = true;
		NPC.lifeMax = 70;
		NPC.defense = 6;
		NPC.noGravity = true;
		NPC.width = 26;
		NPC.aiStyle = -1;
		NPC.behindTiles = true;
		NPC.value = 500f;
		NPC.height = 26;
		NPC.knockBackResist = 0f;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		SpawnModBiomes = [ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type];

		NPC.dontCountMe = true;
		NPC.npcSlots = 0;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PyrasiteGoreTail").Type, 1f);
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 128, default, Main.rand.NextFloat(1, 1.5f));
			}
		}
		for (int i = 0; i < 5; i++)
		{
			Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 128, default, Main.rand.NextFloat(1, 1.5f));
		}
	}
	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}
	public override void Init()
	{
		PyrasiteHead.CommonWormInit(this);
	}
}
