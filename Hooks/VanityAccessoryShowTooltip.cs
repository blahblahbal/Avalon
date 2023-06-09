using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
internal class VanityAccessoryShowTooltip : ModHook
{
    protected override void Apply()
    {
        //On_Main.MouseText_DrawItemTooltip_GetLinesInfo += OnMouseText_DrawItemTooltip_GetLinesInfo;
    }
    public static void OnMouseText_DrawItemTooltip_GetLinesInfo(On_Main.orig_MouseText_DrawItemTooltip_GetLinesInfo orig, Item item, ref int yoyoLogo, ref int researchLine, float oldKB, ref int numLines, string[] toolTipLine, bool[] preFixLine, bool[] badPreFixLine, string[] toolTipNames)
    {
        if (item.social && (item.type == ItemID.HighTestFishingLine || item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity))//item.type == ModContent.ItemType<Items.Accessories.ShadowRing>()
        {
            item.social = false;
        }
        int yea;
        orig(item, ref yoyoLogo, ref researchLine, oldKB, ref numLines, toolTipLine, preFixLine, badPreFixLine, toolTipNames,out yea);
    }
}
