using Avalon.Items.Material;
using Avalon.Items.Vanity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Tropics;

public class TropicalShortGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.SlowlyDiesInWater[Type] = true;
        TileID.Sets.SwaysInWindBasic[Type] = true;
        TileID.Sets.DrawFlipMode[Type] = 1;
        TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
        Main.tileCut[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileWaterDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.AnchorValidTiles = new int[1] { ModContent.TileType<TropicalGrass>() };
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<Dusts.TropicalDust>();
        HitSound = SoundID.Grass;
        AddMapEntry(new Color(82, 123, 35));
        AddMapEntry(new Color(82, 123, 35));
        AddMapEntry(new Color(82, 123, 35));
        AddMapEntry(new Color(82, 123, 35));
        AddMapEntry(new Color(82, 123, 35));
        AddMapEntry(new Color(82, 123, 35));
        AddMapEntry(new Color(82, 123, 35));
        AddMapEntry(new Color(82, 123, 35));
        AddMapEntry(new Color(277, 41, 75), this.GetLocalization("MapEntry1"));
        AddMapEntry(new Color(76, 150, 216));
        for (int i = 10; i < 23; i++)
        {
            AddMapEntry(new Color(82, 123, 35));
        }
    }
    //public override ushort GetMapOption(int i, int j)
    //{
    //    return (ushort)(Main.tile[i, j].TileFrameX / 18);
    //}
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (Main.tile[i, j].TileFrameX / 18 == 8)
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<TropicalShroomCap>());
        if (Main.tile[i, j].TileFrameX / 18 == 9)
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.NaturesGift);
        if (Main.tile[i, j].TileFrameX / 18 is 6 or 7 && Main.rand.NextBool(25))
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<TropicsLily>());
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        if (Main.tile[i, j].TileFrameX / 18 == 8)
        {
            Main.tileLighted[Type] = true;
            Color lightColor = new Color(227, 41, 75);
            r = lightColor.R / 200f;
            g = lightColor.G / 200f;
            b = lightColor.B / 200f;
        }
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        height = 20;
        offsetY = -2;
        tileFrameY = 0;
    }
    public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
    {
        if (i % 2 == 1)
        {
            effects = SpriteEffects.FlipHorizontally;
        }
    }
}
