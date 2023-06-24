using Avalon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class DryadTextDetour : ModHook
    {
        protected override void Apply()
        {
            On_Lang.GetDryadWorldStatusDialog += On_Lang_GetDryadWorldStatusDialog;
        }

        public override void Unload()
        {
            On_Lang.GetDryadWorldStatusDialog -= On_Lang_GetDryadWorldStatusDialog;
        }
        //Another reminder to have a UnApply Hook for ModHook so that we are able to unload detours and IL edits without causing sirious issues
        //On_Lang.GetDryadWorldStatusDialog -= On_Lang_GetDryadWorldStatusDialog; //Would go in such hook

        private string On_Lang_GetDryadWorldStatusDialog(On_Lang.orig_GetDryadWorldStatusDialog orig, out bool worldIsEntirelyPure)
        {
            orig.Invoke(out worldIsEntirelyPure); //Invoke the original code first so nothing from vanilla is executed since this is a return string
            string text = "";
            worldIsEntirelyPure = false;
            int tGood = WorldGen.tGood;
            int tEvil = WorldGen.tEvil;
            int tBlood = WorldGen.tBlood;
            int tSick = AvalonWorld.tSick;
            if (tGood > 0 && tEvil > 0 && tBlood > 0 && tSick > 0)
            {
                text = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusAll", Main.worldName, tGood, tEvil, tBlood, tSick); //We use a combination of vanilla and our own localization to put less on translators
            }

            else if (tGood > 0 && tSick > 0 && tEvil > 0)
            {
                text = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusHallowCorruptSick", Main.worldName, tGood, tEvil, tSick);
            }
            else if (tGood > 0 && tSick > 0 && tBlood > 0)
            {
                text = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusHallowCrimsonSick", Main.worldName, tGood, tBlood, tSick);
            }
            else if (tSick > 0 && tEvil > 0 && tBlood > 0)
            {
                text = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusCorruptCrimsonSick", Main.worldName, tEvil, tBlood, tSick);
            }
            else if (tGood > 0 && tEvil > 0 && tBlood > 0)
            {
                text = Language.GetTextValue("DryadSpecialText.WorldStatusAll", Main.worldName, tGood, tEvil, tBlood);
            }

            else if (tGood > 0 && tEvil > 0)
            {
                text = Language.GetTextValue("DryadSpecialText.WorldStatusHallowCorrupt", Main.worldName, tGood, tEvil);
            }
            else if (tGood > 0 && tBlood > 0)
            {
                text = Language.GetTextValue("DryadSpecialText.WorldStatusHallowCrimson", Main.worldName, tGood, tBlood);
            }
            else if (tSick > 0 && tEvil > 0)
            {
                text = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusCorruptSick", Main.worldName, tSick, tEvil, tSick);
            }
            else if (tSick > 0 && tBlood > 0)
            {
                text = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusCrimsonSick", Main.worldName, tSick, tBlood, tSick);
            }
            else if (tEvil > 0 && tBlood > 0)
            {
                text = Language.GetTextValue("DryadSpecialText.WorldStatusCorruptCrimson", Main.worldName, tEvil, tBlood);
            }
            else if (tGood > 0 && tSick > 0)
            {
                text = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusHallowSick", Main.worldName, tGood, tSick);
            }

            else if (tSick > 0)
            {
                text = Language.GetTextValue("Mods.Avalon.DryadSpecialText.WorldStatusSick", Main.worldName, tSick);
            }
            else if (tEvil > 0)
            {
                text = Language.GetTextValue("DryadSpecialText.WorldStatusCorrupt", Main.worldName, tEvil);
            }
            else if (tBlood > 0)
            {
                text = Language.GetTextValue("DryadSpecialText.WorldStatusCrimson", Main.worldName, tBlood);
            }
            else
            {
                if (tGood <= 0)
                {
                    text = Language.GetTextValue("DryadSpecialText.WorldStatusPure", Main.worldName);
                    worldIsEntirelyPure = true;
                    return text;
                }
                text = Language.GetTextValue("DryadSpecialText.WorldStatusHallow", Main.worldName, tGood);
            }
            string arg = (((double)(tGood) * 1.2 >= (double)(tEvil + tBlood + tSick) && (double)(tGood) * 0.8 <= (double)(tEvil + tBlood + tSick)) ? Language.GetTextValue("DryadSpecialText.WorldDescriptionBalanced") : ((tGood >= tEvil + tBlood + tSick) ? Language.GetTextValue("DryadSpecialText.WorldDescriptionFairyTale") : ((tEvil + tBlood + tSick > (tGood) + 20) ? Language.GetTextValue("DryadSpecialText.WorldDescriptionGrim") : ((tEvil + tBlood + tSick <= 5) ? Language.GetTextValue("DryadSpecialText.WorldDescriptionClose") : Language.GetTextValue("DryadSpecialText.WorldDescriptionWork")))));
            return $"{text} {arg}";
        }
    }
}
