using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class MineralSlime : ModNPC
{
    int[] Ores =
    { 
        ItemID.CobaltOre, ItemID.PalladiumOre, ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
        ItemID.MythrilOre, ItemID.OrichalcumOre, ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
        ItemID.AdamantiteOre, ItemID.TitaniumOre, ModContent.ItemType<Items.Material.Ores.TroxiniumOre>()
    };
    Color[] OreColor =
    {
        new Color(61, 164, 196), new Color(240, 91, 51), new Color(147, 83, 119),
        new Color(157, 210, 144), new Color(248, 113, 227), new Color(94, 199, 197),
        new Color(221, 85, 152), new Color(190, 187, 220), new Color(214, 191, 43)
    };
    int[] OreDusts =
    {
        DustID.Cobalt, DustID.Palladium, ModContent.DustType<Dusts.DurataniumDust>(),
        DustID.Mythril, DustID.Orichalcum, ModContent.DustType<Dusts.NaquadahDust>(),
        DustID.Adamantite, DustID.Titanium, ModContent.DustType<Dusts.TroxiniumDust>()
    };
    int WhichOre;
    public override void AI()
    {
        VanillaNPCExpertChanges.SlimeJumpSpeedIncrease(NPC, 1.2f);
        NPC.ai[0]++;
    }
    public override void OnSpawn(IEntitySource source)
    {
        WhichOre = Main.rand.Next(0, Ores.Length);
        NPC.alpha = 90;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(WhichOre);
        writer.WriteRGB(OreColor[WhichOre]);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        WhichOre = reader.ReadInt32();
        NPC.color = reader.ReadRGB();
    }
    public override void DrawEffects(ref Color drawColor)
    {
        Color lightColor = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
        
        // looks nicer, but has additive blending which isn't desirable
        //int colorAvg = (OreColor[WhichOre].R + OreColor[WhichOre].G + OreColor[WhichOre].B) / 3;
        //int colorClampR = (int)MathHelper.Clamp(OreColor[WhichOre].R, colorAvg - 80, colorAvg + 80);
        //int colorClampG = (int)MathHelper.Clamp(OreColor[WhichOre].G, colorAvg - 80, colorAvg + 80);
        //int colorClampB = (int)MathHelper.Clamp(OreColor[WhichOre].B, colorAvg - 80, colorAvg + 80);
        //drawColor.R = (byte)MathHelper.Clamp(colorClampR * (lightColor.R * 1.2f) / 255f, 0, 255);
        //drawColor.G = (byte)MathHelper.Clamp(colorClampG * (lightColor.G * 1.2f) / 255f, 0, 255);
        //drawColor.B = (byte)MathHelper.Clamp(colorClampB * (lightColor.B * 1.2f) / 255f, 0, 255);
        //drawColor.A = 10;
        drawColor.R = (byte)MathHelper.Clamp(OreColor[WhichOre].R * (lightColor.R * 1.55f) / 255f, 0, 255);
        drawColor.G = (byte)MathHelper.Clamp(OreColor[WhichOre].G * (lightColor.G * 1.55f) / 255f, 0, 255);
        drawColor.B = (byte)MathHelper.Clamp(OreColor[WhichOre].B * (lightColor.B * 1.55f) / 255f, 0, 255);
        drawColor.A = (byte)(NPC.alpha * lightColor.A);
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        for (int i = 0; i < Ores.Length; i++)
        {
            Main.instance.LoadItem(Ores[i]);
        }
        float rotate = MathHelper.SmoothStep(0.1f, -0.1f, Main.masterColor);
        Texture2D oreTexture;
        if (Ores[WhichOre] > ItemID.Count)
        {
            if (Ores[WhichOre] == ModContent.ItemType<Items.Material.Ores.DurataniumOre>())
            {
                oreTexture = ModContent.Request<Texture2D>(ModContent.GetInstance<Items.Material.Ores.DurataniumOre>().Texture).Value;
            }
            else if (Ores[WhichOre] == ModContent.ItemType<Items.Material.Ores.NaquadahOre>())
            {
                oreTexture = ModContent.Request<Texture2D>(ModContent.GetInstance<Items.Material.Ores.NaquadahOre>().Texture).Value;
            }
            else if (Ores[WhichOre] == ModContent.ItemType<Items.Material.Ores.TroxiniumOre>())
            {
                oreTexture = ModContent.Request<Texture2D>(ModContent.GetInstance<Items.Material.Ores.TroxiniumOre>().Texture).Value;
            }
            else oreTexture = TextureAssets.Item[ItemID.CopperOre].Value; // will never get here but VS is stupid
        }
        else
            oreTexture = TextureAssets.Item[Ores[WhichOre]].Value;
        Rectangle frame = oreTexture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Main.EntitySpriteDraw(oreTexture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.frame.Y * -0.05f), frame, drawColor, NPC.rotation + rotate, frameOrigin, NPC.scale, SpriteEffects.None);
        return base.PreDraw(spriteBatch, screenPos, drawColor);
    }
    public override void OnKill()
    {
        Item.NewItem(NPC.GetSource_FromThis(), NPC.Hitbox, Ores[WhichOre], Main.rand.Next(10, 30));
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {

            for (int i = 0; i < 7; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, Main.rand.Next(100, 200), default, Main.rand.NextFloat(1, 1.5f));
                Main.dust[d].color = OreColor[WhichOre] * 0.3f;
                Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1f, 4) * hit.HitDirection, Main.rand.NextFloat(-1, -4));
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, OreDusts[WhichOre], 0, 0, 0, default, Main.rand.NextFloat(0.75f, 1.5f));
                Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-0.5f, 3) * hit.HitDirection, Main.rand.NextFloat(-1, -3));
            }
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, Main.rand.Next(100, 200), default, Main.rand.NextFloat(1, 1.5f));
                Main.dust[d].color = OreColor[WhichOre] * 0.3f;
                Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1.5f, 5) * hit.HitDirection, Main.rand.NextFloat(-1, -5));
            }
            for (int i = 0; i < 1; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, OreDusts[WhichOre], 0, 0, 0, default, Main.rand.NextFloat(0.75f, 1.5f));
                Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1f, 4) * hit.HitDirection, Main.rand.NextFloat(-1, -4));
            }
        }
    }
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 2;
        Data.Sets.NPC.Earthen[NPC.type] = true;
    }

    public override void SetDefaults()
    {
        NPC.damage = 60;
        NPC.lifeMax = 750;
        NPC.defense = 20;
        NPC.width = 52;
        NPC.aiStyle = 1;
        NPC.value = 1000f;
        NPC.knockBackResist = 0.07f;
        NPC.height = 32;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.alpha = 128;
        NPC.scale = 1f;
        //Banner = NPC.type;
        //BannerItem = ModContent.ItemType<AdamantiteSlimeBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.OreSlime")),
        });
    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.65f);
        NPC.damage = (int)(NPC.damage * 0.45f);
    }
    public override void FindFrame(int frameHeight)
    {
        int num2 = 0;
        if (NPC.aiAction == 0)
        {
            if (NPC.velocity.Y < 0f)
            {
                num2 = 2;
            }
            else if (NPC.velocity.Y > 0f)
            {
                num2 = 3;
            }
            else if (NPC.velocity.X != 0f)
            {
                num2 = 1;
            }
            else
            {
                num2 = 0;
            }
        }
        else if (NPC.aiAction == 1)
        {
            num2 = 4;
        }

        NPC.frameCounter += 1.0;
        if (num2 > 0)
        {
            NPC.frameCounter += 1.0;
        }

        if (num2 == 4)
        {
            NPC.frameCounter += 1.0;
        }

        if (NPC.frameCounter >= 8.0)
        {
            NPC.frame.Y = NPC.frame.Y + frameHeight;
            NPC.frameCounter = 0.0;
        }

        if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
        {
            NPC.frame.Y = 0;
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.ZoneRockLayerHeight && !spawnInfo.Player.ZoneDungeon && Main.hardMode
            ? 0.2f * AvalonGlobalNPC.ModSpawnRate
            : 0f;
}
