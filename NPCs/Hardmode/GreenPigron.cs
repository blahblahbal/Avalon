using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class GreenPigron : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 14;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.PigronCorruption);
        NPC.aiStyle = 2;
        NPC.damage = 70;
        NPC.defense = 17;
        NPC.lifeMax = 234;
        NPC.HitSound = SoundID.NPCHit27;
        NPC.DeathSound = SoundID.NPCDeath30;
        NPC.value = 2000f;
        NPC.npcSlots = 0.5f;
        AIType = NPCID.PigronCorruption;
        AnimationType = NPCID.PigronCorruption;
        //SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.ContagionDesert>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("This elusive dragon-pig hybrid has excellent stealth capabilities despite its rotund figure. It is uncertain how they came to exist.")
        });
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            return;
        }
        else
        {
            //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GreenPigron1").Type, NPC.scale);
            //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GreenPigron2").Type, NPC.scale);
            //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GreenPigron3").Type, NPC.scale);
            //Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GreenPigron4").Type, NPC.scale);
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.Bacon, 16));
        npcLoot.Add(ItemDropRule.Common(ItemID.HamBat, 25));
        npcLoot.Add(ItemDropRule.Common(ItemID.PigronMinecart, 100));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), ItemID.KitePigron, 25));
    }
}
