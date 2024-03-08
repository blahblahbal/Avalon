using Avalon.Common.Players;
using Avalon.Items.Consumables;
using Avalon.Items.Material.Bars;
using Avalon.Items.Potions.Other;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class AnkletofAcceleration : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 1);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.runAcceleration *= 2f;
        player.moveSpeed++;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type).AddTile(TileID.WorkBenches)
    //        .AddIngredient(ModContent.ItemType<StaminaCrystal>(), 3)
    //        .AddRecipeGroup("Avalon:GoldBar", 4)
    //        .AddIngredient(ModContent.ItemType<StaminaPotion>(), 2)
    //        .AddTile(TileID.TinkerersWorkbench).Register();
    //}
}
