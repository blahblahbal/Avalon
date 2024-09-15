using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class HellHouses : ModHook
    {
        protected override void Apply()
        {
            // ADD BACK WHEN HELLCASTLE IS RE-ADDED
            if (ModContent.GetInstance<AvalonClientConfig>().UnimplementedStructureGen)
			{
				On_WorldGen.HellFort += OnHellFort;
			}
        }
        private static void OnHellFort(On_WorldGen.orig_HellFort orig, int i, int j, ushort tileType = 75, byte wallType = 14)
        {
            int hellcastleOriginX = (Main.maxTilesX / 2) - 200;
            int ashenLeft = hellcastleOriginX - 100;
            int ashenRight = hellcastleOriginX + 500;
            if (i > ashenLeft && i < ashenRight && j > Main.maxTilesY - 200 && j < Main.maxTilesY - 50)
            {
                return;
            }
            orig(i, j, tileType, wallType);
        }
    }
}
