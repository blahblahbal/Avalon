using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
        ItemDrop = ItemID.AshBlock;
        DustType = DustID.Silt;
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
        Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/Ectograss_Glow").Value, pos, frame,
            Color.White);
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
