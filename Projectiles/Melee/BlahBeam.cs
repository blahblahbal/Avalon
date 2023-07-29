using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.IO;

namespace Avalon.Projectiles.Melee;

public class BlahBeam : ModProjectile
{
    private int tileCollideCounter;
    public bool readyToHome = true;
    public float maxSpeed = 10f + Main.rand.NextFloat(10f);
    public float homeDistance = 600;
    public float homeStrength = 5f;
    public float homeDelay;
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = 27;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 5;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        if (Projectile.localAI[1] >= 15f)
        {
            return new Color(255, 255, 255, Projectile.alpha);
        }
        if (Projectile.localAI[1] < 5f)
        {
            return Color.Transparent;
        }
        int num7 = (int)((Projectile.localAI[1] - 5f) / 10f * 255f);
        return new Color(num7, num7, num7, num7);
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
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        int randomNum = Main.rand.Next(7);
        if (randomNum == 0) target.AddBuff(20, 300);
        else if (randomNum == 1) target.AddBuff(24, 200);
        else if (randomNum == 2) target.AddBuff(31, 120);
        else if (randomNum == 3) target.AddBuff(39, 300);
        else if (randomNum == 4) target.AddBuff(44, 300);
        else if (randomNum == 5) target.AddBuff(70, 240);
        else if (randomNum == 6) target.AddBuff(69, 300);
        readyToHome = false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.type == ModContent.ProjectileType<BlahBeam>())
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
        Lighting.AddLight(Projectile.position, 255 / 255f, 175 / 255f, 0);

        if (!readyToHome)
        {
            homeDelay++;
            if(homeDelay >= 20)
            {
                readyToHome = true;
                homeDelay = 0;
            }
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
}
