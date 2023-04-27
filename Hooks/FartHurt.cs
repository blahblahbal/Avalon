using Avalon.Common;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace Avalon.Hooks
{
    internal class FartHurt : ModHook
    {
        protected override void Apply()
        {
            IL_Player.JumpMovement += ILOnJumpMovement;
        }
        private static void ILOnJumpMovement(ILContext il)
        {
            var c = new ILCursor(il);

            c.GotoNext(i => i.MatchLdarg(0));
            c.GotoNext(i => i.MatchLdcI4(1));
            c.GotoNext(i => i.MatchStfld(typeof(Player).GetField("isPerformingJump_Fart")));

            c.EmitDelegate<Func<bool, Player>>((p) =>
            {
                Item fart = Main.LocalPlayer.HasItemInArmorFindIt(ItemID.FartinaJar);
                if (fart != null)
                {
                    if (fart.prefix == PrefixID.Violent)
                    {
                        if (Main.LocalPlayer.Male) SoundEngine.PlaySound(in SoundID.PlayerHit, Main.LocalPlayer.position);
                        else SoundEngine.PlaySound(in SoundID.FemaleHit, Main.LocalPlayer.position);
                    }
                }
                return Main.LocalPlayer;
            });
        }
    }
}
