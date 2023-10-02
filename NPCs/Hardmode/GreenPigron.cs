using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

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
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.GreenPigron"))
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
            for (int num689 = 0; num689 < 10; num689++)
            {
                int num690 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Cloud, 0f, 0f, 0, default, 1.5f);
                Dust dust103 = Main.dust[num690];
                Dust dust190 = dust103;
                dust190.velocity *= 2f;
                Main.dust[num690].noGravity = true;
            }
            for (int num685 = 0; num685 < 4; num685++)
            {
                int num686 = Gore.NewGore(NPC.GetSource_Death(), new Vector2(NPC.position.X, NPC.position.Y + NPC.height / 2 - 10f), new Vector2(hit.HitDirection, 0f), 99, NPC.scale);
                Gore gore10 = Main.gore[num686];
                Gore gore32 = gore10;
                gore32.velocity *= 0.3f;
            }
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
