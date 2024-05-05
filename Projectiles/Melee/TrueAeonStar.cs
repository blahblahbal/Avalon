using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class TrueAeonStar : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.aiStyle = -1;
        Projectile.alpha = 0;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
        Projectile.tileCollide = false;
        //DrawOriginOffsetX= -4;
        DrawOriginOffsetY= 2;
        DrawOffsetX = 4;
        Projectile.extraUpdates = 1;
    }
    //public override Color? GetAlpha(Color lightColor)
    //{
    //    return new Color(255, 255, 255, 64);
    //}
    Vector2 LastStarPos;
    public override void OnSpawn(IEntitySource source)
    {
        int J = (Projectile.ai[0] != -255) ? Main.projectile[(int)Projectile.ai[0]].whoAmI : Projectile.whoAmI;
        LastStarPos = Main.projectile[J].Center;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        int frameHeight = texture.Height / Main.projFrames[Projectile.type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
        Vector2 frameOrigin = frame.Size() / 2f;
        Color color = Color.Lerp(new Color(255,255,255,0), new Color(128, 128, 128, 64), Projectile.ai[1] * 0.03f);
        for(int i = 0; i < 6; i++)
        {
            Main.EntitySpriteDraw(texture, Projectile.position + frameOrigin - Main.screenPosition + new Vector2(0, (float)Math.Sin(Main.GlobalTimeWrappedHourly * MathHelper.TwoPi / 6f) * 6).RotatedBy(i * MathHelper.PiOver2 + (Main.timeForVisualEffects * 0.03f)), frame, color * 0.2f, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None);
        }
        Main.EntitySpriteDraw(texture, Projectile.position + frameOrigin - Main.screenPosition, frame, color, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None);
        return false;
    }
    public override void AI()
    {
        //int F = Dust.NewDust(Projectile.Center, 0, 0, DustID.UnusedWhiteBluePurple, 0, 0, 0, default, 1);
        //Main.dust[F].noGravity = true;
        //Main.dust[F].velocity *= 0;

        float Seed = Projectile.ai[2];
        Projectile lastStar = (Projectile.ai[0] != -255) ? Main.projectile[(int)Projectile.ai[0]] : Projectile;
        float distanceToStar = Projectile.Center.Distance(lastStar.Center);
        if (!lastStar.active)
        {
            lastStar = Projectile;
        }
        Projectile.ai[1]--;
        if (Projectile.ai[1] == 100 && lastStar.ai[2] == Seed && lastStar.whoAmI != Projectile.whoAmI == true)
        {
            SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);
        }
        if (lastStar.ai[2] == Seed && lastStar.whoAmI != Projectile.whoAmI && Projectile.ai[1] < 100)
        {
            for (int i = 0; i < distanceToStar; i += 6)
            {
                int D = Dust.NewDust(Projectile.Center + new Vector2(i, 0).RotatedBy(Projectile.Center.AngleTo(lastStar.Center)), 0, 0, DustID.UnusedWhiteBluePurple, 0, 0, 0, default, 1);
                Main.dust[D].noGravity = true;
                Main.dust[D].velocity *= 0;
            }
        }

        Projectile.velocity *= 0.95f;

        float num808 = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if ((double)Math.Abs(Projectile.rotation - num808) >= 3.14)
        {
            if (num808 < Projectile.rotation)
            {
                Projectile.rotation -= 6.28f;
            }
            else
            {
                Projectile.rotation += 6.28f;
            }
        }
        Projectile.rotation = (Projectile.rotation * 4f + num808) / 5f;
        Projectile.rotation = MathHelper.Lerp(Projectile.rotation <= MathHelper.Pi ? 0 : MathHelper.TwoPi, Projectile.velocity.ToRotation() + MathHelper.PiOver2, MathHelper.Clamp( Projectile.velocity.Length(),0,1));


        if (Projectile.ai[1] > 30)
        {
            LastStarPos = lastStar.Center;
        }

        if (Projectile.ai[1] < 10)
        {
            int D = Dust.NewDust(Vector2.Lerp(Projectile.Center, LastStarPos, Projectile.ai[1] / 10), 0, 0, DustID.UnusedWhiteBluePurple, 0, 0, 0, default, 2);
            Main.dust[D].color = new Color(255,255,255,0);
            Main.dust[D].noGravity = true;
            Main.dust[D].velocity *= 0;
            Main.dust[D].noLightEmittence = true;
        }
        if (Projectile.ai[1] < 0)
        {
            Projectile.Kill();
        }
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item40, Projectile.Center);

        //for (int i = 0; i < 30; i++)
        //{
        //    int D = Dust.NewDust(Projectile.Center, 0, 0, DustID.GoldCoin, 0, 0, 0, default, 3);
        //    Main.dust[D].color = new Color(255, 255, 255, 0);
        //    Main.dust[D].noGravity = true;
        //    Main.dust[D].noLightEmittence = true;
        //    Main.dust[D].fadeIn = Main.rand.NextFloat(0.5f, 1.5f);
        //    Main.dust[D].velocity = new Vector2(Main.rand.NextFloat(3, 8), 0).RotatedBy(MathHelper.Pi / 15 * i);
        //}

        if (Main.myPlayer == Projectile.owner)
        {
            int Target = ClassExtensions.FindClosestNPC(Projectile, 1000, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly || !Collision.CanHit(npc, Projectile));
            if(Target != -1)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.Center.DirectionTo(Main.npc[Target].Center) * 10, ModContent.ProjectileType<AeonBeam>(), Projectile.damage * 7, 0, Projectile.owner);
                //for(int i = 0; i < 2; i++) 
                //{
                //    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.Center.DirectionTo(Main.npc[Target].Center).RotatedByRandom(0.4f) * 4, ModContent.ProjectileType<AeonBeam>(), Projectile.damage * 4, 0, Projectile.owner);
                //}
            }
        }
        for (int i = 0; i < 2; i++)
        {
            ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
            particleOrchestraSettings.PositionInWorld = Projectile.Center;
            particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(12, 12);
            ParticleOrchestraSettings settings = particleOrchestraSettings;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);
            particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(12, 12);
            settings = particleOrchestraSettings;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, Projectile.owner);
            particleOrchestraSettings.MovementVector = Vector2.Zero;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.TrueExcalibur, settings, Projectile.owner);
        }
    }
}
