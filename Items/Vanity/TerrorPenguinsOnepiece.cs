using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Body)]
class TerrorPenguinsOnepiece : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Purple;
        Item.width = dims.Width;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.height = dims.Height;
    }
}