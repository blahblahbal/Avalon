using Avalon.Items.Placeable.Furniture;
using Avalon.Items.Placeable.Furniture.Coughwood;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.Coughwood;

public class CoughwoodDoorOpen : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = true;
        Main.tileNoSunLight[Type] = true;
        TileObjectData.newTile.Width = 2;
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.Origin = new Point16(0, 0);
        TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 0);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);
        TileObjectData.newTile.UsesCustomCanPlace = true;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.StyleMultiplier = 2;
        TileObjectData.newTile.StyleWrapLimit = 2;
        TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(0, 1);
        TileObjectData.addAlternate(0);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(0, 2);
        TileObjectData.addAlternate(0);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(1, 0);
        TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
        TileObjectData.addAlternate(1);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(1, 1);
        TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
        TileObjectData.addAlternate(1);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(1, 2);
        TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
        TileObjectData.addAlternate(1);
        TileObjectData.addTile(Type);
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
        TileID.Sets.HousingWalls[Type] = true; //needed for non-solid blocks to count as walls
        TileID.Sets.HasOutlines[Type] = true;
        AddMapEntry(new Color(119, 105, 79));
        TileID.Sets.DisableSmartCursor[Type] = true;
        AdjTiles = new int[] { TileID.OpenDoor };
        TileID.Sets.CloseDoorID[Type] = ModContent.TileType<CoughwoodDoorClosed>();
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Furniture.Coughwood.CoughwoodDoor>());
        DustType = -1;
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return true;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = 1;
    }
    public override void MouseOver(int i, int j)
    {
        var player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<CoughwoodDoor>();
    }
}