using Avalon.Projectiles.Magic;
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
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 46;
        Item.reuseDelay = 14;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 13f;
        Item.mana = 14;
        Item.rare = ModContent.RarityType<Rarities.FractureRarity>();
        Item.width = dims.Width;
        Item.useTime = 11;
        Item.knockBack = 0f;
        Item.shoot = ModContent.ProjectileType<CrystalUnityShard>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 505000;
        Item.useAnimation = 11;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item8;
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

        for (int spread = 0; spread < 3; spread++)
        {
            int dmg = Item.damage;
            if (x == 10) dmg = (int)(dmg * 2.5f);
            Projectile.NewProjectile(source, position,velocity * Main.rand.NextFloat(0.8f,1.2f), ModContent.ProjectileType<CrystalUnityShard>(), (int)player.GetDamage(DamageClass.Magic).ApplyTo(dmg), knockback, player.whoAmI, Main.rand.NextFloat() - 0.5f, 0f,x);
        }
        return false;
    }
}
