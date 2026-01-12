using Avalon;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.Crafting;

public class TroxiniumForge : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(255, 216, 0));
        AnimationFrameHeight = 38;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        DustType = ModContent.DustType<TroxiniumDust>();
        AdjTiles = new int[] { TileID.AdamantiteForge, TileID.Hellforge, TileID.Furnaces };
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.83f;
        g = 0.6f;
        b = 0.5f;
    }

    public override void RandomUpdate(int i, int j)
    {
        if (Main.rand.NextBool(40))
        {
            int num306 = Dust.NewDust(new Vector2((j * 16) - 4, (i * 16) - 6), 8, 6, DustID.Torch, 0f, 0f, 100);
            if (!Main.rand.NextBool(3))
            {
                Main.dust[num306].noGravity = true;
            }
        }
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        frame = Main.tileFrame[TileID.AdamantiteForge];
    }
}
