using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class TeleportScroll : ModItem
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
        CreateRecipe(1)
            .AddIngredient(ItemID.Book)
            .AddIngredient(ModContent.ItemType<Material.ChaosDust>(), 15)
            .AddIngredient(ItemID.SoulofSight, 5)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>())
            .AddTile(TileID.Bookcases)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            player.GetModPlayer<AvalonStaminaPlayer>().TeleportUnlocked = true;
        }
    }
    //public override bool CanUseItem(Player player)
    //{
    //    return !player.GetModPlayer<AvalonStaminaPlayer>().TeleportUnlocked;
    //}
    //public override bool? UseItem(Player player)
    //{
    //    player.GetModPlayer<AvalonStaminaPlayer>().TeleportUnlocked = true;
    //    return true;
    //}
}
