using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

class BronzeBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Material;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.PlacedBars>();
        Item.placeStyle = 18;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.value = Item.sellPrice(0, 0, 2, 40);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Ores.BronzeOre>(), 3)
            .AddTile(TileID.Furnaces)
            .Register();
    }
}
