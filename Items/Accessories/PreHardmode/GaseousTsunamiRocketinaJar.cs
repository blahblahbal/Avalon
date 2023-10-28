using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

internal class GaseousTsunamiRocketinaJar : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState<RocketBottleJump>().Enable();
        player.GetJumpState<FartInAJarJump>().Enable();
        player.GetJumpState<TsunamiInABottleJump>().Enable();
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.FartinaJar)
            .AddIngredient(ItemID.TsunamiInABottle)
            .AddIngredient(ModContent.ItemType<RocketinaBottle>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
