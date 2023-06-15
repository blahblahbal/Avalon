using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Critters;

public class PeridotSquirrel : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 6;
        Main.npcCatchable[Type] = true;
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
        NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;
        NPCID.Sets.TownCritter[Type] = true;
        NPCID.Sets.CountsAsCritter[Type] = true;
        NPCID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Peridot>();
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.GemSquirrelAmethyst);
        AnimationType = NPCID.GemSquirrelAmethyst;
        AIType = NPCID.GemSquirrelAmethyst;
        NPC.friendly = false;
        NPC.catchItem = ModContent.ItemType<Items.Consumables.Critters.PeridotSquirrel>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("GemSquirrel"))
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo) //possibly incomplete, needs special rules for special seeds?
    {
        if (spawnInfo.Player.ZoneUnderworldHeight && spawnInfo.PlayerInTown && Main.rand.NextBool(35))
        {
            return 1f;
        }
        else
        {
            return 0;
        }
    }
    public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (NPC.direction == 1)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
        Rectangle frame6 = NPC.frame;
        float num35 = 0f;
        float num36 = Main.NPCAddHeight(NPC);
        Vector2 halfSize = new Vector2(TextureAssets.Npc[Type].Width() / 2, TextureAssets.Npc[Type].Height() / Main.npcFrameCount[Type] / 2);
        Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Avalon/NPCs/Critters/GemSquirrel_Glow").Value, new Vector2(NPC.position.X - screenPos.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[Type].Width() * NPC.scale / 2f + halfSize.X * NPC.scale, NPC.position.Y - screenPos.Y + (float)NPC.height - (float)TextureAssets.Npc[Type].Height() * NPC.scale / (float)Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale + num36 + num35 + NPC.gfxOffY), frame6, NPC.GetAlpha(Color.White), NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        Player player = Main.player[NPC.target];
        if (NPC.life > 0)
        {
            for (int num461 = 0; (double)num461 < (double)10.0 / (double)NPC.lifeMax * 20.0; num461++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PeridotDust>(), hit.HitDirection, -1f);
            }
            return;
        }
        for (int num462 = 0; num462 < 10; num462++)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<PeridotDust>(), 2 * hit.HitDirection, -2f);
        }
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("PeridotSquirrel").Type);
    }
}
public class TourmalineSquirrel : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 6;
        Main.npcCatchable[Type] = true;
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
        NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;
        NPCID.Sets.TownCritter[Type] = true;
        NPCID.Sets.CountsAsCritter[Type] = true;
        NPCID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Tourmaline>();
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.GemSquirrelAmethyst);
        AnimationType = NPCID.GemSquirrelAmethyst;
        AIType = NPCID.GemSquirrelAmethyst;
        NPC.friendly = false;
        NPC.catchItem = ModContent.ItemType<Items.Consumables.Critters.TourmalineSquirrel>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("GemSquirrel"))
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo) //possibly incomplete, needs special rules for special seeds?
    {
        if (spawnInfo.Player.ZoneUnderworldHeight && spawnInfo.PlayerInTown && Main.rand.NextBool(20))
        {
            return 1f;
        }
        else
        {
            return 0;
        }
    }
    public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (NPC.direction == 1)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
        Rectangle frame6 = NPC.frame;
        float num35 = 0f;
        float num36 = Main.NPCAddHeight(NPC);
        Vector2 halfSize = new Vector2(TextureAssets.Npc[Type].Width() / 2, TextureAssets.Npc[Type].Height() / Main.npcFrameCount[Type] / 2);
        Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Avalon/NPCs/Critters/GemSquirrel_Glow").Value, new Vector2(NPC.position.X - screenPos.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[Type].Width() * NPC.scale / 2f + halfSize.X * NPC.scale, NPC.position.Y - screenPos.Y + (float)NPC.height - (float)TextureAssets.Npc[Type].Height() * NPC.scale / (float)Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale + num36 + num35 + NPC.gfxOffY), frame6, NPC.GetAlpha(Color.White), NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        Player player = Main.player[NPC.target];
        if (NPC.life > 0)
        {
            for (int num461 = 0; (double)num461 < (double)10.0 / (double)NPC.lifeMax * 20.0; num461++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<TourmalineDust>(), hit.HitDirection, -1f);
            }
            return;
        }
        for (int num462 = 0; num462 < 10; num462++)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<TourmalineDust>(), 2 * hit.HitDirection, -2f);
        }
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("TourmalineSquirrel").Type);
    }
}
public class ZirconSquirrel : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 6;
        Main.npcCatchable[Type] = true;
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
        NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;
        NPCID.Sets.TownCritter[Type] = true;
        NPCID.Sets.CountsAsCritter[Type] = true;
        NPCID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Zircon>();
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.GemSquirrelAmethyst);
        AnimationType = NPCID.GemSquirrelAmethyst;
        AIType = NPCID.GemSquirrelAmethyst;
        NPC.friendly = false;
        NPC.catchItem = ModContent.ItemType<Items.Consumables.Critters.ZirconSquirrel>();
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("GemSquirrel"))
        });
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo) //possibly incomplete, needs special rules for special seeds?
    {
        if (spawnInfo.Player.ZoneUnderworldHeight && spawnInfo.PlayerInTown && Main.rand.NextBool(100))
        {
            return 1f;
        }
        else
        {
            return 0;
        }
    }
    public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (NPC.direction == 1)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
        Rectangle frame6 = NPC.frame;
        float num35 = 0f;
        float num36 = Main.NPCAddHeight(NPC);
        Vector2 halfSize = new Vector2(TextureAssets.Npc[Type].Width() / 2, TextureAssets.Npc[Type].Height() / Main.npcFrameCount[Type] / 2);
        Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Avalon/NPCs/Critters/GemSquirrel_Glow").Value, new Vector2(NPC.position.X - screenPos.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[Type].Width() * NPC.scale / 2f + halfSize.X * NPC.scale, NPC.position.Y - screenPos.Y + (float)NPC.height - (float)TextureAssets.Npc[Type].Height() * NPC.scale / (float)Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale + num36 + num35 + NPC.gfxOffY), frame6, NPC.GetAlpha(Color.White), NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        Player player = Main.player[NPC.target];
        if (NPC.life > 0)
        {
            for (int num461 = 0; (double)num461 < (double)10.0 / (double)NPC.lifeMax * 20.0; num461++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<ZirconDust>(), hit.HitDirection, -1f);
            }
            return;
        }
        for (int num462 = 0; num462 < 10; num462++)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<ZirconDust>(), 2 * hit.HitDirection, -2f);
        }
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(MathHelper.Pi / 16), Mod.Find<ModGore>("ZirconSquirrel").Type);
    }
}
