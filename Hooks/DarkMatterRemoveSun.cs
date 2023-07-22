using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class DarkMatterRemoveSun : ModHook
{
    protected override void Apply() => On_Main.DrawSunAndMoon += OnDrawSunAndMoon;

    private static void OnDrawSunAndMoon(On_Main.orig_DrawSunAndMoon orig, Main self,
                                         Main.SceneArea sceneArea, Color moonColor, Color sunColor,
                                         float tempMushroomInfluence)
    {
        if (Main.gameMenu)
        {
            orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
            return;
        }

        if (Main.LocalPlayer.GetModPlayer<AvalonBiomePlayer>().ZoneDarkMatter)
        {
            return;
        }

        orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
    }
}
