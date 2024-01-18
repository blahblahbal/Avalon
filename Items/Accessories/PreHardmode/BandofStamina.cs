using Avalon.Common.Players;
using Avalon.Items.Consumables;
using Avalon.Items.Potions.Other;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.HandsOn)]
internal class BandofStamina : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 50000;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 90;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type).AddTile(TileID.WorkBenches)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>(), 3)
            .AddIngredient(ItemID.Shackle, 2)
            .AddIngredient(ModContent.ItemType<StaminaPotion>(), 2)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
}
