using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class ZirconGemspark : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(231, 170, 116));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileLighted[Type] = true;
        TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[Type] = true;
        TileID.Sets.ForcedDirtMerging[Type] = true;
        TileID.Sets.GemsparkFramingTypes[Type] = Type;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.ZirconGemsparkBlock>();
        HitSound = SoundID.Dig;
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 1.1f;
        g = 0.8f;
        b = 0.4f;
    }
    public override bool CreateDust(int i, int j, ref int type)
    {
        var dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.ZirconDust>(), 0, 0, 100);
        return false;
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
        return false;
    }

    public override void ChangeWaterfallStyle(ref int style)
    {
            style = Mod.Find<ModWaterfallStyle>("ZirconWaterfallStyle").Slot;
    }

    public override void HitWire(int i, int j)
    {
        Tile tileSafely = Framing.GetTileSafely(i, j);
        if (!tileSafely.HasActuator)
        {
            tileSafely.TileType = (ushort)ModContent.TileType<ZirconGemsparkOff>();
            WorldGen.SquareTileFrame(i, j);
            NetMessage.SendTileSquare(-1, i, j, 1);
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        Texture2D texture;
        Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
    }
}
public class ZirconGemsparkOff : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(121, 82, 49));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[Type] = true;
        TileID.Sets.ForcedDirtMerging[Type] = true;
        TileID.Sets.GemsparkFramingTypes[Type] = Type;
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Tile.ZirconGemsparkBlock>());
        HitSound = SoundID.Dig;
    }
    public override bool CreateDust(int i, int j, ref int type)
    {
        var dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.ZirconDust>(), 0, 0, 100);
        return false;
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
        return false;
    }
    public override void HitWire(int i, int j)
    {
        Tile tileSafely = Framing.GetTileSafely(i, j);
        if (!tileSafely.HasActuator)
        {
            tileSafely.TileType = (ushort)ModContent.TileType<ZirconGemspark>();
            WorldGen.SquareTileFrame(i, j);
            NetMessage.SendTileSquare(-1, i, j, 1);
        }
    }
}
