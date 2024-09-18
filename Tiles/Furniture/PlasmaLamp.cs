using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture;

public class PlasmaLamp : ModTile
{
	private static Asset<Texture2D>? flameTexture;
	public override void SetStaticDefaults()
    {
        AnimationFrameHeight = 38;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
		flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
	}

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 0.6f;
            g = 0.1f;
            b = 0.6f;
        }
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        frameCounter++;
        if (frameCounter > 5)
        {
            frameCounter = 0;
            frame++;
            if (frame == 3) frame = 0;
        }
    }
    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<Items.Placeable.Furniture.PlasmaLamp>());
    }

    public override void HitWire(int i, int j)
    {
        int x = i - Main.tile[i, j].TileFrameX / 18 % 2;
        int y = j - Main.tile[i, j].TileFrameY / 18 % 2;
        for (int l = x; l < x + 2; l++)
        {
            for (int m = y; m < y + 2; m++)
            {
                if (Main.tile[l, m].HasTile && Main.tile[l, m].TileType == Type)
                {
                    if (Main.tile[l, m].TileFrameX < 36)
                    {
                        Main.tile[l, m].TileFrameX += 36;
                    }
                    else
                    {
                        Main.tile[l, m].TileFrameX -= 36;
                    }
                }
            }
        }
        if (Wiring.running)
        {
            Wiring.SkipWire(x, y);
            Wiring.SkipWire(x, y + 1);
            Wiring.SkipWire(x + 1, y);
            Wiring.SkipWire(x + 1, y + 1);
        }
        NetMessage.SendTileSquare(-1, x, y + 1, 2);
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        Color color = new Color(196, 142, 238, 0);
        int frameX = Main.tile[i, j].TileFrameX;
        int frameY = Main.tile[i, j].TileFrameY;
        int width = 18;
        int offsetY = 0;
        int offsetX = 1;
        int height = frameY % AnimationFrameHeight == 38 ? 18 : 16;
        int animate = Main.tileFrame[Type] * AnimationFrameHeight;
        Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        for (int k = 0; k < 7; k++)
        {
            float x = (float)Utils.RandomInt(ref randSeed, -5, 5) * 0.15f;
            float y = (float)Utils.RandomInt(ref randSeed, -5, 5) * 0.15f;
            Main.spriteBatch.Draw(flameTexture.Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X + offsetX) - (width - 16f) / 2f + x, (float)(j * 16 - (int)Main.screenPosition.Y + offsetY) + y) + zero, new Rectangle(frameX, frameY + animate, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }
    }
}
