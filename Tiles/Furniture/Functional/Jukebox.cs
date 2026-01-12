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

public class Jukebox : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        //TileID.Sets.HasOutlines[Type] = true;
        AddMapEntry(new Color(152, 107, 82), LanguageManager.Instance.GetText("Jukebox"));
        TileID.Sets.DisableSmartCursor[Type] = true;
        // Placement
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.Origin = new Point16(0, 1);
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.addTile(Type);
    }
    public override bool RightClick(int i, int j)
    {
        Main.playerInventory = true;

        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().DisplayJukeboxInterface =
            !Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().DisplayJukeboxInterface;
        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().JukeboxX = i;
        Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().JukeboxY = j;

        return true;
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.player[Main.myPlayer];
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Items.Placeable.Furniture.Jukebox>();
    }
}
