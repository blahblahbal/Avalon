using System;
using Avalon.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Blah;

public class BlahStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.staff[Item.type] = true;
        Rectangle dims = this.GetDims();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Magic;
        Item.useTurn = true;
        Item.damage = 278;
        Item.mana = 19;
        Item.noMelee = true;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.width = dims.Width;
        Item.shoot = 1;
        Item.knockBack = 16f;
        Item.useTime = 15;
        Item.value = Item.sellPrice(2);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.shootSpeed = 20f;
        Item.UseSound = SoundID.Item88;
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe(1)
    //        .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 45)
    //        .AddIngredient(ModContent.ItemType<Placeable.Bar.SuperhardmodeBar>(), 40)
    //        .AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 45)
    //        .AddIngredient(ItemID.LunarFlareBook)
    //        .AddIngredient(ModContent.ItemType<PyroscoricFlareStaff>())
    //        .AddIngredient(ModContent.ItemType<OpalStaff>())
    //        .AddIngredient(ModContent.ItemType<OnyxStaff>())
    //        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
    //        .Register();
    //}
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        return false;
    }
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            for (int num9 = 0; num9 < 1; num9++)
            {
                Vector2 vector = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                vector.X = (vector.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                vector.Y -= 100 * num9;
                float num311 = Main.mouseX + Main.screenPosition.X - vector.X;
                float num312 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                float ai2 = num312 + vector.Y;
                if (num312 < 0f)
                {
                    num312 *= -1f;
                }
                if (num312 < 20f)
                {
                    num312 = 20f;
                }
                float num313 = (float)Math.Sqrt(num311 * num311 + num312 * num312);
                num313 = Item.shootSpeed / num313;
                num311 *= num313;
                num312 *= num313;
                Vector2 vector3 = new Vector2(num311, num312) / 2f;
                int p = Projectile.NewProjectile(Projectile.GetSource_None(), vector.X, vector.Y, vector3.X, vector3.Y, ModContent.ProjectileType<Projectiles.Magic.BlahMeteor>(), (int)(player.GetDamage(DamageClass.Magic).ApplyTo(Item.damage)), Item.knockBack, player.whoAmI, 0f, ai2);
                Main.projectile[p].owner = player.whoAmI;
            }
        }
        return true;
    }
}
