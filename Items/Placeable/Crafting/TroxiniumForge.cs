using Avalon.Items.OreChunks;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

class TroxiniumForge : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.TroxiniumForge>();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 50000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Ores.TroxiniumOre>(), 30)
            .AddIngredient(ItemID.Hellforge)
            .AddTile(TileID.MythrilAnvil)
			.SortAfterFirstRecipesOf(ItemID.AdamantiteForge)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<TroxiniumChunk>(), 30)
			.AddIngredient(ItemID.Hellforge)
			.AddTile(TileID.MythrilAnvil)
			.SortAfterFirstRecipesOf(Type)
			.Register();
    }
}
