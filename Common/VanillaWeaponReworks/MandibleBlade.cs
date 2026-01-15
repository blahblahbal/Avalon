using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Projectiles.Melee.Swords;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.VanillaWeaponReworks
{
    public class MandibleBlade : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.AntlionClaw)
            {
                entity.useTurn = false;
                entity.scale = 1;
                entity.shootsEveryUse = true;
                entity.noMelee = true;
                entity.shoot = ModContent.ProjectileType<SandSlash>();
                entity.knockBack *= 0.5f;
            }
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.type == ItemID.AntlionClaw || item.type == ModContent.ItemType<DesertLongsword>())
            {
                float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem);
                Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, (float)player.direction * player.gravDir, player.itemAnimationMax * 1f, adjustedItemScale5 * 0.8f);
                NetMessage.SendData(13, -1, -1, null, player.whoAmI);
                return false;
            }
            return base.Shoot(item,player,source,position,velocity,type, damage, knockback);
        }
    }
}
