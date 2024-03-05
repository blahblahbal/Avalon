using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class SpectralBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.shootSpeed = 11f;
        Item.damage = 12;
        Item.ammo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ModContent.RarityType<Rarities.QuibopsRarity>();
        Item.width = dims.Width;
        Item.knockBack = 6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.SpectralBullet>();
        Item.maxStack = 9999;
        Item.value = 1200;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(70)
            .AddIngredient(ItemID.MusketBall, 70)
            .AddIngredient(ItemID.Ectoplasm, 2)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    //{
    //    Vector2 pos = player.Center + new Vector2(1000, 0).RotatedBy(player.AngleTo(Main.MouseWorld));
    //    Projectile.NewProjectile(source, pos.X, pos.Y, velocity.X * 3, velocity.Y * 3, Type, damage, knockback);
    //    return false;
    //}
}
