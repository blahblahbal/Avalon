using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Avalon.Items.Material;
using System;

namespace Avalon.Items.Weapons.Magic.Hardmode;

class Sunstorm : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 50;
        Item.autoReuse = true;
        Item.shootSpeed = 12f;
        Item.mana = 17;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 60;
        Item.knockBack = 3f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.Sunstorm>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 5);
        Item.useAnimation = 60;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item8;
        Item.noMelee = true;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(10, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int j = 0; j < 12; j++)
        {
            float x = player.position.X + Main.rand.Next(-400, 400);
            float y = player.position.Y - 90;
            float num9 = player.position.X + player.width / 2 - x;
            float num10 = player.position.Y + player.height / 2 - y;
            num9 += Main.rand.Next(-100, 101);
            int num11 = 23;
            float num12 = (float)Math.Sqrt((double)(num9 * num9 + num10 * num10));
            num12 = num11 / num12;
            num9 *= num12;
            num10 *= num12;
            int num13 = Projectile.NewProjectile(source, x, y, num9, 0, type, damage, knockback, player.whoAmI, 0f, 0f);
            Main.projectile[num13].ai[1] = Main.rand.Next(2);
        }
        return false;
    }
}
