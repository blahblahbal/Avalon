using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Accessories.Vanity;

class PulseVeil : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.QuibopsRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 45);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.AdamantiteBar, 4)
            .AddIngredient(ItemID.HallowedBar, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.TitaniumBar, 4)
            .AddIngredient(ItemID.HallowedBar, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();

        //CreateRecipe(1)
        //    .AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 4)
        //    .AddIngredient(ItemID.HallowedBar, 4)
        //    .AddTile(TileID.MythrilAnvil)
        //    .Register();
    }
}
