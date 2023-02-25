using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Hooks
{
    public class SpelunkerEdit : ModHook
    {
        protected override void Apply()
        {
            IL_TileDrawing.DrawSingleTile += ILDrawSingleTile;
        }

        private static void ILDrawSingleTile(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdcI4(170)))
                return;

            var label = il.DefineLabel();

            c.Emit(OpCodes.Ldarg_1);
            c.Emit(OpCodes.Call, typeof(Color).GetMethod("get_White"));
            c.Emit(OpCodes.Stfld, typeof(TileDrawInfo).GetField("tileLight"));

            c.MarkLabel(label);
        }
    }
}
