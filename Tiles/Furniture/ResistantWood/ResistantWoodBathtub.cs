using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.ResistantWood;

public class ResistantWoodBathtub : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = false;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2); //this style already takes care of direction for us
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
        AddMapEntry(new Color(144, 148, 144), Language.GetText("ItemName.Bathtub"));
        DustType = -1;
    }
}
