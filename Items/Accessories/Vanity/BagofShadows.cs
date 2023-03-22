using Avalon.Common;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

internal class BagofShadows : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
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
            .AddIngredient(ItemID.DemoniteBar, 15)
            //.AddIngredient(ItemID.CursedFlame, 5)
            .AddIngredient(ItemID.EbonstoneBlock, 50)
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

        int dust = Dust.NewDust(player.position, player.width - 20, player.height, DustID.Shadowflame, 0f, 0f, 100,
            Color.White, 2f);
        Main.dust[dust].noGravity = true;
    }
}
