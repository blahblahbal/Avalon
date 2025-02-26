using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.Material.OreChunks;

class HallowedChunk : ModItem
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
        Item.rare = ItemRarityID.Pink;
    }
    public override void AddRecipes()
    {
        Recipe.Create(ItemID.HallowedBar)
            .AddIngredient(Type, 5)
            .AddTile(TileID.AdamantiteForge)
			.SortAfterFirstRecipesOf(ItemID.HallowedBar)
			.Register();
    }
}
