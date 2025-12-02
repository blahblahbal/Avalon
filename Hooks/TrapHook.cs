using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class TrapHook : ModHook
{
	protected override void Apply()
	{
		On_Player.PlaceThing_Tiles_PlaceIt_SpinTraps += On_Player_PlaceThing_Tiles_PlaceIt_SpinTraps;
	}

	private void On_Player_PlaceThing_Tiles_PlaceIt_SpinTraps(On_Player.orig_PlaceThing_Tiles_PlaceIt_SpinTraps orig, Player self)
	{
		orig.Invoke(self);
		if (self.HeldItem.createTile == ModContent.TileType<Tiles.Savanna.PoisonGasTrap>())
		{
			if (self.direction == 1)
			{
				Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameX += 18;
			}

			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY);
			}
		}
	}
}
