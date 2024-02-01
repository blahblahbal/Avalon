using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class GiftofStarpower : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 8, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.manaCost -= 0.2f;
        player.statManaMax2 += 40;
        player.manaFlower = true;
        player.GetDamage(DamageClass.Magic) += 0.15f;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.ManaFlower)
            .AddIngredient(ItemID.BandofStarpower, 2)
            .AddIngredient(ModContent.ItemType<NaturesEndowment>())
            .AddIngredient(ItemID.SorcererEmblem)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
