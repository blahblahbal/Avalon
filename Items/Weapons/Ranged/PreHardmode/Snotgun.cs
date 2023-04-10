using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode
{
    public class Snotgun : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToRangedWeapon(10, AmmoID.Bullet, 46, 6, false);
            Item.damage = 6;
            Item.rare = 1;
            Item.value = Item.sellPrice(0, 1, 50, 0);
            Item.UseSound = SoundID.Item36;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 5; i++)
            Projectile.NewProjectile(source,position,velocity.RotatedByRandom(0.2f) * Main.rand.NextFloat(0.7f,1f),type,damage,knockback,player.whoAmI);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -1);
        }
    }
}
