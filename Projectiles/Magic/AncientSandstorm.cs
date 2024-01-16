using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
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
        //var randScale = 1f;
        //if (Projectile.ai[0] == 8f)
        //{
        //    randScale = 0.25f;
        //}
        //else if (Projectile.ai[0] == 9f)
        //{
        //    randScale = 0.5f;
        //}
        //else if (Projectile.ai[0] == 10f)
        //{
        //    randScale = 0.75f;
        //}
        var dustType = ModContent.DustType<Dusts.AncientSandDust>();
        if (Main.rand.NextBool(2))
        {
            for (var num962 = 0; num962 < 1; num962++)
            {
                var dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                var dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y - 10f), Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                var dust3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 10f), Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                if (Main.rand.Next(3) != 0)
                {
                    Main.dust[dust1].noGravity = true;
                    Main.dust[dust1].scale *= 3f;
                    var dust1_0 = Main.dust[dust1];
                    dust1_0.velocity *= 2f;
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].scale *= 3f;
                    var dust2_0 = Main.dust[dust2];
                    dust2_0.velocity *= 2f;
                    Main.dust[dust3].noGravity = true;
                    Main.dust[dust3].scale *= 3f;
                    var dust3_0 = Main.dust[dust3];
                    dust3_0.velocity *= 2f;
                    if (Main.rand.NextBool(5))
                    {
                        var proj1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, dust1_0.velocity.X, dust1_0.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                        Main.projectile[proj1].timeLeft = 60;
                        Main.projectile[proj1].scale = 0.5f;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        var proj2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, dust2_0.velocity.X, dust2_0.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                        Main.projectile[proj2].timeLeft = 60;
                        Main.projectile[proj2].scale = 0.5f;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        var proj3 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, dust3_0.velocity.X, dust3_0.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                        Main.projectile[proj3].timeLeft = 60;
                        Main.projectile[proj3].scale = 0.5f;
                    }
                }
                Main.dust[dust1].scale *= 1.5f;
                var dust1_1 = Main.dust[dust1];
                dust1_1.velocity *= 1.2f;
                //Main.dust[dust1].scale *= randScale;
                Main.dust[dust2].scale *= 1.5f;
                var dust2_1 = Main.dust[dust2];
                dust2_1.velocity *= 1.2f;
                //Main.dust[dust2].scale *= randScale;
                Main.dust[dust3].scale *= 1.5f;
                var dust3_1 = Main.dust[dust3];
                dust3_1.velocity *= 1.2f;
                //Main.dust[dust3].scale *= randScale;
                if (Main.rand.NextBool(5))
                {
                    var proj4 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, dust1_1.velocity.X, dust1_1.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                    Main.projectile[proj4].timeLeft = 60;
                    Main.projectile[proj4].scale = 0.5f;
                }
                if (Main.rand.NextBool(5))
                {
                    var proj5 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, dust2_1.velocity.X, dust2_1.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                    Main.projectile[proj5].timeLeft = 60;
                    Main.projectile[proj5].scale = 0.5f;
                }
                if (Main.rand.NextBool(5))
                {
                    var proj6 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, dust3_1.velocity.X, dust3_1.velocity.Y, ModContent.ProjectileType<AncientSandy>(), (int)(Main.player[Projectile.owner].GetDamage(DamageClass.Magic)).ApplyTo(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].damage), Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].knockBack, Main.player[Projectile.owner].whoAmI, 0f, 0f);
                    Main.projectile[proj6].timeLeft = 60;
                    Main.projectile[proj6].scale = 0.5f;
                }
            }
        }
        Projectile.rotation += 0.3f * Projectile.direction;
        return;
    }
}
