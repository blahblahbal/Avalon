using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.Functional;

public class GiantGravestone : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        // Placement
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
        TileObjectData.newTile.Origin = new Point16(0, 1);
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(127, 127, 127), Language.GetText("Giant Gravestone"));
    }
    //public override bool RightClick(int i, int j)
    //{


    //    return true;
    //}
    //public override void NearbyEffects(int i, int j, bool closer)
    //{
    //    Main.LocalPlayer.ZoneGraveyard = true;
    //}
    //public override void MouseOver(int i, int j)
    //{
    //    Player player = Main.player[Main.myPlayer];
    //    player.noThrow = 2;
    //    player.cursorItemIconEnabled = true;
    //    player.cursorItemIconID = ModContent.ItemType<Items.Placeable.Furniture.GiantGravestone>();
    //}
}
