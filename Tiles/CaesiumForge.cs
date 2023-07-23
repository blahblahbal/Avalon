using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class CaesiumForge : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(76, 255, 0), Language.GetText("Mods.Avalon.Tiles.CaesiumForge.MapEntry0"));
        Main.tileFrameImportant[Type] = true;
        AnimationFrameHeight = 36;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        Main.tileLighted[Type] = true;
        AdjTiles = new int[] { TileID.AdamantiteForge, TileID.Hellforge, TileID.Furnaces };
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        if (++frameCounter > 3)
        {
            frameCounter = 0;
            if (++frame > 3) frame = 0;
        }
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 230f / 255f;
        g = 155f / 255f;
        b = 115f / 255f;
    }
    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (Main.rand.NextBool(40))
        {
            int num56 = Dust.NewDust(new Vector2(i * 16 - 4, j * 16 - 6), 8, 6, DustID.Torch, 0f, 0f, 100, default, 1f);
            if (!Main.rand.NextBool(3))
            {
                Main.dust[num56].noGravity = true;
            }
        }
    }
}
