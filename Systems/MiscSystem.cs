using Avalon.Tiles.Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.ModLoader;

namespace Avalon.Systems;
internal class MiscSystem : ModSystem
{
	public override void PostAddRecipes()
	{
		HouseUtils.BlacklistedTiles[ModContent.TileType<OrangeBrick>()] = true;
		HouseUtils.BeelistedTiles[ModContent.TileType<OrangeBrick>()] = true;
		HouseUtils.BlacklistedTiles[ModContent.TileType<YellowBrick>()] = true;
		HouseUtils.BeelistedTiles[ModContent.TileType<YellowBrick>()] = true;
		HouseUtils.BlacklistedTiles[ModContent.TileType<PurpleBrick>()] = true;
		HouseUtils.BeelistedTiles[ModContent.TileType<PurpleBrick>()] = true;


	}
}
