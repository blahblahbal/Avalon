using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Avalon.Buffs.Debuffs;
using Avalon.Items.Accessories.Hardmode;
using Avalon;

namespace Avalon.NPCs.Hardmode.ViralMummy;

public class ViralMummy : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.DarkMummy];

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 0.5f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        Data.Sets.NPCSets.Undead[NPC.type] = true;
        Data.Sets.NPCSets.Wicked[NPC.type] = true;
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.DarkMummy);
        AIType = NPCID.DarkMummy;
        AnimationType = NPCID.DarkMummy;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.ViralMummyBanner>();
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.ContagionDesert>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.ViralMummy"))
        });
    }

    public override void PostAI()
    {
        if (/*NPC.ai[3] < (float)num56 && */NPC.DespawnEncouragement_AIStyle3_Fighters_NotDiscouraged(NPC.type, NPC.position, NPC))
        {
            if (NPC.shimmerTransparency < 1f)
            {
                if (Main.rand.NextBool(500))
                {
                    SoundEngine.PlaySound(SoundID.Mummy, NPC.position);
                }
            }
        }
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            for (int num730 = 0; num730 < hit.Damage / (double)NPC.lifeMax * 50.0; num730++)
            {
                int num731 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 31, 0f, 0f, 0, default, 1.5f);
                Dust dust = Main.dust[num731];
                dust.velocity *= 2f;
                Main.dust[num731].noGravity = true;
            }
            return;
        }
        for (int num732 = 0; num732 < 20; num732++)
        {
            int num733 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 31, 0f, 0f, 0, default, 1.5f);
            Dust dust = Main.dust[num733];
            dust.velocity *= 2f;
            Main.dust[num733].noGravity = true;
        }
        int num734 = Gore.NewGore(NPC.GetSource_Death(),new Vector2(NPC.position.X, NPC.position.Y - 10f), new Vector2(hit.HitDirection, 0f), 61, NPC.scale);
        Gore gore2 = Main.gore[num734];
        gore2.velocity *= 0.3f;
        num734 = Gore.NewGore(NPC.GetSource_Death(), new Vector2(NPC.position.X, NPC.position.Y + NPC.height / 2 - 10f), new Vector2(hit.HitDirection, 0f), 62, NPC.scale);
        gore2 = Main.gore[num734];
        gore2.velocity *= 0.3f;
        num734 = Gore.NewGore(NPC.GetSource_Death(), new Vector2(NPC.position.X, NPC.position.Y + NPC.height - 10f), new Vector2(hit.HitDirection, 0f), 63, NPC.scale);
        gore2 = Main.gore[num734];
        gore2.velocity *= 0.3f;
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        if (Main.rand.NextBool(5))
        {
            target.AddBuff(BuffID.Silenced, 7 * 60);
        }
        if (Main.rand.NextBool(4))
        {
            target.AddBuff(BuffID.Darkness, 15 * 60);
        }
        if (Main.rand.NextBool(9))
        {
            target.AddBuff(ModContent.BuffType<BrokenWeaponry>(), 60 * 7);
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 10));
        npcLoot.Add(ItemDropRule.StatusImmunityItem(ItemID.Megaphone, 100));
        npcLoot.Add(ItemDropRule.StatusImmunityItem(ItemID.Blindfold, 100));
        npcLoot.Add(ItemDropRule.StatusImmunityItem(ModContent.ItemType<HiddenBlade>(), 100));
        npcLoot.Add(ItemDropRule.Common(ItemID.MummyMask, 75));
        npcLoot.Add(ItemDropRule.Common(ItemID.MummyShirt, 75));
        npcLoot.Add(ItemDropRule.Common(ItemID.MummyPants, 75));
    }
}
