using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.MusicBoxes;

class MusicBoxTropics : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        //if (ExxoAvalonOrigins.MusicMod != null)
        //    MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Tropics"), ModContent.ItemType<MusicBoxTropics>(), ModContent.TileType<Tiles.MusicBoxes>(), 180);
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBoxes>(), 5);
    }
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
}
