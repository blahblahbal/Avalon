using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class NuclearExtinguisher : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 38;
        Item.rare = ItemRarityID.Yellow;
        Item.accessory = true;
        Item.value = 200000;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Blackout] = true;
        player.buffImmune[BuffID.CursedInferno] = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<GreekExtinguisher>())
            .AddIngredient(ModContent.ItemType<SixHundredWattLightbulb>())
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
}
