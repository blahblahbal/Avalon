using Avalon.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using Avalon.Tiles;
using Avalon.Items.Weapons.Ranged.Hardmode.Hellrazer;

namespace Avalon.WorldGeneration.Passes;

internal class MoreDungeonChests : GenPass
{
    public MoreDungeonChests() : base("Avalon Dungeon Chests", 20f)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configurations)
    {
		// also replace some spikes with poison spikes
		for (int i = 10; i < Main.maxTilesX - 10; i++)
		{
			for (int j = 150; j < Main.maxTilesY - 150; j++)
			{
				if (Main.tile[i, j].TileType == TileID.Spikes && WorldGen.genRand.NextBool(40))
				{
					ClassExtensions.ReplaceVein(new Point(i, j), TileID.Spikes, ModContent.TileType<PoisonSpike>());
				}
			}
		}

		// underworld chest
		bool placedUnderworldChest = false;
		while (!placedUnderworldChest)
		{
			int num80 = WorldGen.genRand.Next(GenVars.dMinX, GenVars.dMaxX);
			int num81 = WorldGen.genRand.Next((int)Main.worldSurface, GenVars.dMaxY);
			if (!Main.wallDungeon[Main.tile[num80, num81].WallType] || Main.tile[num80, num81].HasTile)
			{
				continue;
			}
			ushort chestTileType = (ushort)ModContent.TileType<UnderworldChest>();
			int contain = ModContent.ItemType<Hellrazer>();
			int style2 = 1;
			placedUnderworldChest = WorldGen.AddBuriedChest(num80, num81, contain, notNearOtherChests: false, style2, trySlope: false, chestTileType);
		}
		// more regular locked gold chests
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
