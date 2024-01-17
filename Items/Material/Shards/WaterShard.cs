using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Shards;

class WaterShard : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.Shards;
    }
    public override void SetDefaults()
    {
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.consumable = true;
        Item.useTime = 10;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.createTile = ModContent.TileType<Tiles.Shards>();
        Item.placeStyle = 6 + 9;
        Item.rare = ItemRarityID.Orange;
        Item.Size = new(20);
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 6, 0);
    }
}
