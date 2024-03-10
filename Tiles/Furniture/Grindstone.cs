using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture;

public class Grindstone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(116, 116, 116), Language.GetText("Mods.Avalon.Tiles.Grindstone.MapEntry"));
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        DustType = DustID.Stone;
    }
}
