using Avalon.Items.Placeable.Painting;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class Paintings : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.newTile.Width = 6;
        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.CoordinateHeights = new int[]
        {
            16,
            16,
            16,
            16
        };
        TileObjectData.newTile.AnchorWall = true;
        TileObjectData.addTile(Type);
        DustType = 7;
        TileID.Sets.DisableSmartCursor[Type] = true;
        AddMapEntry(new Color(120, 85, 60));
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        int item = 0;
        switch (frameY / 72)
        {
            case 0:
                item = ModContent.ItemType<EclipseofDoom>();
                break;
            case 1:
                item = ModContent.ItemType<RuinedCivilization>();
                break;
            case 2:
                item = ModContent.ItemType<Trespassing>();
                break;
            case 3:
                item = ModContent.ItemType<BirthofaMonster>();
                break;
            case 4:
                item = ModContent.ItemType<EvilOuroboros>();
                break;
            case 5:
                item = ModContent.ItemType<Items.Placeable.Painting.ACometHasStruckGround>();
                break;
            case 6:
                item = ModContent.ItemType<PlanterasRage>();
                break;
            case 7:
                item = ModContent.ItemType<FightoftheBumblebee>();
                break;
            case 8:
                item = ModContent.ItemType<FrostySpectacle>();
                break;
            case 9:
                item = ModContent.ItemType<RingofDisgust>();
                break;
            case 10:
                item = ModContent.ItemType<CurseofOblivion>();
                break;
            case 11:
                item = ModContent.ItemType<Clash>();
                break;
            case 12:
                item = ModContent.ItemType<CrossingtheTropics>();
                break;
        }
        if (item > 0)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 48, 48, item);
        }
    }
}
