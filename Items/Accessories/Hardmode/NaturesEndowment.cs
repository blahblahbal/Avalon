using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class NaturesEndowment : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = 32;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2, 36, 0);
        Item.height = 34;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.manaCost -= 0.25f;
        player.statManaMax2 += 20;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.NaturesGift, 4)
            .AddIngredient(ItemID.JungleRose)
            .AddIngredient(ModContent.ItemType<Material.Shards.ArcaneShard>(), 2)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
