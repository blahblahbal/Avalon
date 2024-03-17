using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Projectiles;

namespace Avalon.Projectiles.Ranged;

public class SunsShadowProj : ModProjectile
{
    private Color lightColor;
    private int dustType;
    public override void SetDefaults()
    {
        Projectile.width = Projectile.height = 10;
        Projectile.alpha = 0;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.friendly = true;
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 10 / 16;
        Projectile.height = dims.Height * 10 / 16;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
        DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
        Projectile.timeLeft = 200;

        lightColor = new Color(255, 200, 70);
        dustType = ModContent.DustType<SunsShadowDust>();
    }
    public override void AI()
    {
        if (Projectile.ai[1] == 0)
        {
            Projectile.scale = 0.3f;
        }
        Projectile.ai[1]++;
        if (Projectile.ai[1] > 5 && !Main.rand.NextBool(3))
        {
            var dust = Dust.NewDust(Projectile.position - Projectile.velocity * 2, Projectile.width, Projectile.height, dustType, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
            Main.dust[dust].alpha = 20;
            Main.dust[dust].noLightEmittence = true;
        }
        if (Projectile.ai[1] % 20 == 0)
        {
            int TargetNPC = ClassExtensions.FindClosestNPC(Projectile, 500, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly || npc.Distance(Projectile.Center) > 1000);
            if (TargetNPC != -1)
            {
                SoundEngine.PlaySound(SoundID.Item73, Projectile.position);
                Vector2 dustRad = new Vector2(2f, 2f);
                float dustAngle = 1f;
                for (int i = 0; i < 20; i++)
                {
                    var dust2 = Dust.NewDustPerfect(Projectile.Center + Projectile.velocity * 3f, dustType, dustRad.RotatedBy((MathHelper.Pi / 180f) * dustAngle), 50, default, 1f);
                    dust2.noGravity = true;
                    dust2.alpha = 20;
                    dustAngle += 18f;
                    dust2.noLightEmittence = true;
                }
                //Projectile P = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center + Projectile.velocity * 3f, Vector2.Normalize(Projectile.Center.DirectionTo(Main.npc[TargetNPC].Center)) * Projectile.velocity.Length() * 1.25f, (int)Projectile.ai[0], Projectile.damage / 2, Projectile.knockBack);
                //float angle = Main.rand.NextFloat(-6f, -7f);
                //for (int i = 0; i < 2; i++)
                //{
                //    Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center + Projectile.velocity * 3f, P.velocity.RotatedBy((MathHelper.Pi / 180f) * angle) * Main.rand.NextFloat(0.95f, 0.98f), (int)Projectile.ai[0], Projectile.damage, Projectile.knockBack);
                //    angle = Main.rand.NextFloat(6f, 7f);
                //}
                float angle = Main.rand.NextFloat(-2.5f, -3.5f);
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center + Projectile.velocity * 3f, Vector2.Normalize(Projectile.Center.DirectionTo(Main.npc[TargetNPC].Center + new Vector2(0, -10f)).RotatedBy((MathHelper.Pi / 180f) * angle)) * Projectile.velocity.Length() * 1.25f, (int)Projectile.ai[0], Projectile.damage / 2, Projectile.knockBack);
                    angle = Main.rand.NextFloat(2.5f, 3.5f);
                }
            }
        }
        if (Projectile.ai[1] <= 20)
        {
            Projectile.scale += 0.04f;
        }
        else if (Projectile.ai[1] % 40 < 20)
        {
            Projectile.scale += 0.01f;
        }
        else
        {
            Projectile.scale -= 0.01f;
        }
        Projectile.rotation += 0.025f * Projectile.direction;
        Lighting.AddLight(Projectile.Center, lightColor.ToVector3() * 0.8f);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 60 * 4);
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.OnFire, 60 * 4);
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item45, Projectile.position);
        for (int i = 0; i < 14; i++)
        {
            int dust = Dust.NewDust(Projectile.position - Projectile.velocity * Main.rand.NextFloat(1f, 2f), Projectile.width, Projectile.height, dustType, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, default, default, 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale *= 1.25f;
            Main.dust[dust].velocity *= 0.5f;
        }
    }
}
