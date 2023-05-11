using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class NickelAnvil : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(140, 130, 116), LanguageManager.Instance.GetText("Anvil"));
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
        TileObjectData.newTile.CoordinateHeights = new[] { 18, 18 };
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        Main.tileObsidianKill[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileFrameImportant[Type] = true;
        DustType = ModContent.DustType<Dusts.NickelDust>();
        AdjTiles = new int[] { TileID.Anvils };
    }
}
