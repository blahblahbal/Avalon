using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.Projectiles.Magic;

public class DevilScythe : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 48;
        Projectile.height = 48;
        Projectile.alpha = 512;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 4;
        Projectile.tileCollide = true;
        Projectile.scale = 0.9f;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.extraUpdates = 1;
    }
    public override void Kill(int timeLeft)
    {
        Projectile.oldVelocity *= 0.5f;
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int dustAmount = 0; dustAmount < 30; dustAmount++)
        {
            Dust d = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DesertTorch, 0f, 0f, 0, default(Color), 1f);
            d.noGravity = true;
            d.velocity = Projectile.oldVelocity.RotatedByRandom(1) * Main.rand.NextFloat(0.2f, 1f);
            d.noLightEmittence = true;
            d.fadeIn = Main.rand.NextFloat(1, 1.5f);
        }
        for (int dustAmount = 0; dustAmount < 15; dustAmount++)
        {
            Dust d = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BoneTorch, 0f, 0f, 0, default(Color), 1f);
            d.noGravity = true;
            d.velocity = Projectile.oldVelocity.RotatedByRandom(1) * Main.rand.NextFloat(0.2f, 0.8f);
            d.noLightEmittence = true;
            d.fadeIn = 1.4f;
        }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = -oldVelocity.X;
        }
        if (Projectile.velocity.Y != oldVelocity.Y)
        {
            Projectile.velocity.Y = -oldVelocity.Y;
        }
        Projectile.penetrate--;
        return false;
    }
    public override void AI()
    {
        Projectile.rotation += Projectile.direction * 0.2f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] <= 120f)
        {
            Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha - 10, -255, 255);
        }
        Projectile.velocity *= 1.04f;
        Dust d2 = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DesertTorch, 0f, 0f, 0, default(Color), Projectile.Opacity);
        d2.noGravity = true;
        d2.velocity = Projectile.velocity;
        d2.noLightEmittence = Main.rand.NextBool();

        if (Main.rand.NextBool(3))
        {
            Dust d = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BoneTorch, 0f, 0f, 0, default(Color), Projectile.Opacity * 0.8f);
            d.noGravity = true;
            d.velocity = Projectile.velocity;
            d.noLightEmittence = Main.rand.NextBool();
        }

        if (Projectile.ai[1] == 0)
        {
            Projectile.ai[2] = ClassExtensions.FindClosestNPC(Projectile, 400, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly && Collision.CanHit(npc, Projectile));
        }

        int targetNPC = (int)(Projectile.ai[2]);
        if (targetNPC != -1)
        {
            if (Main.npc[targetNPC].active == false || Main.npc[targetNPC].dontTakeDamage)
            {
                targetNPC = -1;
                Projectile.ai[1] = 0;
            }
            else
            {
                Projectile.ai[1] = 1;
                Projectile.velocity += Projectile.Center.DirectionTo(Main.npc[targetNPC].Center) * 0.02f * Projectile.velocity.Length();
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(Main.npc[targetNPC].Center) * Projectile.velocity.Length(), 0.03f);
            }
        }
        if(Projectile.velocity.Length() >= 10)
        {
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 10;
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        int frameHeight = texture.Height / Main.projFrames[Projectile.type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);

        for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
        {
            Main.EntitySpriteDraw(texture, Projectile.oldPos[i] - Main.screenPosition + (Projectile.Size / 2f), frame, new Color(255, 255, 255, 0) * ((float)(8 - i) / 8f) * Projectile.Opacity * 0.5f, Projectile.oldRot[i], new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, Projectile.position - Main.screenPosition + (Projectile.Size / 2f), frame, new Color(255,255,255, 25) * Projectile.Opacity, Projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = Projectile.width - 36;
        height = Projectile.height - 36;
        return true;
    }
}
