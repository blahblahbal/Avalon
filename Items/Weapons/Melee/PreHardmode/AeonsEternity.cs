using System;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode; 

public class AeonsEternity : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 128);
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.IronBroadsword);
        Item.Size = new Vector2(22);
        Item.SetWeaponValues(40, 5, 0);
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.autoReuse = true;
        Item.useTurn = false;
        Item.rare = ItemRarityID.Pink;
        Item.DamageType = DamageClass.Melee;
        Item.shootSpeed = 8f;
        Item.shoot = ModContent.ProjectileType<AeonBeam>();
    }
    int TimesSwung = 0;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        TimesSwung++;
        if (TimesSwung == 6)
        {
            int lastStar = -255;
            SoundEngine.PlaySound(SoundID.Item9, player.Center);
            for (int i = 0; i < Main.rand.Next(4, 8); i++)
            {
                int P = Projectile.NewProjectile(Item.GetSource_FromThis(), position, velocity.RotatedByRandom(Math.PI / 6) * Main.rand.NextFloat(1.6f, 2.4f), ModContent.ProjectileType<AeonStar>(), damage / 4, knockback, player.whoAmI, lastStar, 160 + (i * 10), (float)Main.time);
                Main.projectile[P].scale = Main.rand.NextFloat(0.9f, 1.1f);
                Main.projectile[P].rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
                lastStar = P;
            }
            TimesSwung = 0;
        }
        if (TimesSwung is 2 or 4 or 0)
        {
            return true;
        }
        return false;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(2))
        {
            int num208 = Main.rand.Next(3);
            if (num208 == 0)
            {
                num208 = 15;
            }
            else if (num208 == 1)
            {
                num208 = 57;
            }
            else if (num208 == 2)
            {
                num208 = 58;
            }

            for (int j = 0; j < 2; j++)
            {
                ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
                Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
                Dust.NewDustPerfect(location2, num208, vector2 * 2f, 100, default(Color), 0.7f + Main.rand.NextFloat() * 0.6f);
                if (Main.rand.NextBool(20))
                {
                    int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, num208, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 140, default(Color), 0.7f);
                    Main.dust[num15].position = location2;
                    Main.dust[num15].fadeIn = 1.2f;
                    Main.dust[num15].noGravity = true;
                    Main.dust[num15].velocity *= 0.25f;
                    Main.dust[num15].velocity += vector2 * 5f;
                }
            }
        }
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.Starfury)
            .AddIngredient(ItemID.IceBlade)
            .AddIngredient(ModContent.ItemType<MinersSword>())
            .AddIngredient(ItemID.AntlionClaw)
            .AddIngredient(ModContent.ItemType<OsmiumGreatsword>())
            .AddTile(TileID.DemonAltar)
            .Register();
        CreateRecipe(1)
            .AddIngredient(ItemID.Starfury)
            .AddIngredient(ItemID.IceBlade)
            .AddIngredient(ModContent.ItemType<MinersSword>())
            .AddIngredient(ModContent.ItemType<DesertLongsword>())
            .AddIngredient(ModContent.ItemType<OsmiumGreatsword>())
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Starfury)
            .AddIngredient(ItemID.IceBlade)
            .AddIngredient(ModContent.ItemType<MinersSword>())
            .AddIngredient(ItemID.AntlionClaw)
            .AddIngredient(ModContent.ItemType<RhodiumGreatsword>())
            .AddTile(TileID.DemonAltar)
            .Register();
        CreateRecipe(1)
            .AddIngredient(ItemID.Starfury)
            .AddIngredient(ItemID.IceBlade)
            .AddIngredient(ModContent.ItemType<MinersSword>())
            .AddIngredient(ModContent.ItemType<DesertLongsword>())
            .AddIngredient(ModContent.ItemType<RhodiumGreatsword>())
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Starfury)
            .AddIngredient(ItemID.IceBlade)
            .AddIngredient(ModContent.ItemType<MinersSword>())
            .AddIngredient(ItemID.AntlionClaw)
            .AddIngredient(ModContent.ItemType<IridiumGreatsword>())
            .AddTile(TileID.DemonAltar)
            .Register();
        CreateRecipe(1)
            .AddIngredient(ItemID.Starfury)
            .AddIngredient(ItemID.IceBlade)
            .AddIngredient(ModContent.ItemType<MinersSword>())
            .AddIngredient(ModContent.ItemType<DesertLongsword>())
            .AddIngredient(ModContent.ItemType<IridiumGreatsword>())
            .AddTile(TileID.DemonAltar)
            .Register();
    }
}