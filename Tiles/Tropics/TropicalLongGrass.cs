using Avalon.Items.Material;
using Avalon.Items.Vanity;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Tropics;

public class TropicalLongGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileCut[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileWaterDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.AnchorValidTiles = new int[1] { ModContent.TileType<TropicalGrass>() };
        TileObjectData.newTile.CoordinateHeights = new int[1] { 32 };
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<Dusts.TropicalDust>();
        HitSound = SoundID.Grass;
        AddMapEntry(new Color(58, 188, 32));
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        //if (Main.tile[i, j].TileFrameX / 18 == 8)
        //    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<TropicalShroomCap>());
        //if (Main.tile[i, j].TileFrameX / 18 == 9)
        //    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.NaturesGift);
        //if (Main.tile[i, j].TileFrameX / 18 is 6 or 7)
        //    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<TropicsLily>());
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        offsetY = -16;
    }
}
