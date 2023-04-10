//using Avalon.Common;
//using Microsoft.Xna.Framework;
//using Mono.Cecil.Cil;
//using MonoMod.Cil;
//using System;
//using Terraria;

//namespace Avalon.Hooks;

//internal class EyeoftheGods : ModHook
//{
//    protected override void Apply()
//    {
//        IL_Main.HoverOverNPCs += IL_HoverOverNPCs;
//    }
//    private static void IL_HoverOverNPCs(ILContext il)
//    {
//        var c = new ILCursor(il);
//        if (!c.TryGotoNext(i => i.MatchLdstr(": ")))
//            return;

//        //var label = il.DefineLabel();

//    }
//}
