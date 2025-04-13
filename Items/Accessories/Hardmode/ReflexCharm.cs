using System.Linq;
using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class ReflexCharm : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 2;
        Item.rare = ItemRarityID.LightRed;
        Item.width = 28;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 1, 8);
        Item.height = 32;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.CobaltShield)
            .AddIngredient(ItemID.SoulofSight, 8)
            .AddIngredient(ItemID.LightShard, 3)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().Reflex = true;
    }
}
