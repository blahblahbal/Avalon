using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Shards;

class FireShard : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.Size = new(20);
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 6, 0);
    }
}
