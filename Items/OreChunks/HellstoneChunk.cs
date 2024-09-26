using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.OreChunks;

class HellstoneChunk : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 100;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Green;
    }
    public override void AddRecipes()
    {
        Recipe.Create(ItemID.HellstoneBar)
            .AddIngredient(Type, 3)
            .AddIngredient(ItemID.Obsidian)
            .AddTile(TileID.Hellforge)
			.SortAfterFirstRecipesOf(ItemID.HellstoneBar)
			.Register();
    }
}
