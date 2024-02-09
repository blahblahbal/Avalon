using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.SoulCandles;

class MightCandle : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.SoulCandles.MightCandle>();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 10);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.SoulofMight, 15).AddIngredient(ItemID.Candle).AddTile(TileID.MythrilAnvil).Register();
        CreateRecipe(1).AddIngredient(ItemID.SoulofMight, 15).AddIngredient(ItemID.PlatinumCandle).AddTile(TileID.MythrilAnvil).Register();
        CreateRecipe(1).AddIngredient(ItemID.SoulofMight, 15).AddIngredient(ModContent.ItemType<Furniture.BismuthCandle>()).AddTile(TileID.MythrilAnvil).Register();
    }
}
