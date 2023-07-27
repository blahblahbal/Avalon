using Avalon.Common;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.GemTrees;

public class TourmalineTree : ModTree
{
    private int xCoord = 0;
    private int yCoord = 0;
   
    public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
    {
        UseSpecialGroups = true,
        SpecialGroupMinimalHueValue = 11f / 72f,
        SpecialGroupMaximumHueValue = 0.25f,
        SpecialGroupMinimumSaturationValue = 0.88f,
        SpecialGroupMaximumSaturationValue = 1f
    };
    public override void SetStaticDefaults()
    {
        //PlantTileId = 583;
        GrowsOnTileId = new int[]
        {
            TileID.Stone
        };
    }

    public override int CreateDust()
    {
        return ModContent.DustType<TourmalineDust>();
    }
    public override Asset<Texture2D> GetTexture()
    {
        return ModContent.Request<Texture2D>("Avalon/Tiles/GemTrees/TourmalineTree");
    }
    //public override int TreeLeaf() => ModContent.Find<ModGore>("Avalon/ContagionTreeLeaf").Type;
    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<TourmalineSapling>();
    }

    public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
    {
        topTextureFrameWidth = 116;
        topTextureFrameHeight = 96;
        // This is where fancy code could go, but let's save that for an advanced example
    }

    // Branch Textures
    public override Asset<Texture2D> GetBranchTextures()
    {
        return ModContent.Request<Texture2D>("Avalon/Tiles/GemTrees/TourmalineTreeBranches");
    }

    // Top Textures
    public override Asset<Texture2D> GetTopTextures()
    {
        return ModContent.Request<Texture2D>("Avalon/Tiles/GemTrees/TourmalineTreeTops");
    }

    public override bool CanDropAcorn()
    {
        if (xCoord != 0 && yCoord != 0)
        {
            int dropItem = ItemID.StoneBlock;
            if (Main.rand.NextBool(10))
            {
                dropItem = ModContent.ItemType<Items.Material.Ores.Tourmaline>();
            }
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(xCoord, yCoord), xCoord * 16, yCoord * 16, 8, 8, dropItem, WorldGen.genRand.Next(2) + 1);
            if (Main.rand.NextBool(2))
            {
                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(xCoord, yCoord), xCoord * 16, yCoord * 16, 8, 8, ModContent.ItemType<Items.Placeable.Tile.TourmalineGemcorn>(), WorldGen.genRand.Next(2) + 1);
            }
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(xCoord, yCoord), xCoord * 16, yCoord * 16, 8, 8, ItemID.StoneBlock, WorldGen.genRand.Next(7, 40));
        }
        return false;
    }
    public override int DropWood()
    {
        return ItemID.None;
    }

    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        xCoord = x;
        yCoord = y;
        //if (Main.rand.NextBool(10))
        //{
        //    Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Durian>());
        //    return false;
        //}
        return false;
    }
}
