using Avalon.Common;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;

namespace Avalon.Hooks;

public class SpelunkerEdit : ModHook
{
    protected override void Apply()
    {
        IL_TileDrawing.DrawSingleTile += ILDrawSingleTile;
    }

    private static void ILDrawSingleTile(ILContext il)
    {
        const string tileLightFieldName = "tileLight";
        var cursor = new ILCursor(il);

        cursor.GotoNext(instruction => instruction.MatchCall<Main>(nameof(Main.IsTileSpelunkable)));
        cursor.GotoNext(MoveType.After, instruction => instruction.MatchBrfalse(out _));

        cursor.Emit(OpCodes.Ldarg_1);
        cursor.EmitDelegate(() => Color.White);
        cursor.Emit<TileDrawInfo>(OpCodes.Stfld, tileLightFieldName);
    }
}
