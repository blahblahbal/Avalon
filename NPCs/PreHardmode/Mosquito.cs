using System;
using Avalon.Items.Material;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.Enums;
using Terraria.DataStructures;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Audio;

namespace Avalon.NPCs.PreHardmode;

public class Mosquito : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 6;
        Data.Sets.NPC.Toxic[NPC.type] = true;
    }

    public override void SetDefaults()
    {
        NPC.damage = 25;
        NPC.lifeMax = 50;
        NPC.defense = 12;
        NPC.noGravity = true;
        NPC.width = 58;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.height = 30;
        NPC.HitSound = SoundID.NPCHit32;
        NPC.DeathSound = SoundID.NPCDeath35;
        NPC.value = 200;
        //AnimationType = NPCID.Hornet;
        NPC.knockBackResist = 1f;
    }
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        Texture2D tex = TextureAssets.Hb1.Value;
        float brightness = Lighting.Brightness((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2) + NPC.gfxOffY) / 16f));
        if (NPC.life >= NPC.lifeMax)
        {
            Main.spriteBatch.Draw(tex, position - Main.screenPosition, null, new Color(0,1f,0,1) * brightness, 0, new Vector2(tex.Width / 2,0), scale, SpriteEffects.None, 0);

            Main.spriteBatch.Draw(tex, position - Main.screenPosition, new Rectangle(0,0, (int)MathHelper.Clamp(((NPC.life - NPC.lifeMax) / (NPC.lifeMax * 1.5f - NPC.lifeMax) * tex.Width + 2), 0, tex.Width),tex.Height), Color.Black * 0.3f, 0, new Vector2(tex.Width / 2, 0), scale, SpriteEffects.None, 0);
            
            Main.spriteBatch.Draw(tex, position - Main.screenPosition, new Rectangle(0,0,(int)((NPC.life - NPC.lifeMax) / (NPC.lifeMax * 1.5 - NPC.lifeMax) * tex.Width),tex.Height), new Color(0,0.7f, 1f, 1f) * brightness, 0, new Vector2(tex.Width / 2, 0), scale, SpriteEffects.None, 0);;
            
            return false;
        }
        else if(NPC.life == NPC.lifeMax) // Why does the above bit not work for this?? 
        {
            Main.spriteBatch.Draw(tex, position - Main.screenPosition, null, Color.Lime * brightness, 0, new Vector2(tex.Width / 2, 0), scale, SpriteEffects.None, 0);
        }
        return base.DrawHealthBar(hbPosition, ref scale, ref position);
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        NPC.ai[1] = 1;
        NPC.ai[0] = 0;
        SoundEngine.PlaySound(SoundID.Item3, NPC.position);
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MosquitoProboscis>(), 2));
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return spawnInfo.Player.InModBiome<Biomes.UndergroundTropics>() && !spawnInfo.Player.InPillarZone() ? 0.5f : 0f;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.Mosquito"))
        });
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.frame.Width = 74;
        NPC.frame.X = NPC.ai[1] == 0 ? 0 : 74;
        NPC.frameCounter++;
        if(NPC.frameCounter % 3 == 0)
        {
            NPC.frame.Y += frameHeight;
            if(NPC.frameCounter >= 9)
            {
                NPC.frame.Y -= frameHeight * 3;
                NPC.frameCounter = 0;
            }
        }
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        Main.EntitySpriteDraw(TextureAssets.Npc[Type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, TextureAssets.Npc[Type].Value.Size() / new Vector2(4,12),NPC.scale,NPC.spriteDirection == -1? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        return false;
    }
    public override void AI()
    {
        NPC.ai[0]++;
        NPC.spriteDirection = Math.Sign(NPC.velocity.X);
        if (!NPC.HasValidTarget)
        {
            NPC.TargetClosest();
            if (!NPC.HasValidTarget)
            {
                NPC.velocity *= 0.95f;
            }
        }
        else
        {
            if (NPC.ai[1] == 0)
            {
                NPC.velocity += NPC.Center.DirectionTo(NPC.PlayerTarget().Center) * 0.1f;
            }
            else
            {
                if (NPC.ai[0] < 30)
                {
                    NPC.velocity += NPC.Center.DirectionFrom(NPC.PlayerTarget().Center) * 0.14f;
                }
                else
                {
                    NPC.velocity *= 1.01f;
                    NPC.velocity += NPC.Center.DirectionFrom(NPC.PlayerTarget().Center) * 0.05f;
                }

                if (NPC.ai[0] % 60 == 0 && NPC.life < (int)(NPC.lifeMax * 1.5))
                {
                    NPC.HealEffect(5);
                    NPC.life += 5;
                }
                if (NPC.life >= NPC.lifeMax * 1.5)
                {
                    NPC.life = (int)(NPC.lifeMax * 1.5);
                    NPC.ai[0] = 0;
                    NPC.ai[1] = 0;
                }
            }
        }

        if (NPC.collideX)
        {
            NPC.velocity.X *= -0.8f;
            NPC.velocity += NPC.Center.DirectionTo(NPC.PlayerTarget().Center);
        }
        if (NPC.collideY)
        {
            NPC.velocity.Y *= -0.8f;
            NPC.velocity += NPC.Center.DirectionTo(NPC.PlayerTarget().Center);
        }

        NPC.velocity = NPC.velocity.LengthClamp(8);
        float maxRotate = 0.4f;
        NPC.rotation = MathHelper.Clamp((NPC.position.X - NPC.oldPosition.X) * 0.1f, -maxRotate, maxRotate);
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            string path = $"{Name}" + "Gore";
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>(path + "0").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>(path + "1").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("MosquitoWing").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>("MosquitoWing").Type, NPC.scale);

            for(int i = 0; i < 25 + (25 * NPC.ai[1]); i++)
            {
                Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5));
                d.scale *= 2;
                d.noGravity = !Main.rand.NextBool(3);
                if (!d.noGravity)
                    d.fadeIn = Main.rand.NextFloat(2);
            }
        }
    }
}
public class MosquitoDroopy : Mosquito
{

}
public class MosquitoSmall : Mosquito
{

}
public class MosquitoPainted : Mosquito
{

}

