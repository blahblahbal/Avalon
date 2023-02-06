using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
class BloodstainedHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Bloodstained Helmet");
        //Tooltip.SetDefault("Shows the location of treasures and ores\nWorks in the vanity slot");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.vanity = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 90, 0);
        Item.height = dims.Height;
    }
}
