using Avalon.Common;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria.ID;
using Terraria;

namespace Avalon.Hooks
{
    class AIChanges : ModHook
    {
        protected override void Apply()
        {
            IL_NPC.AI_006_Worms += ILAI_006_Worms;
        }
        public static void ILAI_006_Worms(ILContext il)
        {
            var c = new ILCursor(il);

            FieldReference npcType = null;

            // flag4
            if (!c.TryGotoNext(i => i.MatchStloc(93)))
                return;
            if (!c.TryGotoNext(i => i.MatchLdfld(out npcType)))
                return;

            for (var j = 0; j < 2; j++)
            {
                // 1st run. getZoneCorruption always returns true for devourer
                // 2nd run. getZoneCrimson always returns true for devourer
                if (!c.TryGotoNext(i => i.MatchCallvirt(out _)))
                    return;
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldfld, npcType);
                c.EmitDelegate<Func<bool, int, bool>>((orig, type) =>
                {
                    if (type == NPCID.DevourerHead)
                    {
                        return true;
                    }
                    return orig;
                });
            }
        }
    }
}
