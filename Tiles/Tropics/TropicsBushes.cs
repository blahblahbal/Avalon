using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Tropics;

public class TropicsBushes : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(45, 153, 26));
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.addTile(Type);
    }
}
