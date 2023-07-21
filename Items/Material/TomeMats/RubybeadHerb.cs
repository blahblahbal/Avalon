using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

class RubybeadHerb : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.Size = new(20);
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 9999;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
    }
}
