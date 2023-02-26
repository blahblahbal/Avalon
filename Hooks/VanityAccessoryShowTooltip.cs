using Terraria.ModLoader;
using ExxoAvalonOrigins.Common;
using Terraria.ID;
using Terraria;

namespace ExxoAvalonOrigins.Hooks;

[Autoload(Side = ModSide.Client)]
internal class VanityAccessoryShowTooltip : ModHook
{
    protected override void Apply()
    {
        On_Main.MouseText_DrawItemTooltip_GetLinesInfo += OnMouseText_DrawItemTooltip_GetLinesInfo;
    }
    public static void OnMouseText_DrawItemTooltip_GetLinesInfo(On_Main.orig_MouseText_DrawItemTooltip_GetLinesInfo orig, Item item, ref int yoyoLogo, ref int researchLine, float oldKB, ref int numLines, string[] toolTipLine, bool[] preFixLine, bool[] badPreFixLine, string[] toolTipNames)
    {
        if (item.social && (item.type == ItemID.HighTestFishingLine || //item.type == ModContent.ItemType<Items.Accessories.ShadowRing>() ||
            item.type == ModContent.ItemType<Items.Accessories.PreHardmode.ShadowCharm>() || item.type == ModContent.ItemType<Items.Accessories.PreHardmode.ShadowPulse>() ||
            //item.type == ModContent.ItemType<Items.Accessories.PreHardmode.ShadowPulseBag>() || 
            item.type == ModContent.ItemType<Items.Accessories.PreHardmode.BagofBlood>() ||
            item.type == ModContent.ItemType<Items.Accessories.PreHardmode.BagofFire>() || //item.type == ModContent.ItemType<Items.Accessories.BagofFrost>() ||
            //item.type == ModContent.ItemType<Items.Accessories.PreHardmode.BagofHallows>() || item.type == ModContent.ItemType<Items.Accessories.BagofIck>() ||
            item.type == ModContent.ItemType<Items.Accessories.PreHardmode.BagofShadows>() || //item.type == ModContent.ItemType<Items.Accessories.Omnibag>() ||
            //item.type == ModContent.ItemType<Items.Accessories.CloudGloves>() || item.type == ModContent.ItemType<Items.Accessories.ObsidianGlove>() ||
            item.type == ModContent.ItemType<Items.Accessories.PreHardmode.PulseCharm>()))
        {
            item.social = false;
        }
        orig(item, ref yoyoLogo, ref researchLine, oldKB, ref numLines, toolTipLine, preFixLine, badPreFixLine, toolTipNames);
    }
}
