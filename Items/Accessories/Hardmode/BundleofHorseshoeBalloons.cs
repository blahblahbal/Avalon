using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Balloon)]
public class BundleofHorseshoeBalloons : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = 30;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 4);
        Item.height = 38;
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
            .AddIngredient(ModContent.ItemType<BundleofBalloons>())
            .AddIngredient(ItemID.LuckyHorseshoe)
            .AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ItemID.HorseshoeBundle)
			.Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RocketinaBalloon>())
            .AddIngredient(ItemID.SharkronBalloon)
            .AddIngredient(ItemID.FartInABalloon)
            .AddIngredient(ItemID.LuckyHorseshoe)
            .AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ItemID.HorseshoeBundle)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RocketHorseshoeBalloon>())
            .AddRecipeGroup("Avalon:SharkronBalloons")
            .AddRecipeGroup("Avalon:FartBalloons")
            .AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ItemID.HorseshoeBundle)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.BalloonHorseshoeSharkron)
            .AddRecipeGroup("Avalon:RocketBalloons")
            .AddRecipeGroup("Avalon:FartBalloons")
            .AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ItemID.HorseshoeBundle)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.BalloonHorseshoeFart)
            .AddRecipeGroup("Avalon:RocketBalloons")
            .AddRecipeGroup("Avalon:SharkronBalloons")
            .AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ItemID.HorseshoeBundle)
            .Register();
    }
}
