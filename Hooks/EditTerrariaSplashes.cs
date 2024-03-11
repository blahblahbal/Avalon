using Avalon.Common;
using Terraria;
using Terraria.Localization;

namespace Avalon.Hooks;

internal class EditTerrariaSplashes : ModHook
{
    protected override void Apply()
    {
        On_Lang.GetRandomGameTitle += On_Lang_GetRandomGameTitle;
    }

    private string On_Lang_GetRandomGameTitle(On_Lang.orig_GetRandomGameTitle orig)
    {
        var output = orig.Invoke();
        if (Main.rand.NextBool(12))
        {
            switch (Main.rand.Next(7))
            {
                case 0:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.0");
                    break;
                case 1:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.1");
                    break;
                case 2:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.2");
                    break;
                case 3:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.3");
                    break;
                case 4:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.4");
                    break;
                case 5:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.5");
                    break;
                case 6:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.6");
                    break;
            }
        }
        return output;
    }
}
