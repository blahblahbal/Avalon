using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Herbs;

class BarfbushSeeds : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;

        Item.ResearchUnlockCount = 25;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.AlchemySeeds;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Herbs.Barfbush>();
        Item.placeStyle = 0;
        Item.width = dims.Width;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 90;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe(2).AddIngredient(ModContent.ItemType<Tile.ChunkstoneBlock>(), 5).AddIngredient(ModContent.ItemType<Material.YuckyBit>(), 2).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
    //}
}
