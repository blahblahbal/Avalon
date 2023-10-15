using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.Tiles.Ores;

public class PrimordialOre : ModTile
{
    private static Color[] RainbowColors = { Color.Red, Color.Brown, Color.GreenYellow, Color.Cyan, Color.Navy, Color.Magenta, Color.MistyRose, Color.Yellow, Color.Cyan, Color.BlueViolet, Color.Pink };

    //public static int[] NoRequirement = new int[]
    //{
    //    ItemID.CopperOre,
    //    ItemID.TinOre,
    //    ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
    //    ItemID.IronOre,
    //    ItemID.LeadOre,
    //    ModContent.ItemType<Items.Material.Ores.NickelOre>(),
    //    ItemID.SilverOre,
    //    ItemID.TungstenOre,
    //    ModContent.ItemType<Items.Material.Ores.ZincOre>(),
    //    ItemID.GoldOre,
    //    ItemID.PlatinumOre,
    //    ModContent.ItemType<Items.Material.Ores.BismuthOre>()
    //};
    //public static int[] Power55 = new int[]
    //{
    //    ItemID.CopperOre,
    //    ItemID.TinOre,
    //    ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
    //    ItemID.IronOre,
    //    ItemID.LeadOre,
    //    ModContent.ItemType<Items.Material.Ores.NickelOre>(),
    //    ItemID.SilverOre,
    //    ItemID.TungstenOre,
    //    ModContent.ItemType<Items.Material.Ores.ZincOre>(),
    //    ItemID.GoldOre,
    //    ItemID.PlatinumOre,
    //    ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
    //    ItemID.Meteorite,
    //    ItemID.DemoniteOre,
    //    ItemID.CrimtaneOre,
    //    ItemID.Obsidian,
    //    ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
    //};
    //public static int[] Power60 = new int[]
    //{
    //    ItemID.CopperOre,
    //    ItemID.TinOre,
    //    ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
    //    ItemID.IronOre,
    //    ItemID.LeadOre,
    //    ModContent.ItemType<Items.Material.Ores.NickelOre>(),
    //    ItemID.SilverOre,
    //    ItemID.TungstenOre,
    //    ModContent.ItemType<Items.Material.Ores.ZincOre>(),
    //    ItemID.GoldOre,
    //    ItemID.PlatinumOre,
    //    ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
    //    ItemID.Meteorite,
    //    ItemID.DemoniteOre,
    //    ItemID.CrimtaneOre,
    //    ItemID.Obsidian,
    //    ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
    //    ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
    //};
    //public static int[] Power70 = new int[]
    //{
    //    ItemID.CopperOre,
    //    ItemID.TinOre,
    //    ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
    //    ItemID.IronOre,
    //    ItemID.LeadOre,
    //    ModContent.ItemType<Items.Material.Ores.NickelOre>(),
    //    ItemID.SilverOre,
    //    ItemID.TungstenOre,
    //    ModContent.ItemType<Items.Material.Ores.ZincOre>(),
    //    ItemID.GoldOre,
    //    ItemID.PlatinumOre,
    //    ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
    //    ItemID.Meteorite,
    //    ItemID.DemoniteOre,
    //    ItemID.CrimtaneOre,
    //    ItemID.Obsidian,
    //    ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
    //    ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
    //    ItemID.Hellstone
    //};
    //public static int[] Power100 = new int[]
    //{
    //    ItemID.CopperOre,
    //    ItemID.TinOre,
    //    ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
    //    ItemID.IronOre,
    //    ItemID.LeadOre,
    //    ModContent.ItemType<Items.Material.Ores.NickelOre>(),
    //    ItemID.SilverOre,
    //    ItemID.TungstenOre,
    //    ModContent.ItemType<Items.Material.Ores.ZincOre>(),
    //    ItemID.GoldOre,
    //    ItemID.PlatinumOre,
    //    ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
    //    ItemID.Meteorite,
    //    ItemID.DemoniteOre,
    //    ItemID.CrimtaneOre,
    //    ItemID.Obsidian,
    //    ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
    //    ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
    //    ItemID.Hellstone,
    //    ItemID.CobaltOre,
    //    ItemID.PalladiumOre,
    //    //ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
    //};
    //public static int[] Power110 = new int[]
    //{
    //    ItemID.CopperOre,
    //    ItemID.TinOre,
    //    ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
    //    ItemID.IronOre,
    //    ItemID.LeadOre,
    //    ModContent.ItemType<Items.Material.Ores.NickelOre>(),
    //    ItemID.SilverOre,
    //    ItemID.TungstenOre,
    //    ModContent.ItemType<Items.Material.Ores.ZincOre>(),
    //    ItemID.GoldOre,
    //    ItemID.PlatinumOre,
    //    ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
    //    ItemID.Meteorite,
    //    ItemID.DemoniteOre,
    //    ItemID.CrimtaneOre,
    //    ItemID.Obsidian,
    //    ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
    //    ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
    //    ItemID.Hellstone,
    //    ItemID.CobaltOre,
    //    ItemID.PalladiumOre,
    //    //ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
    //    ItemID.MythrilOre,
    //    ItemID.OrichalcumOre,
    //    //ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
    //};
    //public static int[] Power150 = new int[]
    //{
    //    ItemID.CopperOre,
    //    ItemID.TinOre,
    //    ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
    //    ItemID.IronOre,
    //    ItemID.LeadOre,
    //    ModContent.ItemType<Items.Material.Ores.NickelOre>(),
    //    ItemID.SilverOre,
    //    ItemID.TungstenOre,
    //    ModContent.ItemType<Items.Material.Ores.ZincOre>(),
    //    ItemID.GoldOre,
    //    ItemID.PlatinumOre,
    //    ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
    //    ItemID.Meteorite,
    //    ItemID.DemoniteOre,
    //    ItemID.CrimtaneOre,
    //    ItemID.Obsidian,
    //    ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
    //    ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
    //    ItemID.Hellstone,
    //    ItemID.CobaltOre,
    //    ItemID.PalladiumOre,
    //    //ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
    //    ItemID.MythrilOre,
    //    ItemID.OrichalcumOre,
    //    //ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
    //    ItemID.AdamantiteOre,
    //    ItemID.TitaniumOre,
    //    //ModContent.ItemType<Items.Material.Ores.TroxiniumOre>(),
    //};
    //public static int[] Power200 = new int[]
    //{
    //    ItemID.CopperOre,
    //    ItemID.TinOre,
    //    ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
    //    ItemID.IronOre,
    //    ItemID.LeadOre,
    //    ModContent.ItemType<Items.Material.Ores.NickelOre>(),
    //    ItemID.SilverOre,
    //    ItemID.TungstenOre,
    //    ModContent.ItemType<Items.Material.Ores.ZincOre>(),
    //    ItemID.GoldOre,
    //    ItemID.PlatinumOre,
    //    ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
    //    ItemID.Meteorite,
    //    ItemID.DemoniteOre,
    //    ItemID.CrimtaneOre,
    //    ItemID.Obsidian,
    //    ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
    //    ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
    //    ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
    //    ItemID.Hellstone,
    //    ItemID.CobaltOre,
    //    ItemID.PalladiumOre,
    //    //ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
    //    ItemID.MythrilOre,
    //    ItemID.OrichalcumOre,
    //    //ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
    //    ItemID.AdamantiteOre,
    //    ItemID.TitaniumOre,
    //    //ModContent.ItemType<Items.Material.Ores.TroxiniumOre>(),
    //    ItemID.ChlorophyteOre,
    //    //ModContent.ItemType<Items.Material.Ores.XanthophyteOre>(),
    //    ModContent.ItemType<Items.Material.Ores.CaesiumOre>(),
    //};
    // add more tiers later

