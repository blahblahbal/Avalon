using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

class BronzeBow : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinBow);
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 7)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
