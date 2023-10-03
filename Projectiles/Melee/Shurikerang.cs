using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Melee;

public class Shurikerang : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Ranged;
        AIType = ProjectileID.EnchantedBoomerang;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        int num34 = 10;
        int num35 = 10;
        Vector2 vector7 = new Vector2(Projectile.position.X + Projectile.width / 2 - num34 / 2, Projectile.position.Y + Projectile.height / 2 - num35 / 2);
        Projectile.velocity = Collision.TileCollision(vector7, Projectile.velocity, num34, num35, true, true, 1);
        Projectile.ai[0] = 1f;
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        return false;
    }

    public override void AI()
    {
        if (Projectile.soundDelay == 0)
        {
            Projectile.soundDelay = 8;
            SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
        }
        if (Projectile.ai[0] == 0f)
        {
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] >= 30f)
            {
                Projectile.ai[0] = 1f;
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
        }
        else
        {
            Projectile.tileCollide = false;
            var num100 = 9f;
            var num101 = 0.4f;
            if (Projectile.type == ModContent.ProjectileType<Shurikerang>())
            {
                num100 = 15f;
                num101 = 0.8f;
            }
            var vector4 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            var num102 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector4.X;
            var num103 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector4.Y;
            var num104 = (float)Math.Sqrt(num102 * num102 + num103 * num103);
            if (num104 > 3000f)
            {
                Projectile.Kill();
            }
            num104 = num100 / num104;
            num102 *= num104;
            num103 *= num104;
            if (Projectile.velocity.X < num102)
            {
                Projectile.velocity.X = Projectile.velocity.X + num101;
                if (Projectile.velocity.X < 0f && num102 > 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X + num101;
                }
            }
            else if (Projectile.velocity.X > num102)
            {
                Projectile.velocity.X = Projectile.velocity.X - num101;
                if (Projectile.velocity.X > 0f && num102 < 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num101;
                }
            }
            if (Projectile.velocity.Y < num103)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + num101;
                if (Projectile.velocity.Y < 0f && num103 > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num101;
                }
            }
            else if (Projectile.velocity.Y > num103)
            {
                Projectile.velocity.Y = Projectile.velocity.Y - num101;
                if (Projectile.velocity.Y > 0f && num103 < 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num101;
                }
            }
            if (Main.myPlayer == Projectile.owner)
            {
                var rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                var value4 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
                if (rectangle.Intersects(value4))
                {
                    Projectile.Kill();
                }
            }
        }
        Projectile.rotation += 0.4f * Projectile.direction;
    }
}
