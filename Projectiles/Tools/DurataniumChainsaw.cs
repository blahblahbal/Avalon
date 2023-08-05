using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Tools;

public class DurataniumChainsaw : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 18 / 56;
        Projectile.height = dims.Height * 18 / 56 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.scale = 1.16f;
    }

    public override void AI()
    {
        if (Projectile.soundDelay <= 0)
        {
            SoundEngine.PlaySound(SoundID.Item22, Projectile.position);
            Projectile.soundDelay = 30;
        }
        if (Main.myPlayer == Projectile.owner)
        {
            if (Main.player[Projectile.owner].channel)
            {
                var num316 = Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].shootSpeed * Projectile.scale;
                var vector22 = new Vector2(Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width * 0.5f, Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height * 0.5f);
                var num317 = Main.mouseX + Main.screenPosition.X - vector22.X;
                var num318 = Main.mouseY + Main.screenPosition.Y - vector22.Y;
                if (Main.player[Projectile.owner].gravDir == -1f)
                {
                    num318 = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector22.Y;
                }
                var num319 = (float)Math.Sqrt(num317 * num317 + num318 * num318);
                num319 = (float)Math.Sqrt(num317 * num317 + num318 * num318);
                num319 = num316 / num319;
                num317 *= num319;
                num318 *= num319;
                if (num317 != Projectile.velocity.X || num318 != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.velocity.X = num317;
                Projectile.velocity.Y = num318;
            }
            else
            {
                Projectile.Kill();
            }
        }
        if (Projectile.velocity.X > 0f)
        {
            Main.player[Projectile.owner].ChangeDir(1);
        }
        else if (Projectile.velocity.X < 0f)
        {
            Main.player[Projectile.owner].ChangeDir(-1);
        }
        Projectile.spriteDirection = Projectile.direction;
        Main.player[Projectile.owner].ChangeDir(Projectile.direction);
        Main.player[Projectile.owner].heldProj = Projectile.whoAmI;
        Main.player[Projectile.owner].itemTime = 2;
        Main.player[Projectile.owner].itemAnimation = 2;
        Projectile.position.X = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - Projectile.width / 2;
        Projectile.position.Y = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - Projectile.height / 2;
        Projectile.rotation = (float)(Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.5700000524520874);
        if (Main.player[Projectile.owner].direction == 1)
        {
            Main.player[Projectile.owner].itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
        }
        else
        {
            Main.player[Projectile.owner].itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
        }
        Projectile.velocity.X = Projectile.velocity.X * (1f + Main.rand.Next(-3, 4) * 0.01f);
        if (Main.rand.Next(6) == 0)
        {
            var num320 = Dust.NewDust(Projectile.position + (Projectile.velocity * Main.rand.Next(6, 10)) * 0.1f, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 80, default(Color), 1.4f);
            var dust52 = Main.dust[num320];
            dust52.position.X = dust52.position.X - 4f;
            Main.dust[num320].noGravity = true;
            Main.dust[num320].velocity *= 0.2f;
            Main.dust[num320].velocity.Y = -Main.rand.Next(7, 13) * 0.15f;
        }
    }
}
