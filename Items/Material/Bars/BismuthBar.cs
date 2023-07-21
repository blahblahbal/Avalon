using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

class BismuthBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.PrehardmodeBars;
    }
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.PlacedBars>();
        Item.placeStyle = 21;
        Item.rare = ItemRarityID.White;
        Item.useTime = 10;
        Item.value = Item.sellPrice(0, 0, 15, 0);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Ores.BismuthOre>(), 4)
            .AddTile(TileID.Furnaces)
            .Register();
    }
}
