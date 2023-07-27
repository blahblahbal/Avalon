using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode
{
    public class Blunderblight : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 10;
            Item.DefaultToRangedWeapon(7, AmmoID.Bullet, 50, 5, false);
            Item.damage = 9;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1, 50, 0);
            Item.UseSound = SoundID.Item36;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            UseStyles.ShotgunStyle(player, 0.1f, 5f, 5f);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 5; i++)
            Projectile.NewProjectile(source,position,velocity.RotatedByRandom(0.28f) * Main.rand.NextFloat(0.7f,1f),type,damage,knockback,player.whoAmI);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -1);
        }
    }
}
