using Avalon.Common;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MonoMod.Cil;
using System;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class IslandHouse : ModHook
{
    protected override void Apply()
    {
        IL_WorldGen.IslandHouse += BlahIsBad;
    }

    private void BlahIsBad(ILContext il)
    {
        ILCursor c = new(il);
        try
        {
            while (c.TryGotoNext(i => i.MatchLdloc(1) || i.MatchLdloc(2)))
            {
                if (c.Next.MatchLdloc(1))
                {
                    c.Index++;
                    c.EmitDelegate<Func<int, int>>((i) =>
                    {
                        if (WorldGen.SavedOreTiers.Gold == TileID.Platinum)
                            return ModContent.TileType<Tiles.MoonplateBlock>();
                        else if (WorldGen.SavedOreTiers.Gold == ModContent.TileType<Tiles.Ores.BismuthOre>())
                            return ModContent.TileType<Tiles.TwiliplateBlock>();
                        return i;
                    });
                }
                else
                {
                    c.Index++;
                    c.EmitDelegate<Func<int, int>>((i) =>
                    {
                        if (WorldGen.SavedOreTiers.Gold == TileID.Platinum)
                            return ModContent.WallType<Walls.MoonWall>();
                        else if (WorldGen.SavedOreTiers.Gold == ModContent.TileType<Tiles.Ores.BismuthOre>())
                            return ModContent.WallType<Walls.TwilightWall>();
                        return i;
                    });
                }
            }
        }
        catch (Exception e)
        {
            ExxoAvalonOrigins.Mod.Logger.Error($"[Island Plates IL Error]\n{e.Message}\n{e.StackTrace}");
        }
    }
}
