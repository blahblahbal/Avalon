using Avalon.Dusts;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Avalon.Tiles.Contagion;

public class ContagionPalmTree : ModPalmTree
{
    public override TreePaintingSettings TreeShaderSettings => new();

    public override void SetStaticDefaults() => GrowsOnTileId = new[] { ModContent.TileType<Snotsand>() };

    public override Asset<Texture2D> GetOasisTopTextures() =>
        ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Tiles/Contagion/ContagionOasisTree_Tops");

    public override Asset<Texture2D> GetTexture() =>
        ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Tiles/Contagion/ContagionPalmTree");

    public override Asset<Texture2D> GetTopTextures() =>
        ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>("Tiles/Contagion/ContagionPalmTreeTop");

    public override int DropWood() => ModContent.ItemType<Items.Placeable.Tile.Coughwood>();

    public override int CreateDust() => ModContent.DustType<CoughwoodDust>();
    //public override int TreeLeaf() => ModContent.Find<ModGore>("Avalon/ContagionTreeLeaf").Type;
    public override bool Shake(int x, int y, ref bool createLeaves)
    {
        if (Main.rand.NextBool(10))
        {
            Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Food.Durian>());
            return false;
        }
        return true;
    }
    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<ContagionPalmSapling>();
    }
}
