using System;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode; 

public class TrueAeonsEternity : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 128);
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.IronBroadsword);
        Item.Size = new Vector2(22);
        Item.SetWeaponValues(64, 5, 0);
        Item.useTime = 81;
        Item.useAnimation = 20;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.autoReuse = true;
        Item.useTurn = false;
        Item.rare = ItemRarityID.Yellow;
        Item.DamageType = DamageClass.Melee;
        Item.shootSpeed = 8f;
        Item.shoot = ModContent.ProjectileType<AeonBeam>();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int lastStar = -255;
        SoundEngine.PlaySound(SoundID.Item9, player.Center);
        for (int i = 0; i < 6; i++)
        {
            int P = Projectile.NewProjectile(Item.GetSource_FromThis(), position, velocity.RotatedByRandom(Math.PI / 4) * Main.rand.NextFloat(0.6f, 2.8f), ModContent.ProjectileType<TrueAeonStar>(), damage / 4, knockback, player.whoAmI, lastStar, 160 + (i * 10), (float)Main.time);
            Main.projectile[P].scale = Main.rand.NextFloat(0.9f, 1.1f);
            Main.projectile[P].rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
            lastStar = P;
        }
        for (int i = 0; i < 2; i++)
        {
            ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
            particleOrchestraSettings.PositionInWorld = player.Center;
            particleOrchestraSettings.MovementVector = velocity.RotatedByRandom(0.2f) * 2;
            ParticleOrchestraSettings settings = particleOrchestraSettings;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.StardustPunch, settings, player.whoAmI);
            //particleOrchestraSettings.PositionInWorld = player.Center;
            //particleOrchestraSettings.MovementVector = velocity.RotatedByRandom(0.2f) * 2;
            //settings = particleOrchestraSettings;
            //ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PaladinsHammer, settings, player.whoAmI);
            particleOrchestraSettings.PositionInWorld = player.Center;
            particleOrchestraSettings.MovementVector = velocity.RotatedByRandom(0.2f) * 2;
            settings = particleOrchestraSettings;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PrincessWeapon, settings, player.whoAmI);
        }
        return false;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (player.itemTime % 4 == 0)
        {
            int type = Main.rand.Next(2);
            ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
            Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
            ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
            particleOrchestraSettings.PositionInWorld = location2;
            particleOrchestraSettings.MovementVector = Vector2.Zero;
            ParticleOrchestraSettings settings = particleOrchestraSettings;
            if (type == 0)
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.StardustPunch, settings, player.whoAmI);
            else
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PrincessWeapon, settings, player.whoAmI);
        }
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<AeonsEternity>())
            .AddIngredient(ItemID.Ectoplasm, 10)
            .AddIngredient(ItemID.MeteoriteBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