    public override void SetStaticDefaults()
    {
        TileID.Sets.Ore[Type] = true;
        AddMapEntry(new Color(255, 0, 0), LanguageManager.Instance.GetText("Primordial Ore")); // new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)
        AddMapEntry(new Color(255, 128, 0), LanguageManager.Instance.GetText("Primordial Ore"));
        AddMapEntry(new Color(255, 255, 0), LanguageManager.Instance.GetText("Primordial Ore"));
        AddMapEntry(new Color(0, 255, 0), LanguageManager.Instance.GetText("Primordial Ore"));
        AddMapEntry(new Color(0, 255, 255), LanguageManager.Instance.GetText("Primordial Ore"));
        AddMapEntry(new Color(0, 128, 255), LanguageManager.Instance.GetText("Primordial Ore"));
        AddMapEntry(new Color(0, 0, 255), LanguageManager.Instance.GetText("Primordial Ore"));
        AddMapEntry(new Color(128, 0, 255), LanguageManager.Instance.GetText("Primordial Ore"));
        AddMapEntry(new Color(255, 0, 255), LanguageManager.Instance.GetText("Primordial Ore"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 815;
        HitSound = SoundID.Tink;
        DustType = DustID.ShimmerSpark;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        MineResist = 3f;
    }

    public override ushort GetMapOption(int i, int j)
    {
        return (ushort)((i + j) % 9);
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        Asset<Texture2D> texture = TextureAssets.Tile[Type];

        // if (Main.canDrawColorTile(i, j))
        // {
        //     texture = Main.tileAltTexture[Type, tile.color()];
        // }
        // else
        // {
        //     texture = Main.tileTexture[Type];
        // }
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        //Color[] RainbowColors = { Color.Red, Color.Yellow, Color.Lime, Color.Cyan, Color.Blue, Color.Magenta };
        Color Skittles = GetRainbow(i, j);

        spriteBatch.Draw(texture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), Skittles, 0f, default, 1f, SpriteEffects.None, 0f) ;

        Skittles = Color.Lerp(Color.Black, Main.DiscoColor, (float)Math.Sin((Main.timeForVisualEffects * 0.02f) + i / 10f - j / 20f)) * 1f;
        //Skittles = Color.Lerp(Color.Black, Skittles, Lighting.Brightness(i, j) * 2f);
        Skittles.A = 0;
        spriteBatch.Draw(texture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), Skittles, 0f, default, 1f, SpriteEffects.None, 0f);
    }
    static Color GetRainbow(int i, int j)
    {
        //if (WorldGen.currentWorldSeed == "for the worthy" || Terraria.WorldGen.currentWorldSeed == "fortheworthy")
        //{
        //    return Color.Lerp(Color.Red, new Color(16, 16, 16), (float)Math.Sin(Main.timeForVisualEffects * 0.05f + i * 0.2) + (float)Math.Cos(Main.timeForVisualEffects * 0.03f + j * 0.2f));
        //}
        //else
            return ClassExtensions.CycleThroughColors(RainbowColors, 100, i * 10 + j * 10);
    } 
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        //Color[] RainbowColors = { Color.Red, Color.Yellow, Color.Lime, Color.Cyan, Color.Blue, Color.Magenta };
        Color Skittles = GetRainbow(i, j);

