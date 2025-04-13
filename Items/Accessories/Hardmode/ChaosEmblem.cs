using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class ChaosEmblem : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = 32;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 6, 0, 0);
        Item.height = 30;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.3f);
        player.GetDamage(DamageClass.Generic) += 0.1f;
        player.GetModPlayer<AvalonPlayer>().AllMaxCrit(8);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<ChaosCrystal>())
            .AddIngredient(ItemID.AvengerEmblem)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
