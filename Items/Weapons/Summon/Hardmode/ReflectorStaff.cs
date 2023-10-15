using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode;

public class ReflectorStaff : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = 1;
        Item.shootSpeed = 14f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Summon.Reflector>();
        Item.damage = 0;
        Item.width = 38;
        Item.height = 36;
        Item.UseSound = SoundID.Item44;
        Item.buffType = ModContent.BuffType<Buffs.Minions.Reflector>();
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.noMelee = true;
        Item.value = Item.sellPrice(0, 30, 0, 0);
        Item.knockBack = 8.5f;
        Item.rare = ItemRarityID.Cyan;
        Item.DamageType = DamageClass.Summon;
        Item.mana = 30;
    }
    public override bool CanUseItem(Player player)
    {
        if (player.maxMinions > 6)
            return player.ownedProjectileCounts[Item.shoot] < 6;
        return player.ownedProjectileCounts[Item.shoot] < player.maxMinions;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        player.AddBuff(Item.buffType, 2);
        player.SpawnMinionOnCursor(source, player.whoAmI, type, damage, knockback);
        return false;
    }
}
