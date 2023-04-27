using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.OrangeDungeon;

public class OrangeBrickPlatform : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileTable[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.Platforms[Type] = true;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.StyleMultiplier = 27;
        TileObjectData.newTile.StyleWrapLimit = 27;
        TileObjectData.newTile.UsesCustomCanPlace = false;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.addTile(Type);
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
        AddMapEntry(new Color(166, 87, 45));
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.OrangeBrickPlatform>();
        TileID.Sets.DisableSmartCursor[Type] = true;
        AdjTiles = new int[] { TileID.Platforms };
        DustType = ModContent.DustType<Dusts.OrangeDungeonDust>();
    }

    public override void PostSetDefaults()
    {
        Main.tileNoSunLight[Type] = false;
    }
}
