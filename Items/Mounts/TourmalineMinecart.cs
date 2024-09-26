using Avalon.Mounts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Mounts;

public class TourmalineMinecart : ModItem
{
    public override void SetDefaults()
    {
        Item.mountType = ModContent.MountType<TourmalineGemcart>();
        Item.width = 34;
        Item.height = 22;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.rare = ItemRarityID.Blue;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Minecart)
            .AddIngredient(ModContent.ItemType<Other.LargeTourmaline>())
            .AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.TopazMinecart)
			.Register();
    }
}
