using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class ChaosEye : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = 34;
        Item.value = 400000;
        Item.accessory = true;
        Item.height = 34;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().ChaosCharm = true;
        player.GetCritChance(DamageClass.Generic) += 8;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<ChaosCharm>())
            .AddIngredient(ItemID.EyeoftheGolem)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
