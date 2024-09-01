using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Avalon.Tiles.Savanna;

public class SavannaTree : ModTree
{
    public override TreePaintingSettings TreeShaderSettings => new();
    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<SavannaGrass>() };

    public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
    {
        topTextureFrameWidth = 116;
        topTextureFrameHeight = 96;
    }

    public override Asset<Texture2D> GetTexture() => ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Tiles/Savanna/SavannaTree");

    public override Asset<Texture2D> GetBranchTextures() =>
        ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Tiles/Savanna/SavannaTreeBranches");

    public override Asset<Texture2D> GetTopTextures() =>
        ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Tiles/Savanna/SavannaTreeTop");

    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.BleachedEbony>();

    public override int CreateDust() => 51;
    public override int TreeLeaf() => ModContent.GoreType<SavannaTreeLeaf>();
    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        if (Main.rand.NextBool(10))
        {
            if (Main.rand.NextBool(2))
            {
                Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Mangosteen>());
            }
            else Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Raspberry>());
            return false;
        }
        return true;
    }
    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<SavannaSapling>();
    }
}
