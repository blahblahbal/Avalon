using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Accessories.Vanity;

internal class BagofBlood : ModItem
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
            .AddIngredient(ItemID.Vertebrae, 15)
            //.AddIngredient(ItemID.Ichor, 10)
            .AddIngredient(ItemID.CrimstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<CorruptShard>(), 5)
            .AddTile(TileID.Hellforge)
            .Register();
    }
    public override void UpdateVanity(Player player)
    {
        if (!(player.velocity.Length() > 0))
        {
            return;
        }

        int dust1 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.TheDestroyer, 0f, 0f,
            100,
            Color.White, 0.9f);
        Main.dust[dust1].noGravity = true;
        int dust2 = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Blood, 0f, 0f, 100,
            Color.White,
            1.5f);
        Main.dust[dust2].noGravity = true;
    }
}
