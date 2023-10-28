using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class BronzeShortsword : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinShortsword);
        Item.damage = 7;
        Item.shootSpeed = 2.1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.BronzeShortsword>();
        Item.scale = 0.95f;
        Item.value = 1500;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 7)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
