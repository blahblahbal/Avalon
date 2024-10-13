using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class LaziteGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(3, 2, 209));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBlendAll[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[Type][ModContent.TileType<BlastedStone>()] = true;
        Main.tileMerge[ModContent.TileType<BlastedStone>()][Type] = true;
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Tile.BlastedStone>());
        TileID.Sets.NeedsGrassFraming[Type] = true;
        TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<BlastedStone>();
        DustType = DustID.GemSapphire;
        HitSound = SoundID.Tink;
    }
    public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
    {
        // This allows us to draw things in SpecialDraw
        if (drawData.tileFrameX >= 0 && drawData.tileFrameY >= 0)
        {
            Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
        }
    }
    public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }

        Vector2 pos = new Vector2(i * 16, j * 16) + zero - Main.screenPosition;
        var frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
        var halfFrame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 8);
        Texture2D tex = Mod.Assets.Request<Texture2D>("Tiles/LaziteGrass_Glow").Value;
        var colors = new List<Color> { new(80, 80, 80), new(145, 145, 145) };
        int num = (int)(Main.GlobalTimeWrappedHourly / 135f % colors.Count);
        int index = (int)(Main.GameUpdateCount / 135 % colors.Count);
        int nextIndex = (index + 1) % colors.Count;
        Color colorShift = TileGlowDrawing.ActuatedColor(Color.Lerp(colors[index], colors[nextIndex], Main.GameUpdateCount % 135 / 135f), tile);
        if (tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
        {
            Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/LaziteGrass_Glow").Value, pos, frame, colorShift);
        }
        else if (tile.IsHalfBlock)
        {
            pos = new Vector2(i * 16, (j * 16) + 8) + zero - Main.screenPosition;
            Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/LaziteGrass_Glow").Value, pos, halfFrame, colorShift);
        }
        else
        {
            Vector2 screenOffset = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                screenOffset = Vector2.Zero;
            }
            Vector2 vector = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + screenOffset;
            int slopeType = (int)tile.Slope;
            int num5 = 2;
            int addFrY = Main.tileFrame[Type] * 90;
            int addFrX = 0;
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
                Main.spriteBatch.Draw(tex, vector + new Vector2(num9, q * num5 + num6), (Rectangle?)new Rectangle(tile.TileFrameX + addFrX + num9, tile.TileFrameY + addFrY + num8, num5, num7), colorShift, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            int num10 = ((slopeType <= 2) ? 14 : 0);
            Main.spriteBatch.Draw(tex, vector + new Vector2(0f, num10), (Rectangle?)new Rectangle(tile.TileFrameX + addFrX, tile.TileFrameY + addFrY + num10, 16, 2), colorShift, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail && !effectOnly)
        {
            Main.tile[i, j].TileType = (ushort)ModContent.TileType<BlastedStone>();
            WorldGen.SquareTileFrame(i, j);
        }
    }
}
