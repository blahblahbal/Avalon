using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class IckyArrow : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.arrow = true;
        Rectangle dims = this.GetDims();
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.WoodenArrowFriendly;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
    }

    public override void OnKill(int timeLeft)
    {
        Projectile.maxPenetrate = -1;
        Projectile.penetrate = -1;

        int explosionArea = 5 * 16;
        Vector2 oldSize = Projectile.Size;
        Projectile.position = Projectile.Center;
        Projectile.Size += new Vector2(explosionArea);
        Projectile.Center = Projectile.position;

        Projectile.tileCollide = false;
        Projectile.velocity *= 0.01f;
        Projectile.Damage();
        Projectile.scale = 0.01f;

        Projectile.position = Projectile.Center;
        Projectile.Size = new Vector2(10);
        Projectile.Center = Projectile.position;

        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);

        for (int i = 0; i < 20; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (Projectile.ai[2] == 0) ? ModContent.DustType<Dusts.CoughwoodDust>() : ModContent.DustType<Dusts.SnotsandDust>(), 0, 0, 0, default, 2f);
            Main.dust[d].velocity = Main.rand.NextVector2Circular(12, 12);
            Main.dust[d].noGravity = true;
            //Main.dust[d].fadeIn = 1.3f;
            Main.dust[d].alpha = 128;
        }
        for (int i = 0; i < 12; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.SnotsandDust>(), 0, 0, 0, default, 1.7f);
            //Main.dust[d].color = Color.Red;
            Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-5, 0).RotatedBy(Projectile.velocity.ToRotation());
            Main.dust[d].noGravity = Main.rand.NextBool(3);
            Main.dust[d].alpha = 128;
        }
        //for (int i = 0; i < 9; i++)
        //{
        //    int g = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(10, 6) + new Vector2(-1, 0).RotatedBy(Projectile.velocity.ToRotation()), Main.rand.Next(61, 63), 0.8f);
        //    Main.gore[g].alpha = 128;
        //}
        Projectile.position.X = Projectile.position.X + Projectile.width / 2;
        Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.position.X = Projectile.position.X - Projectile.width / 2;
        Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
    }
}
