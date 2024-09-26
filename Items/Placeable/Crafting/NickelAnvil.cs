using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

class NickelAnvil : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.LeadAnvil);
        Item.createTile = ModContent.TileType<Tiles.NickelAnvil>();
        Item.placeStyle = 0;
        Item.useTime = 10;
        Item.value = Item.sellPrice(0, 0, 13, 0);
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 5)
            .AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ItemID.IronAnvil)
			.Register();
    }
}
