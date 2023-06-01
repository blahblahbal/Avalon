using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.ResistantWood;

public class ResistantWoodBookcase : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileNoAttach[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileTable[Type] = true;
        Main.tileLavaDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.IgnoredByNpcStepUp[Type] = true; // This line makes NPCs not try to step up this tile during their movement. Only use this for furniture with solid tops.

        DustType = -1;
        AdjTiles = new int[] { TileID.Bookcases };

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16 };
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
        AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Bookcase"));
    }
}
