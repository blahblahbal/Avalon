using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.PreHardmode;

public class OreSlime : ModNPC
{
    int[] Ores =
    { 
        ItemID.CopperOre, ItemID.TinOre, ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
        ItemID.IronOre, ItemID.LeadOre, ModContent.ItemType<Items.Material.Ores.NickelOre>(),
        ItemID.SilverOre, ItemID.TungstenOre, ModContent.ItemType<Items.Material.Ores.ZincOre>(),
        ItemID.GoldOre, ItemID.PlatinumOre, ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
        ItemID.Obsidian
    };
    Color[] OreColor =
    {
        new Color(183, 88, 25), new Color(187, 165, 124), new Color(193, 133, 127),
        new Color(181, 164, 149), new Color(62, 82, 114), new Color(107, 158, 149),
        new Color(179, 179, 179), new Color(154, 190, 155), new Color(182, 169, 182),
        new Color(231, 213, 65), new Color(181, 194, 217), new Color(173, 58, 191),
        Color.DarkSlateBlue
    };
    int[] OreDusts =
    {
        DustID.Copper, DustID.Tin, ModContent.DustType<Dusts.BronzeDust>(),
        DustID.Iron, DustID.Lead, ModContent.DustType<Dusts.NickelDust>(),
        DustID.Silver, DustID.Tungsten, ModContent.DustType<Dusts.ZincDust>(),
        DustID.Gold, DustID.Platinum, ModContent.DustType<Dusts.BismuthDust>(),
        DustID.Obsidian
    };
    int WhichOre;
    public override void OnSpawn(IEntitySource source)
    {
        WhichOre = Main.rand.Next(0, Ores.Length);
        NPC.alpha = 100;
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
        //int colorClampR = (int)MathHelper.Clamp(OreColor[WhichOre].R, colorAvg - 60, colorAvg + 60);
        //int colorClampG = (int)MathHelper.Clamp(OreColor[WhichOre].G, colorAvg - 60, colorAvg + 60);
        //int colorClampB = (int)MathHelper.Clamp(OreColor[WhichOre].B, colorAvg - 60, colorAvg + 60);
        //drawColor.R = (byte)MathHelper.Clamp(colorClampR * (lightColor.R * 1.4f) / 255f, 0, 255);
        //drawColor.G = (byte)MathHelper.Clamp(colorClampG * (lightColor.G * 1.4f) / 255f, 0, 255);
        //drawColor.B = (byte)MathHelper.Clamp(colorClampB * (lightColor.B * 1.4f) / 255f, 0, 255);
        //drawColor.A = 10;
        drawColor.R = (byte)MathHelper.Clamp(OreColor[WhichOre].R * (lightColor.R * 1.5f) / 255f, 0, 255);
        drawColor.G = (byte)MathHelper.Clamp(OreColor[WhichOre].G * (lightColor.G * 1.5f) / 255f, 0, 255);
        drawColor.B = (byte)MathHelper.Clamp(OreColor[WhichOre].B * (lightColor.B * 1.5f) / 255f, 0, 255);
        drawColor.A = (byte)(NPC.alpha * lightColor.A);
    }
    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        for (int i = 0; i < Ores.Length; i++)
        {
            Main.instance.LoadItem(Ores[i]);
        }
        float rotate = MathHelper.SmoothStep(0.1f, -0.1f, Main.masterColor);
        Asset<Texture2D> oreTexture;
        if (Ores[WhichOre] > ItemID.Count)
        {
            if (Ores[WhichOre] == ModContent.ItemType<Items.Material.Ores.BronzeOre>())
            {
                oreTexture = BronzeOreTexture;
            }
            else if (Ores[WhichOre] == ModContent.ItemType<Items.Material.Ores.NickelOre>())
            {
                oreTexture = NickelOreTexture;
            }
            else if (Ores[WhichOre] == ModContent.ItemType<Items.Material.Ores.ZincOre>())
            {
                oreTexture = ZincOreTexture;
            }
            else if (Ores[WhichOre] == ModContent.ItemType<Items.Material.Ores.BismuthOre>())
            {
                oreTexture = BismuthOreTexture;
            }
            else oreTexture = TextureAssets.Item[ItemID.CopperOre]; // will never get here but VS is stupid
        }
        else
            oreTexture = TextureAssets.Item[Ores[WhichOre]];//(Texture2D)ModContent.Request<Texture2D>($"Terraria/Images/Item_{Ores[WhichOre]}");
        Rectangle frame = oreTexture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Main.EntitySpriteDraw(oreTexture.Value, NPC.Center - Main.screenPosition + new Vector2(0, NPC.frame.Y * -0.05f), frame, drawColor, NPC.rotation + rotate, frameOrigin, NPC.scale, SpriteEffects.None);
        return base.PreDraw(spriteBatch, screenPos, drawColor);
    }
    public override void OnKill()
    {
        Item.NewItem(NPC.GetSource_FromThis(), NPC.Hitbox, Ores[WhichOre], Main.rand.Next(15, 35));
		Item.NewItem(NPC.GetSource_FromThis(), NPC.Hitbox, ItemID.Gel, Main.rand.Next(3, 6));
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
	private static Asset<Texture2D> BronzeOreTexture;
	private static Asset<Texture2D> NickelOreTexture;
	private static Asset<Texture2D> ZincOreTexture;
	private static Asset<Texture2D> BismuthOreTexture;
	public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 2;
        Data.Sets.NPCSets.Earthen[NPC.type] = true;
		BronzeOreTexture = TextureAssets.Item[ModContent.ItemType<Items.Material.Ores.BronzeOre>()];
		NickelOreTexture = TextureAssets.Item[ModContent.ItemType<Items.Material.Ores.NickelOre>()];
		ZincOreTexture = TextureAssets.Item[ModContent.ItemType<Items.Material.Ores.ZincOre>()];
		BismuthOreTexture = TextureAssets.Item[ModContent.ItemType<Items.Material.Ores.BismuthOre>()];
	}

    public override void SetDefaults()
    {
        NPC.damage = 20;
        NPC.lifeMax = 200;
        NPC.defense = 6;
        NPC.width = 36;
        NPC.aiStyle = 1;
        NPC.value = 1000f;
        NPC.knockBackResist = 0.1f;
        NPC.height = 24;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.alpha = 128;
        //Banner = NPC.type;
        //BannerItem = ModContent.ItemType<AdamantiteSlimeBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.OreSlime"))
        });
    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.65f);
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

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.ZoneUndergroundDesert)
        {
            return 0.02f;
        }
        return spawnInfo.Player.ZoneRockLayerHeight && !spawnInfo.Player.ZoneDungeon ? 0.06f : 0f;
    }
}
