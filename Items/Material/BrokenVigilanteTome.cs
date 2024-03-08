using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class BrokenVigilanteTome : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.BrokenHeroSword)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>())
            .Register();

        Recipe.Create(ItemID.BrokenHeroSword)
            .AddIngredient(Type)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>())
            .Register();
    }
}
