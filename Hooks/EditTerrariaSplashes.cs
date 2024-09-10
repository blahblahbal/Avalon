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
            switch (Main.rand.Next(17))
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
                case 7:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.7");
                    break;
                case 8:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.8");
                    break;
                case 9:
                    output = Language.GetTextValue("Mods.Avalon.GameTitles.9");
                    break;
				case 10:
					output = Language.GetTextValue("Mods.Avalon.GameTitles.10");
					break;
				case 11:
					output = Language.GetTextValue("Mods.Avalon.GameTitles.11");
					break;
				case 12:
					output = Language.GetTextValue("Mods.Avalon.GameTitles.12");
					break;
				case 13:
					output = Language.GetTextValue("Mods.Avalon.GameTitles.13");
					break;
				case 14:
					output = Language.GetTextValue("Mods.Avalon.GameTitles.14");
					break;
				case 15:
					output = Language.GetTextValue("Mods.Avalon.GameTitles.15");
					break;
				case 16:
					output = Language.GetTextValue("Mods.Avalon.GameTitles.16");
					break;
			}
        }
        return output;
    }
}
