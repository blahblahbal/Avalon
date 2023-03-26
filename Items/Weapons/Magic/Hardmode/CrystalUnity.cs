using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

class CrystalUnity : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 26;
        Item.reuseDelay = 14;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 9f;
        Item.mana = 14;
        Item.rare = ModContent.RarityType<Rarities.FractureRarity>();
        Item.width = dims.Width;
        Item.useTime = 11;
        Item.knockBack = 0f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.AmberShard>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 505000;
        Item.useAnimation = 11;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddRecipeGroup("Avalon:GemStaves", 2)
            .AddIngredient(ItemID.CrystalStorm)
            .AddIngredient(ModContent.ItemType<Material.TomeMats.ElementDiamond>(), 2)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int x = Main.rand.Next(10);

        if (x == 0) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.RubyShard>();
        if (x == 1) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.AmberShard>();
        if (x == 2) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.TopazShard>();
        if (x == 3) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.PeridotShard>();
        if (x == 4) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.EmeraldShard>();
        if (x == 5) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.TourmalineShard>();
        if (x == 6) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.SapphireShard>();
        if (x == 7) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.AmethystShard>();
        if (x == 8) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.DiamondShard>();
        if (x == 9) x = ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.ZirconShard>();

        for (int spread = 0; spread < 5; spread++)
        {
            float xVel = velocity.X;
            float yVel = velocity.Y;
            xVel += Main.rand.Next(-40, 41) * 0.05f;
            yVel += Main.rand.Next(-40, 41) * 0.05f;
            int dmg = Item.damage;
            if (x == ModContent.ProjectileType<Projectiles.Magic.CrystalUnity.ZirconShard>()) dmg = (int)(dmg * 2.5f);
            Projectile.NewProjectile(source, position.X, position.Y, xVel, yVel, x, (int)player.GetDamage(DamageClass.Magic).ApplyTo(dmg), knockback, player.whoAmI, 0f, 0f);
        }
        return false;
    }
}
