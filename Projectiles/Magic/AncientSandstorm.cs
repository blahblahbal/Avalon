using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class AncientSandstorm : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 6 / 16;
        Projectile.height = dims.Height * 6 / 16 / Main.projFrames[Projectile.type];
        Projectile.scale = 1f;
        Projectile.alpha = 255;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 3600;
        Projectile.friendly = true;
        Projectile.penetrate = 4;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.MaxUpdates = 2;
        Projectile.DamageType = DamageClass.Magic;
    }

    public override void AI()
    {
        var num960 = 1f;
        if (Projectile.ai[0] == 8f)
        {
            num960 = 0.25f;
        }
        else if (Projectile.ai[0] == 9f)
        {
            num960 = 0.5f;
        }
        else if (Projectile.ai[0] == 10f)
        {
            num960 = 0.75f;
        }
        var num961 = 10;
        if (Main.rand.Next(2) == 0)
        {
            for (var num962 = 0; num962 < 1; num962++)
            {
                var num963 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num961, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                var num964 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y - 10f), Projectile.width, Projectile.height, num961, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                var num965 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 10f), Projectile.width, Projectile.height, num961, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                if (Main.rand.Next(3) != 0 || (num961 == 75 && Main.rand.Next(2) == 0))
                {
                    Main.dust[num963].noGravity = true;
                    Main.dust[num963].scale *= 3f;
                    var dust109 = Main.dust[num963];
                    dust109.velocity.X = dust109.velocity.X * 2f;
                    var dust110 = Main.dust[num963];
                    dust110.velocity.Y = dust110.velocity.Y * 2f;
                    Main.dust[num964].noGravity = true;
                    Main.dust[num964].scale *= 3f;
                    var dust111 = Main.dust[num964];
                    dust111.velocity.X = dust111.velocity.X * 2f;
                    var dust112 = Main.dust[num964];
                    dust112.velocity.Y = dust112.velocity.Y * 2f;
                    Main.dust[num965].noGravity = true;
                    Main.dust[num965].scale *= 3f;
                    var dust113 = Main.dust[num964];
                    dust113.velocity.X = dust113.velocity.X * 2f;
                    var dust114 = Main.dust[num965];
                    dust114.velocity.Y = dust114.velocity.Y * 2f;
                    if (Main.rand.Next(5) == 0)
                    {
                        var num966 = Projectile.NewProjectile(Projectile.GetSource_FromThis(),dust109.position.X, dust109.position.Y, dust109.velocity.X, dust109.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                        Main.projectile[num966].timeLeft = 60;
                        Main.projectile[num966].scale = 0.5f;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        var num967 = Projectile.NewProjectile(Projectile.GetSource_FromThis(),dust111.position.X, dust111.position.Y, dust111.velocity.X, dust111.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                        Main.projectile[num967].timeLeft = 60;
                        Main.projectile[num967].scale = 0.5f;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        var num968 = Projectile.NewProjectile(Projectile.GetSource_FromThis(),dust113.position.X, dust113.position.Y, dust113.velocity.X, dust113.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                        Main.projectile[num968].timeLeft = 60;
                        Main.projectile[num968].scale = 0.5f;
                    }
                }
                Main.dust[num963].scale *= 1.5f;
                var dust115 = Main.dust[num963];
                dust115.velocity.X = dust115.velocity.X * 1.2f;
                var dust116 = Main.dust[num963];
                dust116.velocity.Y = dust116.velocity.Y * 1.2f;
                Main.dust[num963].scale *= num960;
                Main.dust[num964].scale *= 1.5f;
                var dust117 = Main.dust[num964];
                dust117.velocity.X = dust117.velocity.X * 1.2f;
                var dust118 = Main.dust[num964];
                dust118.velocity.Y = dust118.velocity.Y * 1.2f;
                Main.dust[num964].scale *= num960;
                Main.dust[num965].scale *= 1.5f;
                var dust119 = Main.dust[num965];
                dust119.velocity.X = dust119.velocity.X * 1.2f;
                var dust120 = Main.dust[num965];
                dust120.velocity.Y = dust120.velocity.Y * 1.2f;
                Main.dust[num965].scale *= num960;
                if (Main.rand.Next(5) == 0)
                {
                    var num969 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), dust115.position.X, dust115.position.Y, dust115.velocity.X, dust115.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                    Main.projectile[num969].timeLeft = 60;
                    Main.projectile[num969].scale = 0.5f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    var num970 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), dust117.position.X, dust117.position.Y, dust117.velocity.X, dust117.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                    Main.projectile[num970].timeLeft = 60;
                    Main.projectile[num970].scale = 0.5f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    var num971 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), dust119.position.X, dust119.position.Y, dust119.velocity.X, dust119.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                    Main.projectile[num971].timeLeft = 60;
                    Main.projectile[num971].scale = 0.5f;
                }
                if (num961 == 75)
                {
                    Main.dust[num963].velocity += Projectile.velocity;
                    if (!Main.dust[num963].noGravity)
                    {
                        Main.dust[num963].velocity *= 0.5f;
                    }
                }
            }
        }
        Projectile.rotation += 0.3f * Projectile.direction;
        return;
    }
}
