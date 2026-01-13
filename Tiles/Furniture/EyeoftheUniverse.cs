using Avalon;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture;

public class EyeoftheUniverse : ModTile
{
	private Asset<Texture2D> glow;
	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");

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
		TileGlowDrawing.DrawGlowmask(i, j, Color.White, glow);
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
