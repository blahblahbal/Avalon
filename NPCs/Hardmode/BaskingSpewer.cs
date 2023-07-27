using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class BaskingSpewer : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 4;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new int[]
    {
                BuffID.Confused
    }
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.SandShark);
        NPC.aiStyle = 103;
        NPC.damage = 67;
        NPC.defense = 23;
        NPC.lifeMax = 420;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 400f;
        NPC.npcSlots = 0.5f;
        NPC.noTileCollide = true;
        NPC.behindTiles = true;
        AIType = NPCID.SandsharkCorrupt;
        AnimationType = NPCID.SandsharkCorrupt;
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.ContagionDesert>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("In ancient times, a saltwater river once ran through the desert. These powerful creatures evolved to survive in the now dry sand.")
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
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerHead").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerBody").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerFin").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerFin2").Type, NPC.scale);
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.SharkFin, 8));
        npcLoot.Add(ItemDropRule.Common(ItemID.Nachos, 33));
        npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 25));
        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), ItemID.KiteSandShark, 25));
    }
}
