using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ObjectData;
using Avalon.Dusts;

namespace Avalon.Tiles.CrystalMines;

public class ShatterShards : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileSolid[Type] = false;
        //Main.tileShine[Type] = 1200; too much light
        TileID.Sets.TouchDamageImmediate[Type] = 20;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.AnchorValidTiles = new int[]
        {
            ModContent.TileType<CrystalStone>(),
            TileID.CrystalBlock
        };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.RandomStyleRange = 6;
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.addTile(Type);
        HitSound = SoundID.Item27;
        AddMapEntry(new Color(58, 153, 139));
        DustType = ModContent.DustType<ShatterShardDust>();
    }
    public override bool IsTileDangerous(int i, int j, Player player)
    {
        return true;
    }
    //Doesn't work cause it's not a solid tile
    //public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
    //{
    //    dustType = ModContent.DustType<ShatterShardDust>();
    //    makeDust = true;
    //    base.WalkDust(ref dustType, ref makeDust, ref color);
    //}
}
