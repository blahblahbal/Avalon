using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class BundleofBalloons : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 3);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState<RocketBottleJump>().Enable();
        player.GetJumpState<TsunamiInABottleJump>().Enable();
        player.GetJumpState<FartInAJarJump>().Enable();
        player.jumpBoost = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RocketinaBalloon>())
            .AddIngredient(ItemID.SharkronBalloon)
            .AddIngredient(ItemID.FartInABalloon)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<GaseousTsunamiRocketinaJar>())
            .AddIngredient(ItemID.ShinyRedBalloon, 3)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
