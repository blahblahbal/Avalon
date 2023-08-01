using Avalon.Mounts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Mounts;

public class PeridotMinecart : ModItem
{
    public override void SetDefaults()
    {
        Item.mountType = ModContent.MountType<PeridotGemcart>();
        Item.width = 34;
        Item.height = 22;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.rare = ItemRarityID.Blue;
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Minecart)
            .AddIngredient(ModContent.ItemType<Other.LargePeridot>())
            .AddTile(TileID.Anvils)
            .Register();
    }
}
