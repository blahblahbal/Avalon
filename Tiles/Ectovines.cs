using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class Ectovines : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileCut[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileLavaDeath[Type] = false;
        Main.tileNoFail[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLighted[Type] = true;
        HitSound = SoundID.Grass;
        DustType = DustID.DungeonSpirit;
        TileID.Sets.IsVine[Type] = true;
        TileID.Sets.VineThreads[Type] = true;
        AddMapEntry(new Color(18, 176, 229));
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        Tile tile = Framing.GetTileSafely(i, j + 1);
        if (tile.HasTile && tile.TileType == Type)
        {
            WorldGen.KillTile(i, j + 1);
        }
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 35f / 255f;
        g = 200f / 255f;
        b = 254f / 255f;
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Tile tileAbove = Framing.GetTileSafely(i, j - 1);
        int type = -1;
        if (tileAbove.HasTile && !tileAbove.BottomSlope)
        {
            type = tileAbove.TileType;
        }

        if (type == ModContent.TileType<Ectograss>() || type == Type)
        {
            return true;
        }

        WorldGen.KillTile(i, j);
        return true;
    }

    public override void RandomUpdate(int i, int j)
    {
        Tile tileBelow = Framing.GetTileSafely(i, j + 1);
        if (WorldGen.genRand.NextBool(15) && !tileBelow.HasTile && tileBelow.LiquidType != LiquidID.Lava)
        {
            bool placeVine = false;
            int yTest = j;
            while (yTest > j - 10)
            {
                Tile testTile = Framing.GetTileSafely(i, yTest);
                if (testTile.BottomSlope)
                {
                    break;
                }
                else if (!testTile.HasTile || testTile.TileType != ModContent.TileType<Ectograss>())
                {
                    yTest--;
                    continue;
                }
                placeVine = true;
                break;
            }
            if (placeVine)
            {
                tileBelow.TileType = Type;
                tileBelow.HasTile = true;
                WorldGen.SquareTileFrame(i, j + 1, true);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, i, j + 1, 3, TileChangeType.None);
                }
            }
        }
    }
    //public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    //{
    //    Tile tile = Framing.GetTileSafely(i, j);

    //    var source = new Rectangle(tile.frameX, tile.frameY, 16, 16);
    //    Rectangle realSource = source;

    //    float xOff = GetOffset(i, j, tile.frameX); //Sin offset.
    //    Vector2 drawPos = ((new Vector2(i, j)) * 16) - Main.screenPosition;

    //    Color col = Lighting.GetColor(i, j, Color.White);
    //    Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);

    //    spriteBatch.Draw(ModContent.GetTexture("Avalon/Tiles/ImpVines"), drawPos + zero - new Vector2(xOff, 0), realSource, new Color(col.R, col.G, col.B, 255), 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
    //    return false;
    //}
    //public float GetOffset(int i, int j, int frameX, float sOffset = 0f)
    //{
    //    float sin = (float)Math.Sin((Main.time + (i * 24) + (j * 19)) * (0.04f * (!Lighting.NotRetro ? 0f : 1)) + sOffset) * 1.4f;
    //    if (Framing.GetTileSafely(i, j - 1).type != Type) //Adjusts the sine wave offset to make it look nicer when closer to ground
    //        sin *= 0.25f;
    //    else if (Framing.GetTileSafely(i, j - 2).type != Type)
    //        sin *= 0.5f;
    //    else if (Framing.GetTileSafely(i, j - 3).type != Type)
    //        sin *= 0.75f;

    //    return sin;
    //}
}
