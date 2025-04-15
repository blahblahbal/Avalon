using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class BismuthShortsword : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GoldShortsword);
        Item.damage = 14;
        Item.shootSpeed = 2.1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.BismuthShortsword>();
        Item.scale = 0.95f;
        Item.value = 9000;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 6).AddTile(TileID.Anvils).Register();
    }
}
