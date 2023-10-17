using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class BundleofHorseshoeBalloons : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 4);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState<RocketBottleJump>().Enable();
        player.GetJumpState<TsunamiInABottleJump>().Enable();
        player.GetJumpState<FartInAJarJump>().Enable();
        player.noFallDmg = true;
        player.hasLuck_LuckyHorseshoe = true;
        player.jumpBoost = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RocketHorseshoeBalloon>())
            .AddIngredient(ItemID.BalloonHorseshoeSharkron)
            .AddIngredient(ItemID.BalloonHorseshoeFart)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RocketinaBalloon>())
            .AddIngredient(ItemID.SharkronBalloon)
            .AddIngredient(ItemID.FartInABalloon)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.BalloonHorseshoeSharkron)
            .AddIngredient(ModContent.ItemType<RocketinaBalloon>())
            .AddIngredient(ItemID.FartInABalloon)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.BalloonHorseshoeFart)
            .AddIngredient(ModContent.ItemType<RocketinaBalloon>())
            .AddIngredient(ItemID.SharkronBalloon)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<BundleofBalloons>())
            .AddIngredient(ItemID.LuckyHorseshoe)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
