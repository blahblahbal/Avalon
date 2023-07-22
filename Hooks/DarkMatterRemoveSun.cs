using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.Main;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class DarkMatterRemoveSun : ModHook
{
    protected override void Apply()
    {
        On_Main.DrawSunAndMoon += OnDrawSunAndMoon;
        On_Main.SetBackColor += OnSetBackColor;
    }

    private static void OnDrawSunAndMoon(On_Main.orig_DrawSunAndMoon orig, Main self,
                                         Main.SceneArea sceneArea, Color moonColor, Color sunColor,
                                         float tempMushroomInfluence)
    {
        if (gameMenu)
        {
            orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
            return;
        }
        if (LocalPlayer.GetModPlayer<AvalonBiomePlayer>().ZoneDarkMatter)
        {
            return;
        }
        orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
    }

    private static void OnSetBackColor(On_Main.orig_SetBackColor orig, InfoToSetBackColor info, out Color sunColor, out Color moonColor)
    {
        orig(info, out sunColor, out moonColor);
        if (!gameMenu)
        {
            if (LocalPlayer.GetModPlayer<AvalonBiomePlayer>().ZoneDarkMatter && dayTime)
            {
                Color bgColorToSet = new Color(5, 5, 5);
                sunColor = bgColorToSet;
                moonColor = bgColorToSet;
                ColorOfTheSkies = bgColorToSet;
            }
        }
        
    }
}
