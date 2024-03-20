using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class ContaminatedGhoul : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 8;
        Data.Sets.NPC.Wicked[NPC.type] = true;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }
    public override void SetDefaults()
    {
        NPC.width = 24;
        NPC.height = 44;
        NPC.aiStyle = 3;
        NPC.damage = 58;
        NPC.defense = 30;
        NPC.lifeMax = 262;
        NPC.HitSound = SoundID.NPCHit37;
        NPC.DeathSound = SoundID.NPCDeath40;
        NPC.knockBackResist = 0.6f;
        NPC.value = 650f;
        NPC.npcSlots = 0.5f;
        AIType = NPCID.DesertGhoulCorruption;
        AnimationType = NPCID.DesertGhoulCorruption;
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.ContagionCaveDesert>().Type };
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return spawnInfo.Player.InModBiome<Biomes.ContagionCaveDesert>() && Main.hardMode ? 0.33f : 0f;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.ContaminatedGhoul"))
        });
    }
    public override void PostAI()
    {
        Vector3 rgb = new Vector3(0.7f, 1f, 0.2f) * 0.5f;
        Lighting.AddLight(NPC.Top + new Vector2(0f, 15f), rgb);

        if (Main.rand.NextBool(700))
        {
            if(Main.rand.NextBool())
                SoundEngine.PlaySound(SoundID.Zombie55,NPC.position);
            else
                SoundEngine.PlaySound(SoundID.Zombie56, NPC.position);
        }
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            for (int num207 = 0; (double)num207 < hit.Damage / (double)NPC.lifeMax * 20.0; num207++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f);
                if (Main.rand.NextBool(4)&& 75 > 0)
                {
                    Dust dust59 = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PathogenDust>())];
                    dust59.noGravity = true;
                    dust59.scale = 1.5f;
                    dust59.fadeIn = 1f;
                    Dust dust = dust59;
                    dust.velocity *= 3f;
                }
            }
        }
        else
        {
            for (int num208 = 0; num208 < 20; num208++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f);
                if (Main.rand.NextBool(3)&& 75 > 0)
                {
                    Dust dust60 = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PathogenDust>())];
                    dust60.noGravity = true;
                    dust60.scale = 1.5f;
                    dust60.fadeIn = 1f;
                    Dust dust = dust60;
                    dust.velocity *= 3f;
                }
            }
            Gore.NewGore(NPC.GetSource_Death(),NPC.position, NPC.velocity, Mod.Find<ModGore>("ContaminatedGhoul1").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(),NPC.position, NPC.velocity, Mod.Find<ModGore>("ContaminatedGhoul2").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(),new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, Mod.Find<ModGore>("ContaminatedGhoul3").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(),new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, Mod.Find<ModGore>("ContaminatedGhoul3").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(),new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, Mod.Find<ModGore>("ContaminatedGhoul4").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_Death(),new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, Mod.Find<ModGore>("ContaminatedGhoul4").Type, NPC.scale);
        }
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        target.AddBuff(ModContent.BuffType<Pathogen>(), 7 * 60);
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 15));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Material.Pathogen>(), 3, 1, 3));
        npcLoot.Add(ItemDropRule.Common(ItemID.AncientCloth, 10));
    }
    public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture + "Glow").Value;
        Rectangle frame = NPC.frame;
        Vector2 drawPos = NPC.position + new Vector2(NPC.width / 2, NPC.height / 2) - Main.screenPosition;
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (NPC.spriteDirection == -1)
            spriteEffects = SpriteEffects.None;
        else
            spriteEffects = SpriteEffects.FlipHorizontally;
        Main.EntitySpriteDraw(texture, drawPos, frame, new Color(200, 200, 200, 100), NPC.rotation, new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, spriteEffects, 0);
    }
}
