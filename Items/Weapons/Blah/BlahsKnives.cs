using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah;

internal class BlahsKnives : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 95;
        Item.mana = 14;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.shootSpeed = 15f;
        Item.noMelee = true;
        Item.rare = ModContent.RarityType<BlahRarity>();
        Item.width = dims.Width;
        Item.useTime = 14;
        Item.knockBack = 3.75f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.BlahKnife>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 50);
        Item.useAnimation = 14;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item39;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        int numberProjectiles = Main.rand.Next(4, 8); // AvalonGlobalProjectile.HowManyProjectiles(4, 8);
        for (int i = 0; i < numberProjectiles; i++)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage,
                knockback, player.whoAmI);
        }

        return false;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ModContent.ItemType<Magic.PhantomKnives>())
    //        .AddIngredient(ModContent.ItemType<Melee.KnivesoftheCorruptor>())
    //        .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 40)
    //        .AddIngredient(ModContent.ItemType<Placeable.Bar.SuperhardmodeBar>(), 35)
    //        .AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 40)
    //        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
    //        .Register();
    //}
}
