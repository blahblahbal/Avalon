using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Tropics;

public class TuhrtlPressurePlate : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(99, 89, 85));
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.CoordinateHeights = new int[1] { 18 };
        TileObjectData.newTile.CoordinatePadding = 0;
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        DustType = DustID.Silt;
    }
    public static void Trigger(int i, int j)
    {
        SoundEngine.PlaySound(SoundID.Mech, new Vector2(i * 16, j * 16));
        Wiring.TripWire(i, j, 1, 1);
    }
}
