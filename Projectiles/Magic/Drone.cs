using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.IO;

namespace Avalon.Projectiles.Magic;

public class Drone : ModProjectile
{
    private int tileCollideCounter;
    public bool readyToHome = true;
    public float maxSpeed = 10f + Main.rand.NextFloat(5f);
    public float homeDistance = 400;
    public float homeStrength = 5f;
    public float homeDelay;
    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = -1;
        Projectile.alpha = 0;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 30;
        Projectile.timeLeft = 120;
        Projectile.ArmorPenetration = 15;
        DrawOriginOffsetY = -2;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        if(Projectile.timeLeft < 118)
        {
            return new Color(255, 255, 255, 200);
        }
        return new Color(0, 0, 0, 0);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        readyToHome = false;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(tileCollideCounter);
        writer.Write(readyToHome);
        writer.Write(homeDelay);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        tileCollideCounter = reader.ReadInt32();
        readyToHome = reader.ReadBoolean();
        homeDelay = reader.ReadSingle();
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.type == ModContent.ProjectileType<Drone>())
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            tileCollideCounter++;
            if (tileCollideCounter >= 4f)
            {
                Projectile.position += Projectile.velocity;
                Projectile.Kill();
            }
            else
            {
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
            }
        }
        return false;
    }
    
    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 219 / 510f, 205 / 510f, 79 / 510f);
        if(Projectile.timeLeft < 118)
        {
            Dust dust = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<Dusts.DroneDust>(), Vector2.Zero, default, default, 1.2f);
            dust.noGravity = true;
        }
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        if (!readyToHome)
        {
            homeDelay++;
            if(homeDelay >= 15)
            {
                readyToHome = true;
                homeDelay = 0;
            }
            Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.Pi / 10);
        }

        Vector2 startPosition = Projectile.Center;
        int closest = Projectile.FindClosestNPC(homeDistance, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
        if (closest != -1 && readyToHome)
        {
            if (Collision.CanHit(Main.npc[closest], Projectile))
            {
                Vector2 target = Main.npc[closest].Center;
                float distance = Vector2.Distance(target, startPosition);
                Vector2 goTowards = Vector2.Normalize(target - startPosition) * ((homeDistance - distance) / (homeDistance / homeStrength));

                Projectile.velocity += goTowards;

                if (Projectile.velocity.Length() > maxSpeed)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxSpeed;
                }
            }
        }
    }
    public override void Kill(int timeLeft)
    {
        for (int i = 0; i < 6; i++)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.DroneDust>(), 0f, 0f, default, default, 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1f;
        }
    }
}
