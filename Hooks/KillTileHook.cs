using Avalon.Common;
using Avalon.Tiles.Hellcastle;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class KillTileHook : ModHook
{
    protected override void Apply()
    {
        On_WorldGen.KillTile += On_WorldGen_KillTile;
    }

    private void On_WorldGen_KillTile(On_WorldGen.orig_KillTile orig, int i, int j, bool fail, bool effectOnly, bool noItem)
    {
        if (Main.tile[i, j].TileType == ModContent.TileType<UltraResistantWood>() && fail)
        {
            return;
        }
        orig.Invoke(i, j, fail, effectOnly, noItem);
    }
}
