using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.NPCs.PreHardmode;

public class PyrasiteHead : WormHead
{
    public override int BodyType => ModContent.NPCType<PyrasiteBody>();

    public override int TailType => ModContent.NPCType<PyrasiteTail>();

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
    }
    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.8f);
    }
    //public override float SpawnChance(NPCSpawnInfo spawnInfo)
    //{
    //    if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && !spawnInfo.Player.InPillarZone() && spawnInfo.Player.ZoneOverworldHeight)
    //        return (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && !spawnInfo.Player.InPillarZone() && spawnInfo.Player.ZoneOverworldHeight) ? 0.1f : 0f;
    //    return 0f;
    //}
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PyrasiteHead").Type, 1f);
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
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YuckyBit>()));
    }
    public override void Init()
    {
        MinSegmentLength = 10;
        MaxSegmentLength = 15;

        CommonWormInit(this);
    }
    internal static void CommonWormInit(Worm worm)
    {
        // These two properties handle the movement of the worm
        worm.MoveSpeed = 7.5f;
        worm.Acceleration = 0.065f;
    }
    public class PyrasiteBody : WormBody
    {
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
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PyrasiteBody").Type, 1f);
                for(int i = 0; i < 10; i++) 
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2),Main.rand.NextFloat(-2, 2),128,default,Main.rand.NextFloat(1,1.5f));
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
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PyrasiteTail").Type, 1f);
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
