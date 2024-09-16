using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Avalon.Tiles;

public class WarmGemsparkBlock : ModTile
{
    public static int G { get; private set; } = 0;
    private static int style = 0;

    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.OrangeRed);
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        DustType = DustID.Crimstone;
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        Texture2D texture;
        //if (Main.canDrawColorTile(i, j))
        //{
        //    texture = Main.tileAltTexture[Type, (int)tile.color()];
        //}
        //else
        //{
        texture = TextureAssets.Tile[Type].Value;
        //}
        Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        Main.spriteBatch.Draw(texture, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), new Color(255, G, 0), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
    }

    public static void StaticUpdate()
    {
        if (style == 0)
        {
            G += 5;
            if (G >= 255)
            {
                G = 255;
                style = 1;
            }
        }
        if (style == 1)
        {
            G -= 5;
            if (G <= 0)
            {
                G = 0;
                style = 0;
            }
        }
    }
}
