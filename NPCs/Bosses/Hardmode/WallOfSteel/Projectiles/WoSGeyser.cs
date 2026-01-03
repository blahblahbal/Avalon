using Avalon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode.WallOfSteel.Projectiles;

public class WoSGeyser : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames;
    public override void SetDefaults()
    {
        Projectile.width = 0;
        Projectile.height = 8;
        Projectile.hostile = true;
        Projectile.friendly = false;
        Projectile.penetrate = -1;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = true;
        Projectile.timeLeft = 50;
        Projectile.alpha = 255;
        Projectile.extraUpdates = 2;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 10;
    }

    public override void AI()
    {
        if (Projectile.ai[0] > 1f)
        {
            float num418 = 1f;
            if (Projectile.ai[0] == 8f)
            {
                num418 = 0.25f;
            }
            else if (Projectile.ai[0] == 9f)
            {
                num418 = 0.5f;
            }
            else if (Projectile.ai[0] == 10f)
            {
                num418 = 0.75f;
            }

            Projectile.ai[0] += 1f;
            int num419 = 6;
            if (num419 == 6 || Main.rand.Next(3) == 0)
            {
                for (int num420 = 0; num420 < 1; num420++)
                {
                    var num150 = Dust.NewDust(
                        new Vector2(Projectile.position.X + Projectile.velocity.X,
                            Projectile.position.Y + Projectile.velocity.Y), Projectile.width, Projectile.height,
                        DustID.Smoke, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f);
                    Main.dust[num150].noGravity = true;
                    var num151 = Dust.NewDust(
                        new Vector2(Projectile.position.X + Projectile.velocity.X,
                            Projectile.position.Y + Projectile.velocity.Y), Projectile.width, Projectile.height,
                        DustID.FlameBurst, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f);
                    Main.dust[num151].noGravity = true;
                    var randomDust = 0;
                    Dust dust98;
                    Dust dust189;

                    if (Main.rand.NextBool(2))
                        randomDust = num150;
                    else
                        randomDust = num151;

                    if (Main.rand.Next(3) != 0 || (num419 == 75 && Main.rand.Next(3) == 0))
                    {
                        Main.dust[randomDust].noGravity = true;
                        dust98 = Main.dust[randomDust];
                        dust189 = dust98;
                        dust189.scale *= 3f;
                        Main.dust[randomDust].velocity.X *= 2f;
                        Main.dust[randomDust].velocity.Y *= 2f;
                    }

                    dust98 = Main.dust[randomDust];
                    dust189 = dust98;
                    dust189.scale *= 1.5f;
                    Main.dust[randomDust].velocity.X *= 1.2f;
                    Main.dust[randomDust].velocity.Y *= 1.2f;
                    dust98 = Main.dust[randomDust];
                    dust189 = dust98;
                    dust189.scale *= num418;
                }
            }
        }
        else
        {
            Projectile.ai[0] += 1f;
        }

        Projectile.rotation += 0.3f * (float)Projectile.direction;
    }

    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        int size = 20;
        hitbox.X -= size;
        hitbox.Y -= size;
        hitbox.Width += size * 2;
        hitbox.Height += size * 2;
    }
}
