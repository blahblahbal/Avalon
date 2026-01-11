using Avalon.Common;
using Avalon.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.GameContent.Drawing.TileDrawing;

namespace Avalon.Tiles.Herbs;

public class LargeHerbsStage4 : ModTile
{
	public override string Texture => ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement ? $"Avalon/Tiles/Herbs/{Name}_Alt" : base.Texture;
	public override void SetStaticDefaults()
    {
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
        AddMapEntry(new Color(246, 197, 26), this.GetLocalization("MapEntry0"));
        AddMapEntry(new Color(76, 150, 216), this.GetLocalization("MapEntry1"));
        AddMapEntry(new Color(185, 214, 42), this.GetLocalization("MapEntry2"));
        AddMapEntry(new Color(167, 203, 37), this.GetLocalization("MapEntry3"));
        AddMapEntry(new Color(72, 145, 125), this.GetLocalization("MapEntry4"));
        AddMapEntry(new Color(177, 69, 49), this.GetLocalization("MapEntry5"));
        AddMapEntry(new Color(40, 152, 240), this.GetLocalization("MapEntry6"));
        AddMapEntry(Color.IndianRed, this.GetLocalization("MapEntry7"));
        AddMapEntry(new Color(216, 161, 50), this.GetLocalization("MapEntry8"));
        AddMapEntry(new Color(0, 200, 50), this.GetLocalization("MapEntry9"));
        AddMapEntry(new Color(75, 184, 230), this.GetLocalization("MapEntry10"));
        AddMapEntry(new Color(191, 0, 81), this.GetLocalization("MapEntry11"));
    }
    public override ushort GetMapOption(int i, int j)
    {
        return (ushort)(Main.tile[i, j].TileFrameX / 18);
    }
    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        bool intoRenderTargets = true;
        bool flag = intoRenderTargets || Main.LightingEveryFrame;

        if (Main.tile[i, j].TileFrameX % 18 == 0 && Main.tile[i, j].TileFrameY % 54 == 0 && flag)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileCounterType.MultiTileGrass);
        }

        return false;
    }
    public override bool CreateDust(int i, int j, ref int type)
    {
        switch (Main.tile[i, j].TileFrameX / 18)
        {
            case 0:
            case 1:
            case 9:
                type = DustID.GrassBlades;
                break;
            case 2:
                type = DustID.WoodFurniture;
                break;
            case 3:
                type = DustID.CorruptPlants;
                break;
            case 4:
            case 11:
                type = DustID.SeaOatsOasis;
                break;
            case 5:
                type = DustID.Torch;
                break;
            case 6:
                type = DustID.Shiverthorn;
                break;
            case 7:
                type = DustID.CrimsonPlants;
                break;
            case 8:
                type = DustID.GoldCritter_LessOutline;
                break;
            case 10:
                type = DustID.EnchantedNightcrawler;
                break;
        }
        return base.CreateDust(i, j, ref type);
    }
}
