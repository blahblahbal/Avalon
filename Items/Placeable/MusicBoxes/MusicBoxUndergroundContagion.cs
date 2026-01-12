using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.MusicBoxes;

public class MusicBoxUndergroundContagion : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        if (ExxoAvalonOrigins.MusicMod != null)
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/UndergroundContagion"), ModContent.ItemType<MusicBoxUndergroundContagion>(), ModContent.TileType<Tiles.Furniture.Functional.MusicBoxes>(), 144);
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.Furniture.Functional.MusicBoxes>(), 4);
    }
}
