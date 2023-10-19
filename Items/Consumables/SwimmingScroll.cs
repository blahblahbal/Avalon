using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class SwimmingScroll : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.rare = ItemRarityID.Green;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Scroll");
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Book)
            .AddIngredient(ItemID.Flipper)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>())
            .AddTile(TileID.Bookcases)
            .Register();
    }
    public override bool CanUseItem(Player player)
    {
        return !player.GetModPlayer<AvalonStaminaPlayer>().SwimmingUnlocked;
    }
    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<AvalonStaminaPlayer>().SwimmingUnlocked = true;
        return true;
    }
}
