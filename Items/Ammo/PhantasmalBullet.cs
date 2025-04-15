using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class PhantasmalBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.shootSpeed = 11f;
        Item.damage = 18;
        Item.ammo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ModContent.RarityType<Rarities.QuibopsRarity>();
        Item.width = dims.Width;
        Item.knockBack = 6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.PhantasmalBullet>();
        Item.maxStack = 9999;
        Item.value = 1200;
        Item.height = dims.Height;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ModContent.ItemType<SpectralBullet>(), 70)
    //        .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 2)
    //        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
    //        .Register();
    //}
    //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    //{
    //    Vector2 pos = player.Center + new Vector2(50, 0).RotatedBy(player.AngleTo(Main.MouseWorld));
    //    Projectile.NewProjectile(source, pos.X, pos.Y, velocity.X * 3, velocity.Y * 3, Type, damage, knockback);
    //    return false;
    //}
}
