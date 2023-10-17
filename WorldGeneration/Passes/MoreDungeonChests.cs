using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes;

internal class MoreDungeonChests : GenPass
{
    public MoreDungeonChests() : base("Avalon Dungeon Chests", 20f)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configurations)
    {
        for (int num79 = 0; num79 < 6; num79++)
        {
            bool flag5 = false;
            while (!flag5)
            {
                int num80 = WorldGen.genRand.Next(GenVars.dMinX, GenVars.dMaxX);
                int num81 = WorldGen.genRand.Next((int)Main.worldSurface, GenVars.dMaxY);
                if (!Main.wallDungeon[Main.tile[num80, num81].WallType] || Main.tile[num80, num81].HasTile)
                {
                    continue;
                }
                ushort chestTileType = TileID.Containers;
                int contain = 0;
                int style2 = 2;
                if (num79 is 0 or 2 or 4)
                {
                    contain = ModContent.ItemType<Items.Tools.PreHardmode.SapphirePickaxe>();
                }
                if (num79 is 1 or 3 or 5)
                {
                    contain = ModContent.ItemType<Items.Tools.PreHardmode.Blueshift>();
                }
                flag5 = WorldGen.AddBuriedChest(num80, num81, contain, notNearOtherChests: false, style2, trySlope: false, chestTileType);
            }
        }
    }
}
