using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class ForsakenCross : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.longInvince = true;
        if (player.immune)
        {
            player.GetCritChance(DamageClass.Generic) += 7;
            player.GetDamage(DamageClass.Generic) += 0.07f;
        }
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<ForsakenRelic>())
            .AddIngredient(ItemID.CrossNecklace)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
