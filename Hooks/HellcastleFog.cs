using Avalon.Common;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using static Mono.Cecil.Cil.OpCodes;

namespace Avalon.Hooks
{
    internal class HellcastleFog : ModHook
    {
        protected override void Apply()
        {
            //IL_AmbientWindSystem.Update += IL_AmbientWindSystem_Update;
            On_Main.DoUpdateInWorld += On_Main_DoUpdateInWorld;
        }

        private void On_Main_DoUpdateInWorld(On_Main.orig_DoUpdateInWorld orig, Main self, System.Diagnostics.Stopwatch sw)
        {
            orig(self, sw);
            ExxoAvalonOrigins.hellcastleFog.Update();
        }

        private void IL_AmbientWindSystem_Update(ILContext il)
        {
            try
            {
                var c = new ILCursor(il);
                var label = il.DefineLabel();

                c.Emit(Call, typeof(Player).GetMethod("InModBiome", new Type[] { }).MakeGenericMethod(typeof(Biomes.Hellcastle)));
                c.Emit(Brfalse_S, label);
                c.Emit(Ret);

                c.MarkLabel(label);

            }
            catch (Exception e)
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<ExxoAvalonOrigins>(), il);
            }
        }
    }
}
