using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.DarkMatter;

public class DarkMatterMonolith : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.Origin = new Point16(1, 2);
        TileObjectData.newTile.StyleWrapLimit = 36;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(126, 71, 107));
        DustType = ModContent.DustType<DarkMatterDust>();
        AnimationFrameHeight = 56;
        TileID.Sets.DisableSmartCursor[Type] = true;
    }

    //public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(
    //    WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 16,
    //    ModContent.ItemType<Items.Placeable.Tile.DarkMatterMonolith>());

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (Main.tile[i, j].TileFrameX >= 36)
        {
            Main.LocalPlayer.GetModPlayer<AvalonPlayer>().DarkMatterMonolith = true;
            Main.LocalPlayer.GetModPlayer<AvalonPlayer>().DarkMatterTimeOut = 2;
        }
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        if (++frameCounter >= 8)
        {
            frameCounter = 0;
            if (++frame >= 8)
            {
                frame = 0;
            }
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }

        int height = tile.TileFrameY % AnimationFrameHeight == 36 ? 18 : 16;
        int animate = Main.tileFrame[Type] * AnimationFrameHeight;
        spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/DarkMatter/DarkMatterMonolith_Glow").Value,
            new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero,
            new Rectangle(tile.TileFrameX, tile.TileFrameY + animate, 16, height), Color.White, 0f, Vector2.Zero, 1f,
            SpriteEffects.None, 0f);
    }

    public override bool RightClick(int i, int j)
    {
        SoundEngine.PlaySound(SoundID.Mech);
        HitWire(i, j);
        return true;
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Items.Placeable.Tile.DarkMatterMonolith>();
    }

    public override void HitWire(int i, int j)
    {
        int x = i - (Main.tile[i, j].TileFrameX / 18 % 2);
        int y = j - (Main.tile[i, j].TileFrameY / 18 % 3);
        for (int l = x; l < x + 2; l++)
        {
            for (int m = y; m < y + 3; m++)
            {
                if (Main.tile[l, m].HasTile && Main.tile[l, m].TileType == Type)
                {
                    if (Main.tile[l, m].TileFrameX < 36)
                    {
                        //Main.tile[l, m].TileFrameY += 56;
                        Main.tile[l, m].TileFrameX += 36;
                    }
                    else
                    {
                        //Main.tile[l, m].TileFrameY -= 56;
                        Main.tile[l, m].TileFrameX -= 36;
                    }
                }
            }
        }

        if (Wiring.running)
        {
            Wiring.SkipWire(x, y);
            Wiring.SkipWire(x, y + 1);
            Wiring.SkipWire(x, y + 2);
            Wiring.SkipWire(x + 1, y);
            Wiring.SkipWire(x + 1, y + 1);
            Wiring.SkipWire(x + 1, y + 2);
        }

        NetMessage.SendTileSquare(-1, x, y + 1, 3);
    }
}
