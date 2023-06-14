using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class TomeForge : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(55, 100, 134), LanguageManager.Instance.GetText("Tome Forge"));
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
        TileObjectData.newTile.CoordinateHeights = new[]
        {
            16,
            16,
            18
        };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        DustType = DustID.DungeonBlue;
    }
}
