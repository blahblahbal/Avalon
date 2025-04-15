using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Placeable.Tile;

namespace Avalon.Items.Placeable.Crafting;

public class IckyAltar : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.EvilAltarsPlaced>();
        Item.placeStyle = 2;
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
        Recipe.Create(ModContent.ItemType<IckyAltar>())
            .AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 50)
            .AddIngredient(ModContent.ItemType<YuckyBit>(), 10)
            .AddIngredient(ModContent.ItemType<Barfbush>(), 5)
            .AddIngredient(ItemID.SoulofNight, 20)
            .AddIngredient(ModContent.ItemType<Booger>(), 20)
            .AddIngredient(ModContent.ItemType<BacciliteBar>(), 25)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
