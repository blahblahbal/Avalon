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
        On_Player.TryLandingOnDetonator += On_Player_TryLandingOnDetonator;
        IL_Collision.SwitchTiles += IL_Collision_SwitchTiles;
    }

    private void IL_Collision_SwitchTiles(MonoMod.Cil.ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.PressurePlates, id => TileID.Sets.Factory.CreateBoolSet(ModContent.TileType<Tiles.Savanna.TuhrtlPressurePlate>())[id]);
    }

    private void On_Player_TryLandingOnDetonator(On_Player.orig_TryLandingOnDetonator orig, Player self)
    {
        if (self.GetModPlayer<AvalonPlayer>().TrapImmune) return;
        orig.Invoke(self);
    }

    private static bool OnCanTileHurt(On_Collision.orig_CanTileHurt orig, ushort type, int i, int j, Player player)
    {
        if (player != null)
        {
            if (player.GetModPlayer<AvalonPlayer>().TrapImmune)
            {
                if (type == TileID.Spikes || type == TileID.WoodenSpikes || type == ModContent.TileType<Tiles.VenomSpike>() ||
                    type == ModContent.TileType<Tiles.Savanna.BrambleSpikes>() ||
                    type == ModContent.TileType<Tiles.CrystalMines.ShatterShards>() ||
                    type == ModContent.TileType<Tiles.Savanna.Bramble>())
                {
                    return false;
                }
            }
            else if (!player.GetModPlayer<AvalonPlayer>().TrapImmune && (type == ModContent.TileType<Tiles.VenomSpike>() || type == ModContent.TileType<Tiles.Savanna.BrambleSpikes>()))
            {
                return true;
            }
            if (type == ModContent.TileType<Tiles.CrystalMines.ShatterShards>() || type == ModContent.TileType<Tiles.Savanna.Bramble>())
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
        if (tileId == ModContent.TileType<Tiles.Savanna.BrambleSpikes>())
        {
            int num = Main.DamageVar(90, 0f - self.luck);
            int time = 10;
            if (Main.expertMode) time = 20;
            if (Main.masterMode) time = 25;
            self.AddBuff(BuffID.Bleeding, time * 60);
            self.Hurt(PlayerDeathReason.ByOther(3), num, 0, false, false, 0, true);
        }
        orig(self, tileId, x, y);
    }
}
