using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class ZincShortsword : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.SilverShortsword);
        Item.damage = 11;
        Item.shootSpeed = 2.1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ZincShortsword>();
        Item.scale = 0.95f;
        Item.value = 4500;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 7)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
