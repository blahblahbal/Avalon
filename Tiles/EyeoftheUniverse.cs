using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class EyeoftheUniverse : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.newTile.Width = 12;
        TileObjectData.newTile.Height = 9;
        TileObjectData.newTile.CoordinateHeights = new int[]
        {
            16, 16, 16, 16, 16, 16, 16, 16, 16
        };
        TileObjectData.newTile.AnchorWall = true;
        TileObjectData.addTile(Type);
        TileID.Sets.DisableSmartCursor[Type] = true;
        AddMapEntry(new Color(120, 85, 60));
    }
    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (Main.tile[i, j].TileFrameY >= 161 && Main.tile[i, j].TileFrameX <= 322)
        {
            Lighting.AddLight(new Vector2(i * 16, j * 16), new Vector3(0.043f, 0.11f, 0.19f));
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

        Vector2 pos = new Vector2(i * 16, j * 16) + zero - Main.screenPosition;
        var frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
        Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/EyeoftheUniverse_Glow").Value, pos, frame,
            Color.White);
    }

    public override bool CreateDust(int i, int j, ref int type)
    {
        if (Main.rand.NextBool(5))
        {
            switch (Main.tile[i, j].TileFrameY / 160)
            {
                case 0:
                    Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.WoodFurniture);
                    return false;
                case 1:
                    if (Main.rand.NextBool(2))
                    {
                        Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.MagicMirror);
                    }
                    return false;
            }
        }
        return false;
    }
}
