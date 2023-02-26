using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExxoAvalonOrigins.Items.Material.Shards;

namespace ExxoAvalonOrigins.Items.Accessories.Vanity;

internal class BagofFire : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.width = 20;
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 1);
        Item.height = 20;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            UpdateVanity(player);
        }
    }
    public override void AddRecipes()
    {
        Recipe.Create(1)
            .AddIngredient(ItemID.Fireblossom, 15)
            .AddIngredient(ItemID.HellstoneBar, 10)
            .AddIngredient(ItemID.AshBlock, 50)
            .AddIngredient(ModContent.ItemType<FireShard>(), 5)
            .AddTile(TileID.Hellforge).Register();
    }
    public override void UpdateVanity(Player player)
    {
        if (!(player.velocity.Length() > 0))
        {
            return;
        }

        int dust = Dust.NewDust(player.position, player.width + 20, player.height + 20, DustID.Torch, 0f, 0f, 100,
            Color.White, 2f);
        Main.dust[dust].noGravity = true;
    }
}
