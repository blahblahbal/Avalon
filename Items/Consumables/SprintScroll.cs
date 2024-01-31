using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class SprintScroll : ModItem
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
            .AddIngredient(ItemID.Book)
            .AddIngredient(ItemID.HermesBoots)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>())
            .AddTile(TileID.Bookcases)
            .Register();
        
        CreateRecipe()
            .AddIngredient(ItemID.Book)
            .AddIngredient(ItemID.FlurryBoots)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>())
            .AddTile(TileID.Bookcases)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.Book)
            .AddIngredient(ItemID.SailfishBoots)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>())
            .AddTile(TileID.Bookcases)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.Book)
            .AddIngredient(ItemID.SandBoots)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>())
            .AddTile(TileID.Bookcases)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            player.GetModPlayer<AvalonStaminaPlayer>().SprintUnlocked = true;
        }
    }
}
