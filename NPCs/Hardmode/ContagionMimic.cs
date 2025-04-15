using Avalon.Common;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Banners;
using Avalon.Items.Tools.Hardmode;
using Avalon.Items.Weapons.Magic.Hardmode;
using Avalon.Items.Weapons.Melee.Hardmode;
using Avalon.Items.Weapons.Ranged.Hardmode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class ContagionMimic : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 14;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire3] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        Data.Sets.NPCSets.Wicked[NPC.type] = true;
        NPCID.Sets.TrailingMode[NPC.type] = 7;
        ItemID.Sets.KillsToBanner[ModContent.ItemType<ContagionMimicBanner>()] = 25;
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.BigMimicCrimson);
        AIType = NPCID.BigMimicCrimson;
        AnimationType = NPCID.BigMimicCrimson;
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<ContagionMimicBanner>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.ContagionMimic"))
        });
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        int num670 = 31;
        if (NPC.life > 0)
        {
            for (int num673 = 0; (double)num673 < hit.Damage / (double)NPC.lifeMax * 50.0; num673++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, num670);
            }
            return;
        }
        for (int num674 = 0; num674 < 20; num674++)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, num670);
        }
        int num675 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, new Vector2(hit.HitDirection, 0f), 61, NPC.scale);
        Gore gore9 = Main.gore[num675];
        Gore gore32 = gore9;
        gore32.velocity *= 0.3f;
        num675 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, new Vector2(hit.HitDirection, 0f), 62, NPC.scale);
        gore9 = Main.gore[num675];
        gore32 = gore9;
        gore32.velocity *= 0.3f;
        num675 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, new Vector2(hit.HitDirection, 0f), 63, NPC.scale);
        gore9 = Main.gore[num675];
        gore32 = gore9;
        gore32.velocity *= 0.3f;
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<HemorrhagingHalberd>(), ModContent.ItemType<InfectionHook>(),
            ModContent.ItemType<DartShotgun>(), ModContent.ItemType<Outbreak>(), ModContent.ItemType<ThePill>()));
        npcLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 5, 10));
        npcLoot.Add(ItemDropRule.Common(ItemID.GreaterManaPotion, 1, 5, 15));
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (Main.hardMode && spawnInfo.SpawnTileY > Main.worldSurface && spawnInfo.Player.RollLuck(Main.tenthAnniversaryWorld ? 25 : 75) == 0 &&
            spawnInfo.Player.InModBiome<Biomes.UndergroundContagion>())
        {
            return 1;
        }
        return 0;
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        return true;
    }
}
public class ContagionMimicHook : ModHook
{
    protected override void Apply()
    {
        On_NPC.NewNPC += On_NPC_NewNPC;
    }

    private int On_NPC_NewNPC(On_NPC.orig_NewNPC orig, IEntitySource source, int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target)
    {
        if (source is EntitySource_TileInteraction)
        {
            if ((Main.drunkWorld && Main.rand.NextBool(3)) || !Main.drunkWorld)
            {
                if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion && Type is NPCID.BigMimicCorruption or NPCID.BigMimicCrimson)
                {
                    Type = ModContent.NPCType<ContagionMimic>();
                }
            }
            //else if (!Main.drunkWorld)
            //{
            //    if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion && Type is NPCID.BigMimicCorruption or NPCID.BigMimicCrimson)
            //    {
            //        Type = ModContent.NPCType<ContagionMimic>();
            //    }
            //}
        }
        return orig(source, X, Y, Type, Start, ai0, ai1, ai2, ai3, Target);
    }
}
