using System;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Pets;

public class BacteriumPet : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 6;
        Main.projPet[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = 36;
        Projectile.height = 40;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.timeLeft *= 5;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        return false;
    }
    public override void AI()
    {
        /*Player player = Main.player[Projectile.owner];

        // If the player is no longer active (online) - deactivate (remove) the projectile.
        if (!player.active)
        {
            Projectile.active = false;
            return;
        }

        // Keep the projectile disappearing as long as the player isn't dead and has the pet buff.
        if (!player.dead && player.HasBuff(ModContent.BuffType<Buffs.Pets.Bacterium>()))
        {
            Projectile.timeLeft = 2;
        }

        Vector2 targetPos = player.Center + new Vector2(0, 40) + new Vector2(player.velocity.X, player.velocity.Y);

        //Projectile.ai[0] = MathHelper.Clamp(Projectile.ai[0], -40 , 40 * 3);
        Projectile.velocity = Vector2.SmoothStep(Projectile.velocity += Projectile.Center.DirectionTo(targetPos) * (Projectile.Center.Distance(targetPos) * 0.01f), Projectile.Center.DirectionTo(targetPos) * 3, 0.1f);
        if(Projectile.Center.Distance(targetPos) < 10)
        {
            Projectile.velocity *= 0.8f;
        }
        if (Projectile.Center.Distance(targetPos) < 3 && Projectile.velocity.Length() < 1)
        {
            Projectile.velocity *= 0f;
        }
        float MaxSpeed = MathHelper.Clamp(Projectile.Center.Distance(targetPos) * 0.05f, 6, 12);
        Projectile.velocity = Vector2.Clamp(Projectile.velocity, new Vector2(-MaxSpeed), new Vector2(MaxSpeed));
        
        if(Projectile.Center.Distance(targetPos) < 100)
        {
            Projectile.velocity *= 0.95f;
        }
        
        if (Projectile.Center.Distance(player.Center) > 1000)
            Projectile.Center = player.Center;

        Projectile.rotation = Projectile.velocity.X * 0.06f;

        // This part is for the pulsing effect
        if (Projectile.ai[2] == 0)
            Projectile.ai[2] = 0.05f;

        if (Projectile.ai[1] > 1 || Projectile.ai[1] < 0)
            Projectile.ai[2] *= -1;

        Projectile.ai[1] += Projectile.ai[2];

        Projectile.scale = MathHelper.SmoothStep(0.95f, 1.05f, Projectile.ai[1]);

        if (Main.rand.NextBool(10))
        {
            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, 0, 0, 128);
            d.noGravity = true;
            d.velocity *= 0.3f;
            d.velocity += Projectile.velocity;
            d.fadeIn = 1.2f;
        }*/

        float num190 = 0.1f;
        Projectile.tileCollide = false;
        int num2 = 300;
        Vector2 vector = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
        float num13 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector.X;
        float num24 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector.Y;
        float num35 = (float)Math.Sqrt(num13 * num13 + num24 * num24);
        float num46 = 7f;
        float num57 = 2000f;
        bool num202 = num35 > num57;
        if (num35 < num2 && Main.player[Projectile.owner].velocity.Y == 0f && Projectile.position.Y + Projectile.height <= Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
        {
            Projectile.ai[1] = 0f;
            if (Projectile.velocity.Y < -6f)
            {
                Projectile.velocity.Y = -6f;
            }
        }
        if (num35 < 150f)
        {
            if (Math.Abs(Projectile.velocity.X) > 2f || Math.Abs(Projectile.velocity.Y) > 2f)
            {
                Projectile.velocity *= 0.99f;
            }
            num190 = 0.01f;
            if (num13 < -2f)
            {
                num13 = -2f;
            }
            if (num13 > 2f)
            {
                num13 = 2f;
            }
            if (num24 < -2f)
            {
                num24 = -2f;
            }
            if (num24 > 2f)
            {
                num24 = 2f;
            }
        }
        else
        {
            if (num35 > 300f)
            {
                num190 = 0.2f;
            }
            num35 = num46 / num35;
            num13 *= num35;
            num24 *= num35;
        }
        if (num202)
        {
            int num78 = 17;
            for (int i = 0; i < 12; i++)
            {
                float speedX = 1f - Main.rand.NextFloat() * 2f;
                float speedY = 1f - Main.rand.NextFloat() * 2f;
                int num89 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, num78, speedX, speedY);
                Main.dust[num89].noLightEmittence = true;
                Main.dust[num89].noGravity = true;
            }
            Projectile.Center = Main.player[Projectile.owner].Center;
            Projectile.velocity = Vector2.Zero;
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.netUpdate = true;
            }
        }
        if (Math.Abs(num13) > Math.Abs(num24) || num190 == 0.05f)
        {
            if (Projectile.velocity.X < num13)
            {
                Projectile.velocity.X += num190;
                if (num190 > 0.05f && Projectile.velocity.X < 0f)
                {
                    Projectile.velocity.X += num190;
                }
            }
            if (Projectile.velocity.X > num13)
            {
                Projectile.velocity.X -= num190;
                if (num190 > 0.05f && Projectile.velocity.X > 0f)
                {
                    Projectile.velocity.X -= num190;
                }
            }
        }
        if (Math.Abs(num13) <= Math.Abs(num24) || num190 == 0.05f)
        {
            if (Projectile.velocity.Y < num24)
            {
                Projectile.velocity.Y += num190;
                if (num190 > 0.05f && Projectile.velocity.Y < 0f)
                {
                    Projectile.velocity.Y += num190;
                }
            }
            if (Projectile.velocity.Y > num24)
            {
                Projectile.velocity.Y -= num190;
                if (num190 > 0.05f && Projectile.velocity.Y > 0f)
                {
                    Projectile.velocity.Y -= num190;
                }
            }
        }


        Projectile.ai[0]++;
        if (Projectile.ai[0] >= 100 && Projectile.ai[0] <= 150)
        {
            if (++Projectile.frameCounter >= 5)
            {
                if (++Projectile.frame > 5)
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
            if (Projectile.ai[0] == 150)
            {
                Projectile.ai[0] = 0;
            }
        }
        else Projectile.frame = 0;
    }
}
