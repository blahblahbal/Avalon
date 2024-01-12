using Avalon.Common;
using Avalon.Tiles.Contagion;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Hooks
{
    internal class ContagionSandstorm : ModHook
    {
        protected override void Apply()
        {
            On_Sandstorm.ShouldSandstormDustPersist += On_Sandstorm_ShouldSandstormDustPersist;
        }

        private bool On_Sandstorm_ShouldSandstormDustPersist(On_Sandstorm.orig_ShouldSandstormDustPersist orig)
        {
            if (Sandstorm.Happening && Main.LocalPlayer.ZoneSandstorm && (Main.bgStyle == 2 || Main.bgStyle == 5 || Main.bgStyle == 15))
            {
                return Main.bgDelay < 50;
            }
            return orig.Invoke();
        }
    }
}
