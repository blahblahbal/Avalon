using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.DrawMagics;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.Audio;
using Avalon.Particles;

namespace Avalon.Projectiles.Magic;

public class CursedFlamelash : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        Main.projFrames[Type] = 6;
    }
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.MagicMissile);
        //Projectile.extraUpdates = 1;
        Projectile.penetrate = 3;
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (Main.rand.NextBool(3))
            target.AddBuff(BuffID.CursedInferno, 160);
    }
    public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
        if (Main.rand.NextBool(3))
            target.AddBuff(BuffID.CursedInferno, 160);
    }
    public override void AI()
    {
        Projectile.scale = MathHelper.Lerp(1.5f, 1.2f, MathHelper.Clamp(Projectile.velocity.Length() * 0.1f, 0, 1));

        if (Main.rand.NextBool(5) && Projectile.position.Distance(Projectile.oldPos[1]) > 0.2f)
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.CursedTorch, Projectile.velocity.RotateRandom(0.4f) * Main.rand.NextFloat(0.1f,0.2f));
            d.noGravity = true;
            d.scale = (Main.rand.NextFloat(1, 2));
            d.fadeIn = (Main.rand.NextFloat(1.5f, 2.5f));
        }

        if (Projectile.position.Distance(Projectile.oldPos[1]) < 0.2f && Main.rand.NextBool(2))
        {
            Dust d2 = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(Projectile.Hitbox), DustID.CursedTorch, Main.rand.NextVector2Circular(1,2));
            d2.noGravity = Main.rand.NextBool(6);
            d2.scale = (Main.rand.NextFloat(2, 3));
            //d2.noLight = true;
            if (!d2.noGravity)
            {
                d2.scale *= 0.2f;
            }
        }

        Projectile.frameCounter++;
        if (Projectile.frameCounter >= 3)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if(Projectile.frame > 5)
        {
            Projectile.frame = 0;
        }
    }

    public override void Kill(int timeLeft)
    {
        ParticleSystem.AddParticle(new CursedExplosionParticle(), Projectile.Center, Vector2.Zero, default, Main.rand.NextFloat(MathHelper.TwoPi), Main.rand.NextFloat(0.9f, 1.2f));
        SoundEngine.PlaySound(SoundID.Item14,Projectile.Center);
        float decreaseBy = 0.05f;
        for(int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type] / 2; i++)
        {
            if (Projectile.oldPos[i].DirectionFrom(Projectile.oldPos[i + 1]).Length() > 0.6f)
            {
                for (int i2 = 0; i2 < Main.rand.Next(2,4); i2++)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.oldPos[i], DustID.CursedTorch, Projectile.oldPos[i].DirectionFrom(Projectile.oldPos[i + 1]).RotateRandom(0.4f) * Main.rand.NextFloat(7,9));
                    d.noGravity = !Main.rand.NextBool(3);
                    d.scale = (Main.rand.NextFloat(0.25f, 0.5f) * i2) - (decreaseBy * i);
                    d.fadeIn = (Main.rand.NextFloat(0.75f, 1f) * i2) - (decreaseBy * i * 2);
                    //d.noLight = true;
                    if (!d.noGravity)
                    {
                        d.scale *= 0.5f;
                    }
                }
            }
        }

        for (int i = 0; i < 30; i++)
        {
            Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.CursedTorch, 0, 0, 0, default, 3);
            D.color = new Color(255, 255, 255, 0);
            D.noGravity = !Main.rand.NextBool(3);
            D.fadeIn = Main.rand.NextFloat(0f, 2f);
            D.velocity = Main.rand.NextVector2Circular(4,6).RotatedBy(Projectile.rotation);
        }
        if (Main.myPlayer == Projectile.owner)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CursedFlamelashExplosion>(), Projectile.damage * 2, Projectile.knockBack * 2, Projectile.owner);
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        default(CursedFlameLashDrawer).Draw(Projectile);

        //Thanks ballsfah
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        int frameHeight = texture.Height / Main.projFrames[Type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        float Rot = MathHelper.Lerp(0,Projectile.velocity.ToRotation() - MathHelper.PiOver2, MathHelper.Clamp(Projectile.velocity.Length() * 0.1f,0,1));

        //Main.EntitySpriteDraw(texture, drawPos, frame, Color.White, Rot, new Vector2(texture.Width, frameHeight) / 2, new Vector2(Projectile.scale,MathHelper.Clamp(Projectile.velocity.Length() * 0.2f,Projectile.scale,Projectile.scale * 2f)), SpriteEffects.None, 0);
        //The line above stretches the flame with speed
        Main.EntitySpriteDraw(texture, drawPos, frame, new Color(230,255,0,0), Rot, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);

        return false;
    }
}
public class CursedFlamelashExplosion : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(128);
        Projectile.aiStyle = -1;
        Projectile.alpha = 0;
        Projectile.penetrate = -1;
        Projectile.scale = 1f;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 21;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 20;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        return false;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if(Main.rand.NextBool(3))
        target.AddBuff(BuffID.CursedInferno, 160);
        modifiers.HitDirectionOverride = (target.Center.X <= Projectile.Center.X) ? -1 : 1;
    }
    public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
        if (Main.rand.NextBool(3))
            target.AddBuff(BuffID.CursedInferno, 160);
        modifiers.HitDirectionOverride = (target.Center.X <= Projectile.Center.X) ? -1 : 1;
    }
}

