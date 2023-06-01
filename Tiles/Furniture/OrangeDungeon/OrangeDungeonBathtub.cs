using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.OrangeDungeon;

public class OrangeDungeonBathtub : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2); //this style already takes care of direction for us
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
        TileObjectData.addTile(Type);
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
        AddMapEntry(new Color(144, 148, 144), Language.GetText("ItemName.Bathtub"));
        DustType = -1;
    }
}