        Skittles = Color.Lerp(Skittles, new Color(Skittles.R + Main.DiscoColor.R, Skittles.G + Main.DiscoColor.G, Skittles.B + Main.DiscoColor.B), (float)Math.Sin((Main.timeForVisualEffects * 0.02f) + i / 10f - j / 20f)) * 1f;

        Skittles *= 0.9f;
        r = Skittles.R / 255f;
        g = Skittles.G / 255f;
        b = Skittles.B / 255f;
    }
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        int[] NoRequirement = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
            ItemID.IronOre,
            ItemID.LeadOre,
            ModContent.ItemType<Items.Material.Ores.NickelOre>(),
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ModContent.ItemType<Items.Material.Ores.ZincOre>(),
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ModContent.ItemType<Items.Material.Ores.BismuthOre>()
        };
        int[] Power55 = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
            ItemID.IronOre,
            ItemID.LeadOre,
            ModContent.ItemType<Items.Material.Ores.NickelOre>(),
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ModContent.ItemType<Items.Material.Ores.ZincOre>(),
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ItemID.Obsidian,
            ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
        };
        int[] Power60 = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
            ItemID.IronOre,
            ItemID.LeadOre,
            ModContent.ItemType<Items.Material.Ores.NickelOre>(),
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ModContent.ItemType<Items.Material.Ores.ZincOre>(),
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ItemID.Obsidian,
            ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
            ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
            ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
            ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
        };
        int[] Power70 = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
            ItemID.IronOre,
            ItemID.LeadOre,
            ModContent.ItemType<Items.Material.Ores.NickelOre>(),
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ModContent.ItemType<Items.Material.Ores.ZincOre>(),
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ItemID.Obsidian,
            ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
            ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
            ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
            ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
            ItemID.Hellstone
        };
        int[] Power100 = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
            ItemID.IronOre,
            ItemID.LeadOre,
            ModContent.ItemType<Items.Material.Ores.NickelOre>(),
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ModContent.ItemType<Items.Material.Ores.ZincOre>(),
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ItemID.Obsidian,
            ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
            ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
            ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
            ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
            ItemID.Hellstone,
            ItemID.CobaltOre,
            ItemID.PalladiumOre,
            ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
        };
        int[] Power110 = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
            ItemID.IronOre,
            ItemID.LeadOre,
            ModContent.ItemType<Items.Material.Ores.NickelOre>(),
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ModContent.ItemType<Items.Material.Ores.ZincOre>(),
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ItemID.Obsidian,
            ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
            ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
            ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
            ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
            ItemID.Hellstone,
            ItemID.CobaltOre,
            ItemID.PalladiumOre,
            ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
            ItemID.MythrilOre,
            ItemID.OrichalcumOre,
            ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
        };
        int[] Power150 = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
            ItemID.IronOre,
            ItemID.LeadOre,
            ModContent.ItemType<Items.Material.Ores.NickelOre>(),
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ModContent.ItemType<Items.Material.Ores.ZincOre>(),
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ItemID.Obsidian,
            ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
            ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
            ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
            ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
            ItemID.Hellstone,
            ItemID.CobaltOre,
            ItemID.PalladiumOre,
            ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
            ItemID.MythrilOre,
            ItemID.OrichalcumOre,
            ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
            ItemID.AdamantiteOre,
            ItemID.TitaniumOre,
            ModContent.ItemType<Items.Material.Ores.TroxiniumOre>(),
        };
        int[] Power200 = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
            ItemID.IronOre,
            ItemID.LeadOre,
            ModContent.ItemType<Items.Material.Ores.NickelOre>(),
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ModContent.ItemType<Items.Material.Ores.ZincOre>(),
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ItemID.Obsidian,
            ModContent.ItemType<Items.Material.Ores.BacciliteOre>(),
            ModContent.ItemType<Items.Material.Ores.RhodiumOre>(),
            ModContent.ItemType<Items.Material.Ores.OsmiumOre>(),
            ModContent.ItemType<Items.Material.Ores.IridiumOre>(),
            ItemID.Hellstone,
            ItemID.CobaltOre,
            ItemID.PalladiumOre,
            ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
            ItemID.MythrilOre,
            ItemID.OrichalcumOre,
            ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
            ItemID.AdamantiteOre,
            ItemID.TitaniumOre,
            ModContent.ItemType<Items.Material.Ores.TroxiniumOre>(),
            ItemID.ChlorophyteOre,
            //ModContent.ItemType<Items.Material.Ores.XanthophyteOre>(),
            ModContent.ItemType<Items.Material.Ores.CaesiumOre>(),
        };


        int itemType = 0;
        int stackSize = 1;

        Player p = Main.player[Player.FindClosest(new(i * 16, j * 16), 16, 16)];

        int power = 0;
        for (int x = 0; x < 58; x++)
        {
            if (p.inventory[x].pick > 0 || p.inventory[x].pick > power)
            {
                power = p.inventory[x].pick;
            }
        }

        if (power < 55)
        {
            stackSize = 10;
            itemType = Main.rand.NextFromList(NoRequirement);
        }
        else if (power < 60)
        {
            stackSize = 8;
            itemType = Main.rand.NextFromList(Power55);
        }
        else if (power < 70)
        {
            stackSize = 7;
            itemType = Main.rand.NextFromList(Power60);
        }
        else if (power < 100)
        {
            stackSize = 6;
            itemType = Main.rand.NextFromList(Power70);
        }
        else if (power < 110)
        {
            stackSize = 5;
            itemType = Main.rand.NextFromList(Power100);
        }
        else if (power < 150)
        {
            stackSize = 5;
            itemType = Main.rand.NextFromList(Power110);
        }
        else if (power < 200)
        {
            stackSize = 5;
            itemType = Main.rand.NextFromList(Power150);
        }
        else // if
        {
            stackSize = 4;
            itemType = Main.rand.NextFromList(Power200);
        }
        yield return new Item(itemType, stackSize);
    }
}
