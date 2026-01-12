using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.MusicBoxes;

public class MusicBoxDesertBeakOtherworldly : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        if (ExxoAvalonOrigins.MusicMod != null)
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DesertBeakEnnway"), ModContent.ItemType<MusicBoxDesertBeakOtherworldly>(), ModContent.TileType<Tiles.Furniture.Functional.MusicBoxes>(), 396);
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.Furniture.Functional.MusicBoxes>(), 11);
    }
}
