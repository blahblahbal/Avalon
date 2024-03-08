using Avalon.Items.Material.Shards;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class ObsidianGlaive : ModItem // Obisidian Glaive
{
    int ShootTimes;
    public override void SetStaticDefaults()
    {
        ItemID.Sets.Spears[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.damage = 23;
        Item.UseSound = SoundID.Item1;
        Item.noUseGraphic = true;
        Item.scale = 1f;
        Item.shootSpeed = 4f;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.knockBack = 4.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ObsidianGlaive>();
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 20, 0, 0);
        Item.UseSound = SoundID.Item1;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        ShootTimes++;
        if (ShootTimes % 2 == 0)
        {
            float RotationAmount = Main.rand.NextFloat(0.1f, 0.4f);
            Projectile.NewProjectile(source, position, velocity.RotatedBy(RotationAmount * -player.direction), type, damage, knockback, player.whoAmI, 0, 0, RotationAmount * 2);
        }
        else
        {
            float RotationAmount = Main.rand.NextFloat(0.1f, 0.4f);
            Projectile.NewProjectile(source, position, velocity.RotatedBy(RotationAmount * -player.direction), type, damage, knockback, player.whoAmI, 0, 0, RotationAmount * 2);
        }
        return false;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Obsidian, 30)
            .AddIngredient(ItemID.Fireblossom, 3)
            .AddIngredient(ModContent.ItemType<FireShard>())
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}
