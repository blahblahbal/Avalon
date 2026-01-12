using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.MusicBoxes;

public class MusicBoxHellCastle : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        if (ExxoAvalonOrigins.MusicMod != null)
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Hellcastle"), ModContent.ItemType<MusicBoxHellCastle>(), ModContent.TileType<Tiles.Furniture.Functional.MusicBoxes>(), 288);
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.Furniture.Functional.MusicBoxes>(), 8);
    }
}
