using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Balloon)]
public class MegaBundleofHorseshoeBalloons : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = 34;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 6);
        Item.height = 56;
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
        player.noFallDmg = true;
        player.hasLuck_LuckyHorseshoe = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<MegaBundleofBalloons>())
            .AddIngredient(ItemID.LuckyHorseshoe)
            .AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ModContent.ItemType<MegaBundleofBalloons>())
			.Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<BundleofHorseshoeBalloons>())
            .AddIngredient(ItemID.BundleofBalloons)
            .AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ModContent.ItemType<MegaBundleofBalloons>())
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.HorseshoeBundle)
            .AddIngredient(ModContent.ItemType<BundleofBalloons>())
            .AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ModContent.ItemType<MegaBundleofBalloons>())
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<BundleofHorseshoeBalloons>())
            .AddIngredient(ItemID.HorseshoeBundle)
            .AddTile(TileID.TinkerersWorkbench)
			.SortAfterFirstRecipesOf(ModContent.ItemType<MegaBundleofBalloons>())
            .Register();
    }
}
