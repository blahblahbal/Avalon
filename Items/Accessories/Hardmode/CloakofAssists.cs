using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class CloakofAssists : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 8, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.honeyCombItem = Item;
        player.starCloakItem = Item;
        player.panic = player.GetModPlayer<AvalonPlayer>().LightningInABottle = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.StarCloak)
            .AddIngredient(ItemID.PanicNecklace)
            .AddIngredient(ItemID.HoneyComb)
            .AddIngredient(ModContent.ItemType<LightninginaBottle>())
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.StarCloak)
            .AddIngredient(ItemID.SweetheartNecklace)
            .AddIngredient(ModContent.ItemType<LightninginaBottle>())
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.BeeCloak)
            .AddIngredient(ItemID.PanicNecklace)
            .AddIngredient(ModContent.ItemType<LightninginaBottle>())
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
}
