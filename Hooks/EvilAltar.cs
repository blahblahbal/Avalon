using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class EvilAltar : ModHook
{
    protected override void Apply() => IL_WorldGen.SmashAltar += WorldGen_SmashAltar;

    private static Color GetHardmodeColor(int i)
    {
        switch (i) {
            case TileID.Cobalt:
                return new Color(26, 105, 161);
            case TileID.Palladium:
                return new Color(235, 87, 47);
            case TileID.Mythril:
                return new Color(93, 147, 88);
            case TileID.Orichalcum:
                return new Color(163, 22, 158);
            case TileID.Adamantite:
                return new Color(221, 85, 152);
            case TileID.Titanium:
                return new Color(185, 194, 215);
            default:
            //    if (i == ModContent.TileType<DurataniumOre>())
            //    {
            //        return new Color(137, 81, 89);
            //    }

            //    if (i == ModContent.TileType<TroxiniumOre>())
            //    {
            //        return new Color(193, 218, 72);
            //    }

            //    if (i == ModContent.TileType<NaquadahOre>())
            //    {
            //        return new Color(0, 38, 255);
            //    }

                return new Color(50, 255, 130);
        }
    }

    private void WorldGen_SmashAltar(ILContext il)
	{
        ILCursor c = new(il);
        int j = 0;
        while (c.TryGotoNext(i => i.MatchLdcI4(50),
            i => i.MatchLdcI4(255),
            i => i.MatchLdcI4(130)))
		{
            c.Index++;
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, j);
            c.EmitDelegate<Func<int, int>>((j) =>
            {
                int type = WorldGen.SavedOreTiers.Cobalt;
                if (j > 1 && j <= 3)
                    type = WorldGen.SavedOreTiers.Mythril;
                else if (j > 3 && j <= 5)
                    type = WorldGen.SavedOreTiers.Adamantite;
                return GetHardmodeColor(type).R;
            });

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, j);
            c.EmitDelegate<Func<int, int>>((j) =>
            {
                int type = WorldGen.SavedOreTiers.Cobalt;
                if (j > 1 && j <= 3)
                    type = WorldGen.SavedOreTiers.Mythril;
                else if (j > 3 && j <= 5)
                    type = WorldGen.SavedOreTiers.Adamantite;
                return GetHardmodeColor(type).G;
            });

            c.Index++;
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, j);
            c.EmitDelegate<Func<int, int>>((j) =>
            {
                int type = WorldGen.SavedOreTiers.Cobalt;
                if (j > 1 && j <= 3)
                    type = WorldGen.SavedOreTiers.Mythril;
                else if (j > 3 && j <= 5)
                    type = WorldGen.SavedOreTiers.Adamantite;
                return GetHardmodeColor(type).B;
            });

            j++;
		}
	}
}
