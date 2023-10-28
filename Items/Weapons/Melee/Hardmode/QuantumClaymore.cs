using Avalon.Items.Material.Shards;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;
public class QuantumClaymore : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 42;
        Item.damage = 88;
        Item.autoReuse = true;
        Item.rare = ModContent.RarityType<Rarities.QuantumRarity>();
        Item.knockBack = 10f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 10, 90, 0);
        Item.useTime = 23 * 2;
        Item.useAnimation = 23;
        Item.UseSound = SoundID.Item15;
        Item.scale = 1.1f;
        Item.shootSpeed = 16;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.QuantumBeam>();
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<CorruptShard>(), 10) // Replace with corrupted bar later
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddIngredient(ItemID.Ectoplasm, 20)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        int DustType = DustID.CorruptTorch;
        if (Main.rand.NextBool())
            DustType = DustID.HallowedTorch;

        for (int j = 0; j < 2; j++)
        {
            ClassExtensions.GetPointOnSwungItemPath(60f, 120f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
            Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
            Dust d = Dust.NewDustPerfect(location2, DustType, vector2 * 2f, 100, default(Color), 0.7f + Main.rand.NextFloat() * 1.3f);
            d.noGravity = true;
            d.velocity *= 2f;
            if (Main.rand.NextBool(20))
            {
                int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 140, default(Color), 1.3f);
                Main.dust[num15].position = location2;
                Main.dust[num15].fadeIn = 1.2f;
                Main.dust[num15].noGravity = true;
                Main.dust[num15].velocity *= 2f;
                Main.dust[num15].velocity += vector2 * 5f;
            }
        }
    }
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        player.itemLocation = Vector2.Lerp(player.itemLocation, player.MountedCenter, 0.5f);
    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        Vector2 SwordSpawn = player.position - new Vector2(Main.rand.Next(60, 180) * player.direction, Main.rand.Next(-75, 75));
        //ParticleSystem.AddParticle(new QuantumPortal(), SwordSpawn, default, default);
        Projectile P = Projectile.NewProjectileDirect(player.GetSource_FromThis(), SwordSpawn, SwordSpawn.DirectionTo(target.Center) * (Item.shootSpeed * Main.rand.NextFloat(1.2f, 1.6f)), ModContent.ProjectileType<Projectiles.Melee.QuantumBeam>(), (int)(Item.damage * 0.6f), Item.knockBack * 0.1f, player.whoAmI,0, Main.rand.Next(-20, -10));
        target.AddBuff(BuffID.ShadowFlame, 300);
    }
    //public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    //{
    //    for (int i = 0; i < Main.rand.Next(3, 4); i++)
    //    {
    //        Vector2 SwordSpawnFunnyPlaceRealOnGodTheyComeFromHereQuiteCoolHonestly = player.position - new Vector2(Main.rand.Next(230,280) * player.direction, Main.rand.Next(-75, 75));

    //        Projectile.NewProjectile(player.GetSource_FromThis(), SwordSpawnFunnyPlaceRealOnGodTheyComeFromHereQuiteCoolHonestly, SwordSpawnFunnyPlaceRealOnGodTheyComeFromHereQuiteCoolHonestly.DirectionTo(Main.MouseWorld) * (Item.shootSpeed * Main.rand.NextFloat(1.6f, 3f)), ModContent.ProjectileType<Projectiles.Melee.QuantumBeam2>(), (int)(Item.damage * 0.6f), Item.knockBack * 0.1f, player.whoAmI);
    //    }
    //}
}
