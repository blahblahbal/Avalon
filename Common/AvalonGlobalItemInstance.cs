using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Common;

public class AvalonGlobalItemInstance : GlobalItem
{
    public override bool InstancePerEntity => true;
    public int HealStamina { get; set; }
    public bool Tome { get; set; }
    public bool WasWiring { get; set; }
    public bool TomeMaterial { get; set; }
    public bool WorksInVanity { get; set; }
    public int RiftTimeLeft { get; set; }

    public override GlobalItem Clone(Item item, Item itemClone)
    {
        var clone = (AvalonGlobalItemInstance)base.Clone(item, itemClone);
        clone.HealStamina = HealStamina;
        clone.WasWiring = WasWiring;
        clone.Tome = Tome;
        clone.TomeMaterial = TomeMaterial;
        clone.WorksInVanity = WorksInVanity;
        clone.RiftTimeLeft = RiftTimeLeft;
        return clone;
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        TooltipLine? tooltipLine = tooltips.Find(x => x.Name == "ItemName" && x.Mod == "Terraria");
        TooltipLine? lineKnockback = tooltips.Find(x => x.Name == "Knockback" && x.Mod == "Terraria");
        TooltipLine? lineSpeed = tooltips.Find(x => x.Name == "Speed" && x.Mod == "Terraria");
        if (lineKnockback != null &&
            LanguageManager.Instance.ActiveCulture.LegacyId == (int)GameCulture.CultureName.English)
        {
            if (item.knockBack is > 0f and < 1.5f)
            {
                lineKnockback.Text = Language.GetTextValue("Mods.Avalon.Knockback.Puny");
            }
            if (item.knockBack > 15f)
            {
                lineKnockback.Text = Language.GetTextValue("Mods.Avalon.Knockback.Absurd");
            }
            if (item.knockBack > 17f)
            {
                lineKnockback.Text = Language.GetTextValue("Mods.Avalon.Knockback.Ridiculous");
            }
            if (item.knockBack > 19f)
            {
                lineKnockback.Text = Language.GetTextValue("Mods.Avalon.Knockback.Godly");
            }
            if (item.type == ItemID.BatBat)
            {
                lineKnockback.Text = lineKnockback.Text.Replace("knockback", "knockbat");
            }
        }
        if (lineSpeed == null)
        {
            return;
        }
        if (LanguageManager.Instance.ActiveCulture.LegacyId != (int)GameCulture.CultureName.English)
        {
            return;
        }
        if (item.useAnimation <= 5f)
        {
            lineSpeed.Text = Language.GetTextValue("Mods.Avalon.Speed.Lightning");
        }
        if (item.useAnimation >= 58f)
        {
            lineSpeed.Text = Language.GetTextValue("Mods.Avalon.Speed.Slowpoke");
        }
    }
    public override bool? PrefixChance(Item item, int pre, UnifiedRandom rand)
    {
        if (item.IsArmor() && pre == -3)
        {
            return true;
        }
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome && (pre == -1 || pre == -3))
        {
            return false;
        }

        return base.PrefixChance(item, pre, rand);
    }
}
