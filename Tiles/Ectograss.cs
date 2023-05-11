using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class Ectograss : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(27, 194, 254));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBlendAll[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileLighted[Type] = true;
        TileID.Sets.NeedsGrassFraming[Type] = true;
        TileID.Sets.NeedsGrassFramingDirt[Type] = TileID.Ash;
        TileID.Sets.Grass[Type] = true;
        RegisterItemDrop(ItemID.AshBlock);
        DustType = DustID.Silt;
    }
    // still needs work lol
    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }

        Vector2 pos = new Vector2(i * 16, j * 16) + zero - Main.screenPosition;
        var frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
        var halfFrame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, -8);
        Texture2D tex = Mod.Assets.Request<Texture2D>("Tiles/Ectograss_Glow").Value;
        if (tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
        {
            Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/Ectograss_Glow").Value, pos, frame, Color.White);
        }
        else if (tile.IsHalfBlock)
        {
            Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/Ectograss_Glow").Value, pos, halfFrame, Color.White);
        }
        else
        {
            Vector2 screenOffset = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                screenOffset = Vector2.Zero;
            }
            Vector2 vector = new Vector2(i * 16 - (int)Main.screenPosition.X - ((float) - 16f) / 2f, j * 16 - (int)Main.screenPosition.Y + 0 + 8) + screenOffset;
            int slopeType = (int)tile.Slope;
            int num5 = 2;
            for (int q = 0; q < 8; q++)
            {
                int num6 = q * -2;
                int num7 = 16 - q * 2;
                int num8 = 16 - num7;
                int num9;
                switch (slopeType)
                {
                    case 1:
                        num6 = 0;
                        num9 = q * 2;
                        num7 = 14 - q * 2;
                        num8 = 0;
                        break;
                    case 2:
                        num6 = 0;
                        num9 = 16 - q * 2 - 2;
                        num7 = 14 - q * 2;
                        num8 = 0;
                        break;
                    case 3:
                        num9 = q * 2;
                        break;
                    default:
                        num9 = 16 - q * 2 - 2;
                        break;
                }
                Main.spriteBatch.Draw(tex, vector + new Vector2(num9, q * num5 + num6), (Rectangle?)new Rectangle(tile.TileFrameX + 0 + num9, tile.TileFrameY + 8 + num8, num5, num7), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            int num10 = ((slopeType <= 2) ? 14 : 0);
            Main.spriteBatch.Draw(tex, vector + new Vector2(0f, num10), (Rectangle?)new Rectangle(tile.TileFrameX + 8, tile.TileFrameY + 8 + num10, 16, 2), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 35f / 255f;
        g = 200f / 255f;
        b = 254f / 255f;
    }
    //public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    //{
    //    if (fail && !effectOnly)
    //    {
    //        Main.tile[i, j].TileType = TileID.Ash;
    //        WorldGen.SquareTileFrame(i, j);
    //    }
    //}
}
