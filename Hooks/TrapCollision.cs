using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class TrapCollision : ModHook
{
    protected override void Apply()
    {
        On_Collision.CanTileHurt += OnCanTileHurt;
        On_Player.ApplyTouchDamage += OnApplyTouchDamage;
    }

    private static bool OnCanTileHurt(On_Collision.orig_CanTileHurt orig, ushort type, int i, int j, Player player)
    {
        if (player != null)
        {
            if (player.GetModPlayer<AvalonPlayer>().TrapImmune)
            {
                if (type == TileID.Spikes || type == TileID.WoodenSpikes || type == ModContent.TileType<Tiles.VenomSpike>() || type == ModContent.TileType<Tiles.CrystalMines.ShatterShards>())
                {
                    return false;
                }
            }
            else if (!player.GetModPlayer<AvalonPlayer>().TrapImmune && type == ModContent.TileType<Tiles.VenomSpike>())
            {
                return true;
            }
            if (type == ModContent.TileType<Tiles.CrystalMines.ShatterShards>())
            {
                if (player.afkCounter > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return orig(type, i, j, player);
    }
    private static void OnApplyTouchDamage(On_Player.orig_ApplyTouchDamage orig, Player self, int tileId, int x, int y)
    {
        if (tileId == ModContent.TileType<Tiles.VenomSpike>())
        {
            int num = Main.DamageVar(90, 0f - self.luck);
            self.AddBuff(BuffID.Venom, 180);
            self.Hurt(PlayerDeathReason.ByOther(3), num, 0, false, false, 0, true);
        }
        orig(self, tileId, x, y);
    }
}
