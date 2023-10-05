using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class OxygenTank : ModItem
{    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 100000;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.Suffocation] = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddIngredient(ItemID.ChlorophyteBar, 20)
            .AddIngredient(ItemID.GillsPotion, 2)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
