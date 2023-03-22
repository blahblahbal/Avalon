using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.CrystalUnity;

public class AmberShard : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 20 / 16;
        Projectile.height = dims.Height * 20 / 16 / Main.projFrames[Projectile.type];
        Projectile.scale = 1f;
        Projectile.alpha = 0;
        Projectile.aiStyle = 1;
        Projectile.timeLeft = 3600;
        Projectile.friendly = true;
        AIType = ProjectileID.CursedBullet;
        Projectile.penetrate = 1;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
    }
    public override void AI()
    {
        for (var i = 0; i < 2; i++)
        {
            var dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.AmberBolt, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
        }
        if (Projectile.ai[1] == 0f)
        {
            Projectile.ai[1] = 1f;
            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
        }

        Lighting.AddLight(new Vector2((int)((Projectile.position.X + Projectile.width / 2) / 16f), (int)((Projectile.position.Y + Projectile.height / 2) / 16f)), new Color(252, 193, 45).ToVector3());
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 60 * 5);
    }
    
    public override void OnHitPlayer(Player target, Player.HurtInfo info) {
        if (!info.PvP) {
            return;
        }
        target.AddBuff(BuffID.OnFire, 60 * 5);
    }
}
