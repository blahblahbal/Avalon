using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.ResistantWood;

class ResistantWoodPlatform : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodPlatform>();
        Item.consumable = true;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.scale = 1f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    //public override bool? CanBurnInLava()
    //{
    //    return false;
    //}
    public override void AddRecipes()
    {
        CreateRecipe(2).AddIngredient(ModContent.ItemType<Tile.ResistantWood>()).Register();
        Recipe.Create(ModContent.ItemType<Tile.ResistantWood>()).AddIngredient(this, 2).Register();
    }
}