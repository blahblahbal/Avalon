using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class MiningScroll : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.rare = ItemRarityID.Green;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = new SoundStyle("Avalon/Sounds/Item/Scroll");
        Item.accessory = true;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Leather, 2)
            .AddIngredient(ItemID.MiningPotion, 2)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>())
            .AddTile(TileID.Loom)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            player.GetModPlayer<AvalonStaminaPlayer>().MiningSpeedUnlocked = true;
        }
    }
}
