using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class RocketHorseshoeBalloon : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 3);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetJumpState<RocketBottleJump>().Enable();
        player.jumpBoost = true;
        player.noFallDmg = true;
        player.hasLuck_LuckyHorseshoe = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RocketinaBalloon>())
            .AddIngredient(ItemID.LuckyHorseshoe)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
