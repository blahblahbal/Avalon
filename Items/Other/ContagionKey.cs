using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

internal class ContagionKey : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.height = dims.Height;
    }
    public override bool ConsumeItem(Player player)
    {
        return NPC.downedPlantBoss;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        if (!NPC.downedPlantBoss)
        {
            var thing = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("LegacyTooltip.59"));
            tooltips.Add(thing);
        }
        else
        {
            var thing = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("Mods.Avalon.ContagionKey.AfterPlantera"));
            tooltips.Add(thing);
        }
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe()
    //        .AddIngredient(ItemID.TempleKey)
    //        .AddIngredient(ModContent.ItemType<ContagionKeyMold>())
    //        .AddIngredient(ItemID.SoulofFright, 5)
    //        .AddIngredient(ItemID.SoulofMight, 5)
    //        .AddIngredient(ItemID.SoulofSight, 5)
    //        .AddTile(TileID.MythrilAnvil)
    //        .Register();
    //}
}
