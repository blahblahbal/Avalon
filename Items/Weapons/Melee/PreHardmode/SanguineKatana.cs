using System;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode; 

public class SanguineKatana : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 36;
        Item.damage = 24;
        Item.scale = 1f;
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Orange;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 5f;
        Item.UseSound = SoundID.Item1;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.shoot = ModContent.ProjectileType<SanguineKatanaSlash>();
        Item.noMelee= true;
        Item.shootSpeed = 16;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float adjustedItemScale5 = player.GetAdjustedItemScale(player.HeldItem);
        Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, (float)player.direction * player.gravDir, player.itemAnimationMax * 1f, adjustedItemScale5);
        NetMessage.SendData(13, -1, -1, null, player.whoAmI);
        return false;
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        if(target.type != NPCID.TargetDummy)
        {
            int healAmount = Main.rand.Next(4) + 2;
            player.HealEffect(healAmount, true);
            player.statLife += healAmount;
        }
        for(int i = 0; i < 15; i++)
        {
            int num15 = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox),0,0, DustID.Blood, 0,0, 140, default(Color), 2f);
            Main.dust[num15].fadeIn = 1.2f;
            Main.dust[num15].noGravity = true;
            Main.dust[num15].velocity = Main.rand.NextVector2Circular(6, 6);
        }
    }
    public override bool? UseItem(Player player)
    {
        int healthSucked = 2;
        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, healthSucked, dramatic: false, dot: false);
        player.statLife -= healthSucked;
        if(player.statLife <= 0)
        {
            player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name}'s soul has been entombed within a sword."),healthSucked,1,false,true,false,-1,false);
        }
        return true;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        //if (Main.rand.NextBool(3))
        //{
        //    //int num209 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
        //    //Dust dust = Main.dust[num209];
        //    //dust.velocity.X = 2f * player.direction;
        //    //dust.velocity.Y = -0.5f;
        //}
        ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
        Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
        Dust.NewDustPerfect(location2, DustID.Blood, vector2 * 2f, 100, default(Color), 0.7f + Main.rand.NextFloat() * 0.6f);
    }
}