using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.Hellcastle;
using Avalon.Walls;
using Terraria.ModLoader;

namespace Avalon.ModSupport;

internal class FargosCompat : ModSystem
{
	public override void PostSetupRecipes()
	{
		if (ModLoader.TryGetMod("Fargowiltas", out Mod fargos))
		{
			fargos.Call("AddEvilAltar", ModContent.TileType<IckyAltar>());
			fargos.Call("AddIndestructibleTileType", ModContent.TileType<ImperviousBrick>());
			fargos.Call("AddIndestructibleWallType", ModContent.WallType<ImperviousBrickWallUnsafe>());
			fargos.Call("AddIndestructibleWallType", ModContent.WallType<ImperviousBrickWallBrownUnsafe>());
			fargos.Call("AddIndestructibleWallType", ModContent.WallType<ImperviousBrickWallEctoUnsafe>());
			fargos.Call("AddIndestructibleWallType", ModContent.WallType<ImperviousBrickWallWhiteUnsafe>());
		}
	}
}
