using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Avalon.Particles;
using Terraria.Graphics.Shaders;
using Terraria.Graphics;
using ReLogic.Content;

namespace Avalon.Projectiles.Magic;

public class CursedFlamelash : ModProjectile
{
	private static Asset<Texture2D> texture;
	public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        Main.projFrames[Type] = 6;
        texture = ModContent.Request<Texture2D>(Texture);
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

    public override void OnKill(int timeLeft)
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
        int frameHeight = texture.Value.Height / Main.projFrames[Type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Value.Width, frameHeight);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        float Rot = MathHelper.Lerp(0,Projectile.velocity.ToRotation() - MathHelper.PiOver2, MathHelper.Clamp(Projectile.velocity.Length() * 0.1f,0,1));

        //Main.EntitySpriteDraw(texture, drawPos, frame, Color.White, Rot, new Vector2(texture.Width, frameHeight) / 2, new Vector2(Projectile.scale,MathHelper.Clamp(Projectile.velocity.Length() * 0.2f,Projectile.scale,Projectile.scale * 2f)), SpriteEffects.None, 0);
        //The line above stretches the flame with speed
        Main.EntitySpriteDraw(texture.Value, drawPos, frame, new Color(230,255,0,0), Rot, new Vector2(texture.Value.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);

        return false;
    }
}
public struct CursedFlameLashDrawer
{
    private static VertexStrip _vertexStrip = new VertexStrip();

    private float transitToDark;

    public void Draw(Projectile proj)
    {
        this.transitToDark = Utils.GetLerpValue(0f, 6f, proj.localAI[0], clamped: true);
        MiscShaderData miscShaderData = GameShaders.Misc["FlameLash"];
        miscShaderData.UseSaturation(-2f);
        miscShaderData.UseOpacity(MathHelper.Lerp(4f, 8f, this.transitToDark));
        miscShaderData.Apply();
        CursedFlameLashDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, StripColors, StripWidth, -Main.screenPosition + proj.Size / 2f);
        CursedFlameLashDrawer._vertexStrip.DrawTrail();
        Main.pixelShader.CurrentTechnique.Passes[0].Apply();
    }

    private Color StripColors(float progressOnStrip)
    {
        float lerpValue = Utils.GetLerpValue(0f - 0.1f * this.transitToDark, 0.7f - 0.2f * this.transitToDark, progressOnStrip, clamped: true);
        Color result = Color.Lerp(Color.Lerp(Color.Lime, Color.DarkGreen, this.transitToDark * 0.5f), Color.Green, lerpValue) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip));
        result.A /= 8;
        return result;
    }

    private float StripWidth(float progressOnStrip)
    {
        float lerpValue = Utils.GetLerpValue(0f, 0.06f + this.transitToDark * 0.01f, progressOnStrip, clamped: true);
        lerpValue = 1f - (1f - lerpValue) * (1f - lerpValue);
        return MathHelper.Lerp(24f + this.transitToDark * 16f, 8f, Utils.GetLerpValue(0f, 1f, progressOnStrip, clamped: true)) * lerpValue;
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

