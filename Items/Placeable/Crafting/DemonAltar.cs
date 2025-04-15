using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class DemonAltar : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = Item.autoReuse = true;
        Item.createTile = ModContent.TileType<Tiles.EvilAltarsPlaced>();
        Item.placeStyle = 0;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.EbonstoneBlock, 50)
            .AddIngredient(ItemID.RottenChunk, 10)
            .AddIngredient(ItemID.Deathweed, 5)
            .AddIngredient(ItemID.SoulofNight, 20)
            .AddIngredient(ItemID.ShadowScale, 20)
            .AddIngredient(ItemID.DemoniteBar, 25)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
