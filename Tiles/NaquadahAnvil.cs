using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class NaquadahAnvil : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(66, 66, 255), LanguageManager.Instance.GetText("Naquadah Anvil"));
        TileObjectData.newTile.Width = 2;
        TileObjectData.newTile.Height = 1;
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.addTile(Type);
        Main.tileObsidianKill[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.placementPreview = true;
        DustType = ModContent.DustType<Dusts.NaquadahDust>();
        AdjTiles = new int[] { TileID.Anvils, TileID.MythrilAnvil };
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Placeable.Crafting.NaquadahAnvil>());
    }
}
