using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
class TerrorPenguinsEarpiece : ModItem
{
    public override void SetStaticDefaults()
    {
        ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.vanity = true;
        Item.rare = ItemRarityID.Purple;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.height = dims.Height;
    }
}
