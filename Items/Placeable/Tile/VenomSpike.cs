using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Placeable.Tile;

class VenomSpike : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.VenomSpike>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.value = 50;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(40)
            .AddIngredient(ItemID.Spike, 40)
            .AddIngredient(ItemID.FlaskofVenom)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
