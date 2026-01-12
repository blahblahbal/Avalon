using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.MusicBoxes;

public class MusicBoxUndergroundContagionOtherworldly : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        if (ExxoAvalonOrigins.MusicMod != null)
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/UndergroundContagionEnnway"), ModContent.ItemType<MusicBoxUndergroundContagionOtherworldly>(), ModContent.TileType<Tiles.Furniture.Functional.MusicBoxes>(), 468);
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.Furniture.Functional.MusicBoxes>(), 4);
    }
}
