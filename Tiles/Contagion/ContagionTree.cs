using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class ContagionTree : ModTree
{
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
        GrowsOnTileId = new int[1] { ModContent.TileType<Ickgrass>() };
    }

    public override Asset<Texture2D> GetTexture()
    {
        return ModContent.Request<Texture2D>("Avalon/Tiles/Contagion/ContagionTree");
    }
    //public override int TreeLeaf() => ModContent.Find<ModGore>("Avalon/ContagionTreeLeaf").Type;
    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<ContagionSapling>();
    }

    public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
    {
        // This is where fancy code could go, but let's save that for an advanced example
    }

    // Branch Textures
    public override Asset<Texture2D> GetBranchTextures()
    {
        return ModContent.Request<Texture2D>("Avalon/Tiles/Contagion/ContagionTreeBranches");
    }

    // Top Textures
    public override Asset<Texture2D> GetTopTextures()
    {
        return ModContent.Request<Texture2D>("Avalon/Tiles/Contagion/ContagionTreeTop");
    }

    public override int DropWood()
    {
        return ModContent.ItemType<Items.Placeable.Tile.Coughwood>();
    }

    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        if (Main.rand.NextBool(10))
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Durian>());
            return false;
        }
        return true;
    }
}
