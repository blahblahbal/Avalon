using Avalon.Common;
using Avalon.Tiles;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes;

internal class Ectovines : GenPass
{
    public Ectovines() : base("Ectovines", 897.331f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
		if (ModContent.GetInstance<AvalonClientConfig>().UnimplementedStructureGen)
		{
			for (int num586 = 0; num586 < Main.maxTilesX; num586++)
			{
				int num587 = 0;
				for (int num589 = 0; num589 < Main.maxTilesY; num589++)
				{
					if (num587 > 0 && !Main.tile[num586, num589].HasTile)
					{
						Main.tile[num586, num589].TileType = (ushort)ModContent.TileType<Tiles.Ectovines>();
						Tile t = Main.tile[num586, num589];
						t.HasTile = true;
						num587--;
					}
					else
					{
						num587 = 0;
					}

					if (Main.tile[num586, num589].HasTile &&
						Main.tile[num586, num589].TileType == (ushort)ModContent.TileType<Ectograss>() &&
						!Main.tile[num586, num589].BottomSlope && WorldGen.genRand.Next(5) < 3)
					{
						num587 = WorldGen.genRand.Next(1, 10);
					}
				}
			}
		}
    }
}
