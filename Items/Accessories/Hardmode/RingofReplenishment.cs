using Avalon.Items.Accessories.PreHardmode;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

internal class RingofReplenishment : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 9);
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RestorationBand>())
            .AddIngredient(ModContent.ItemType<StaminaFlower>())
            .AddIngredient(ItemID.CharmofMyths)
            .AddIngredient(ItemID.ManaFlower)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 60;
        player.manaFlower = true;
        player.GetModPlayer<AvalonStaminaPlayer>().StamFlower = true;
        player.pStone = true;
        player.lifeRegen += 2;
        player.manaRegen++;
        player.GetModPlayer<AvalonStaminaPlayer>().StaminaRegenCost = 1600;
    }
}
