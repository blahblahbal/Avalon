using System.Collections.Generic;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Expert;

public class AstrallineArtifact : ModItem
{
    private int AstralCooldown;

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = 1;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 15);
        Item.height = dims.Height;
        Item.expert = true;
    }

    //public override void ModifyTooltips(List<TooltipLine> tooltips)
    //{
    //    var AstralCooldownInfo = new TooltipLine(Mod, "Controls:AstralCooldown", "Time before you can astral project: " + AstralCooldown / 60 + " seconds");
    //    tooltips.Add(AstralCooldownInfo);
    //}

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().AstralProject = true;
        //AstralCooldown = player.GetModPlayer<AvalonPlayer>().AstralCooldown;
    }
}
