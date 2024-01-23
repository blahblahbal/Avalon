using Avalon.Projectiles.Magic;
using Avalon.Projectiles.Melee;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

class BloodBarrage : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Magic;
        Item.damage = 16;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 12f;
        Item.mana = 8;
        Item.rare = ItemRarityID.LightRed;
        Item.Size = new Vector2(32);
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.BloodBlob>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 50000;
        Item.useAnimation = 24;
        Item.reuseDelay = 20;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BloodBlob>(), (int)(damage * 1.25f), knockback, player.whoAmI);
        }
        else
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BloodBlob>(), damage, knockback, player.whoAmI);
        }
        return false;
    }
    public SoundStyle note = new SoundStyle("Terraria/Sounds/NPC_Hit_18")
    {
        Volume = 0.5f,
        Pitch = -0.5f,
        PitchVariance = 0.5f,
        MaxInstances = 10,
    };
    public override bool? UseItem(Player player)
    {
        SoundEngine.PlaySound(note, player.Center);
        if (!player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
        {
            Item.useTime = 12;
        }
        else
        {
            Item.useTime = 6;
        }
        return true;
    }
    public override bool AltFunctionUse(Player player)
    {
        int healthSucked = 80;
        if (/* player.statLife > 80 && */!player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
        {
            player.AddBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>(), 60 * 8);
            SoundEngine.PlaySound(SoundID.NPCDeath1, Main.LocalPlayer.position);

            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, healthSucked, dramatic: false, dot: false);
            player.statLife -= healthSucked;
            if (player.statLife <= 0)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} forgot their blood is necessary."), healthSucked, 1, false, true, -1, false);
            }
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDustPerfect(player.MountedCenter, DustID.Blood, (player.velocity * 0.85f) + Main.rand.NextVector2Circular(2f, 1f).RotatedBy(i) + Main.rand.NextVector2Square(-3.5f, 3.5f), 100, default(Color), 0.7f + Main.rand.NextFloat() * 0.6f);
                Dust.NewDustPerfect(player.MountedCenter, DustID.Blood, (player.velocity * 0.85f) + Main.rand.NextVector2Circular(0.5f, 0.5f).RotatedBy(i) + Main.rand.NextVector2Square(-1.5f, 1.5f), 100, default(Color), 1f + Main.rand.NextFloat() * 0.6f);
            }
        }
        return !player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>());
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (!player.HasBuff(ModContent.BuffType<Buffs.Debuffs.SanguineSacrifice>()))
        {
            velocity = velocity.RotatedByRandom(0.11);
        }
        else
        {
            velocity = velocity.RotatedByRandom(0.17);
        }
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(10, 0);
    }
}
