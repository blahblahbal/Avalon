using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class NickelShortsword : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.IronShortsword);
        Item.damage = 9;
        Item.shootSpeed = 2.1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.NickelShortsword>();
        Item.scale = 1f;
        Item.value = 1800;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 6)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
