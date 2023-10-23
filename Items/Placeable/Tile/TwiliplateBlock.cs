using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class TwiliplateBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.TwiliplateBlock>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(40)
            .AddIngredient(ItemID.FallenStar)
            .AddIngredient(ItemID.StoneBlock, 40)
            .AddIngredient(ModContent.ItemType<Material.Ores.BismuthOre>())
            .AddTile(TileID.SkyMill)
            .Register(); //Change when alt added :)
    }
}
