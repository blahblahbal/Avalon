using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    class SanguineKabuto : ModItem
    {
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 1, 20);
            Item.rare = ItemRarityID.Orange;
        }
    }
}
