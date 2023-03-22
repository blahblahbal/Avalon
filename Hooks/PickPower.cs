using Avalon.Common;
using Terraria;
using Terraria.ID;

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
        if (tileTarget.TileType == TileID.Hellstone && pickPower < 70)
        {
            num = 0;
        }
        return num;
    }
}
