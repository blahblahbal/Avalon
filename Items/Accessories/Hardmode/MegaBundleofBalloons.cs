using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class MegaBundleofBalloons : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 5);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState<RocketBottleJump>().Enable();
        player.GetJumpState<TsunamiInABottleJump>().Enable();
        player.GetJumpState<FartInAJarJump>().Enable();
        player.GetJumpState<SandstormInABottleJump>().Enable();
        player.GetJumpState<BlizzardInABottleJump>().Enable();
        player.GetJumpState<CloudInABottleJump>().Enable();
        player.jumpBoost = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<BundleofBalloons>())
            .AddIngredient(ItemID.BundleofBalloons)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
