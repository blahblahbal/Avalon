using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;
internal class PickPower : ModHook
{
    protected override void Apply()
    {
        On_Player.GetPickaxeDamage += OnGetPickaxeDamage;
    }
    private static int OnGetPickaxeDamage(On_Player.orig_GetPickaxeDamage orig, Player self, int x, int y, int pickPower, int hitBufferIndex, Tile tileTarget)
    {
        int num = orig(self, x, y, pickPower, hitBufferIndex, tileTarget);
        if (tileTarget.TileType == TileID.Hellstone && pickPower < 75)
        {
            num = 0;
        }
        if (ExxoAvalonOrigins.Depths != null)
        {
            if (tileTarget.TileType == ExxoAvalonOrigins.Depths.Find<ModTile>("ArqueriteOre").Type && pickPower < 75)
            {
                num = 0;
            }
        }
        return num;
    }
}
