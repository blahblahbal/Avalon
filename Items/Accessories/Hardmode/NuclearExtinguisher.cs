using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class NuclearExtinguisher : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 200000;
        Item.height = dims.Height;
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
