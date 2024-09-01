using Avalon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class TrapHook : ModHook
{
    protected override void Apply()
    {
        On_Player.PlaceThing_Tiles_PlaceIt_SpinTraps += On_Player_PlaceThing_Tiles_PlaceIt_SpinTraps;
        IL_Wiring.HitSwitch += IL_Wiring_HitSwitch;
    }

    private void IL_Wiring_HitSwitch(MonoMod.Cil.ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.PressurePlates, id => TileID.Sets.Factory.CreateBoolSet(ModContent.TileType<Tiles.Savanna.TuhrtlPressurePlate>())[id]);
    }

    private void On_Player_PlaceThing_Tiles_PlaceIt_SpinTraps(On_Player.orig_PlaceThing_Tiles_PlaceIt_SpinTraps orig, Player self)
    {
        orig.Invoke(self);
        if (self.HeldItem.createTile == ModContent.TileType<Tiles.Savanna.PoisonGasTrap>())
        {
            if (self.direction == 1)
                Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameX += 18;

            if (Main.netMode == 1)
                NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY);
        }
    }
}
