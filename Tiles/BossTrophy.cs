using Avalon.Items.Placeable.Trophy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class BossTrophy : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.StyleWrapLimit = 36;
        TileObjectData.addTile(Type);
        DustType = 7;
        TileID.Sets.DisableSmartCursor[Type] = true;
        AddMapEntry(new Color(120, 85, 60), Language.GetText("ItemName.Trophy"));
    }

    //public override void KillMultiTile(int i, int j, int frameX, int frameY)
    //{
    //    int item = 0;
    //    switch (frameX / 54)
    //    {
    //        case 0:
    //            item = ModContent.ItemType<DesertBeakTrophy>();
    //            break;
    //        case 1:
    //            item = ModContent.ItemType<ArmageddonSlimeTrophy>();
    //            break;
    //        case 2:
    //            item = ModContent.ItemType<DragonLordTrophy>();
    //            break;
    //        case 3:
    //            item = ModContent.ItemType<OblivionTrophy>();
    //            break;
    //        case 4:
    //            item = ModContent.ItemType<BacteriumPrimeTrophy>();
    //            break;
    //        case 5:
    //            item = ModContent.ItemType<EggmanTrophy>();
    //            break;
    //        case 6:
    //            item = ModContent.ItemType<WallofSteelTrophy>();
    //            break;
    //        case 7:
    //            item = ModContent.ItemType<MechastingTrophy>();
    //            break;
    //        case 8:
    //            item = ModContent.ItemType<PhantasmTrophy>();
    //            break;
    //    }
    //    if (item > 0)
    //    {
    //        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 48, 48, item);
    //    }
    //}
}
