using Avalon.Common.Templates;
using Avalon.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture;

public class BronzeChandelier : ChandelierTemplate
{
    public override Color FlameColor => base.FlameColor;
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f;
            g = 0.95f;
            b = 0.65f;
        }
    }
}
