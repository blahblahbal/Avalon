using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Critters;

public class ContaminatedBunny : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 7;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.CorruptBunny);
        AnimationType = NPCID.CorruptBunny;
        AIType = NPCID.CorruptBunny;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.ContaminatedBunny"))
        });
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            //for (int num461 = 0; (double)num461 < (double)10.0 / (double)NPC.lifeMax * 20.0; num461++)
            //{
            //    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PeridotDust>(), hit.HitDirection, -1f);
            //}
            return;
        }
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("ContaminatedBunny1").Type);
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("ContaminatedBunny2").Type);
    }
}

public class ContaminatedGoldfish : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 6;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.CorruptGoldfish);
        AnimationType = NPCID.CorruptGoldfish;
        AIType = NPCID.CorruptGoldfish;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.ContaminatedGoldfish"))
        });
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            //for (int num461 = 0; (double)num461 < (double)10.0 / (double)NPC.lifeMax * 20.0; num461++)
            //{
            //    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PeridotDust>(), hit.HitDirection, -1f);
            //}
            return;
        }
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("ContaminatedGoldfish1").Type);
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("ContaminatedGoldfish2").Type);
    }
}

public class ContaminatedPenguin : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 12;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.CorruptPenguin);
        AnimationType = NPCID.CorruptPenguin;
        AIType = NPCID.CorruptPenguin;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.ContaminatedPenguin"))
        });
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.OneFromOptions(50, ItemID.PedguinHat, ItemID.PedguinShirt, ItemID.PedguinPants));
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            //for (int num461 = 0; (double)num461 < (double)10.0 / (double)NPC.lifeMax * 20.0; num461++)
            //{
            //    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PeridotDust>(), hit.HitDirection, -1f);
            //}
            return;
        }
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("ContaminatedPenguin1").Type);
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("ContaminatedPenguin2").Type);
    }
}
