using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class ContaminatedGhoul : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 8;
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.DesertGhoulCorruption);
        AIType = NPCID.DesertGhoulCorruption;
        SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type };
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement("Ghouls plagued by the Contagion slobber infectious drool that weakens their victim's immune system.")
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
    public override void FindFrame(int frameHeight)
    {
        if (NPC.velocity.Y == 0f)
        {
            if (NPC.direction != 0)
            {
                NPC.spriteDirection = NPC.direction;
            }
            if (NPC.velocity.X == 0f)
            {
                NPC.frame.Y = 0;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y <= frameHeight)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            NPC.frameCounter += Math.Abs(NPC.velocity.X);
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter > 9.0)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y / frameHeight >= Main.npcFrameCount[Type])
            {
                NPC.frame.Y = frameHeight * 2;
            }
        }
        else
        {
            NPC.frame.Y = frameHeight;
            NPC.frameCounter = 0.0;
        }
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            for (int num207 = 0; (double)num207 < hit.Damage / (double)NPC.lifeMax * 20.0; num207++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f);
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
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f);
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
        target.AddBuff(ModContent.BuffType<Pathogen>(),7 * 60);
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 15));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Material.Pathogen>(), 3,1,3));
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
        Main.EntitySpriteDraw(texture, drawPos, frame, new Color(200,200,200,100), NPC.rotation, new Vector2(NPC.frame.Width / 2, NPC.frame.Height / 2), NPC.scale, spriteEffects, 0);
    }
}
