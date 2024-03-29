using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

class Boltstone : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.Gems;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Ores.Boltstone>();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 0, 50);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe(25)
            .AddIngredient(ModContent.ItemType<Consumables.StaminaCrystal>())
            .AddTile(TileID.Furnaces)
            .DisableDecraft()
            .Register();
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(ModContent.ItemType<Items.Consumables.StaminaCrystal>()).AddIngredient(this, 35).AddTile(TileID.Furnaces).Register();
    //    CreateRecipe(35).AddIngredient(ModContent.ItemType<Items.Consumables.StaminaCrystal>()).AddTile(TileID.Furnaces).Register();
    //}
}
