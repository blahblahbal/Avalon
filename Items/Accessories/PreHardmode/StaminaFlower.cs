using Avalon.Common.Players;
using Avalon.Items.Potions.Other;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

internal class StaminaFlower : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 54);
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ModContent.ItemType<StaminaPotion>())
            //.AddIngredient(ModContent.ItemType<BandofStamina>())
            .AddIngredient(ItemID.JungleRose)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonStaminaPlayer>().StamFlower = true;
        player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 90;
    }
}
