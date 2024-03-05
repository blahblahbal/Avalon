using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class TroxiniumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 4f;
        AddMapEntry(Color.Goldenrod, this.GetLocalization("MapEntry"));
        Data.Sets.Tile.RiftOres[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileOreFinderPriority[Type] = 660;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 875;
        HitSound = SoundID.Tink;
        MinPick = 150;
        DustType = ModContent.DustType<TroxiniumDust>();
        TileID.Sets.Ore[Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Tropics.Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Tropics.Loam>()][Type] = true;
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
        Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, pos, frame,
            new Color(255, 255, 255, 0) * (Lighting.Brightness(i, j) * 4f));
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
