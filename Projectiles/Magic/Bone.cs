using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class Bone1 : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.penetrate = 2;
        Projectile.scale = 1.2f;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.extraUpdates = 1;
    }
    public override void AI()
    {
        Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
        //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile.ai[0]++;
        if(Projectile.ai[0] > 45)
        {
            Projectile.velocity.Y += 0.15f;
        }
        Projectile.velocity *= 0.99f;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin;

        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * (-i * 2), Projectile.velocity.Y * (-i * 2)), frame, (lightColor * (1 - (i * 0.25f))) * 0.5f, Projectile.rotation * (1 - (i * 0.1f)), frameOrigin, Projectile.scale * (1 - (i * 0.1f)), SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 4; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Bone, -Projectile.velocity.X * 0.25f, -Projectile.velocity.Y * 0.25f, default, default, 0.9f);
        }
        SoundEngine.PlaySound(SoundID.NPCHit2, Projectile.Center);
    }
}
public class Bone2 : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.scale = 1.2f;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.extraUpdates = 1;
    }
    public override void AI()
    {
        Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
        //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 45)
        {
            Projectile.velocity.Y += 0.15f;
        }
        Projectile.velocity *= 0.99f;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin;

        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * (-i * 2), Projectile.velocity.Y * (-i * 2)), frame, (lightColor * (1 - (i * 0.25f))) * 0.5f, Projectile.rotation * (1 - (i * 0.1f)), frameOrigin, Projectile.scale * (1 - (i * 0.1f)), SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 4; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Bone, -Projectile.velocity.X * 0.25f, -Projectile.velocity.Y * 0.25f, default, default, 0.9f);
        }
        SoundEngine.PlaySound(SoundID.NPCHit2, Projectile.Center);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 2 + Main.rand.Next(2); i++)
        {
            Vector2 shootDir = new Vector2(Projectile.oldVelocity.X, Projectile.oldVelocity.Y);
            int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, shootDir.RotatedByRandom(0.4f), ModContent.ProjectileType<Bone1>(), Projectile.damage / 2, Projectile.knockBack, Main.player[Projectile.owner].whoAmI);
            Main.projectile[proj].ai[0] = 45;
        }
        SoundEngine.PlaySound(SoundID.NPCHit2, Projectile.Center);
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        for (int i = 0; i < 2 + Main.rand.Next(2); i++)
        {
            float x = Projectile.velocity.X;
            float y = Projectile.velocity.Y;
            if (x != oldVelocity.X)
            {
                x = -oldVelocity.X;
            }
            if (y != oldVelocity.Y)
            {
                y = -oldVelocity.Y;
            }
            Vector2 shootDir = new Vector2(x, y);
            int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, shootDir.RotatedByRandom(0.4f) * 1.15f, ModContent.ProjectileType<Bone1>(), Projectile.damage / 2, Projectile.knockBack, Main.player[Projectile.owner].whoAmI);
            Main.projectile[proj].ai[0] = 45;
        }
        return true;
    }
}
public class Bone3 : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.scale = 1.2f;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.extraUpdates = 1;
        DrawOffsetX = -17;
        DrawOriginOffsetY = -16;
    }
    public override void AI()
    {
        Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
        //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 45)
        {
            Projectile.velocity.Y += 0.15f;
        }
        Projectile.velocity *= 0.99f;
    }
    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        int size = 10;
        hitbox.X -= size;
        hitbox.Y -= size;
        hitbox.Width += size * 2;
        hitbox.Height += size * 2;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin;

        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture, new Vector2(drawPos.X - 9, drawPos.Y - 8) + new Vector2(Projectile.velocity.X * (-i * 2), Projectile.velocity.Y * (-i * 2)), frame, (lightColor * (1 - (i * 0.25f))) * 0.5f, Projectile.rotation * (1 - (i * 0.1f)), frameOrigin, Projectile.scale * (1 - (i * 0.1f)), SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, new Vector2(drawPos.X - 9, drawPos.Y - 8), frame, lightColor, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 10; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Bone, -Projectile.oldVelocity.X * 0.35f, -Projectile.oldVelocity.Y * 0.35f, default, default, 0.9f);
        }
        SoundEngine.PlaySound(SoundID.NPCHit2, Projectile.Center);
    }
}
public class Bone4 : ModProjectile
{
    public float bounces = 3;
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.penetrate = 2;
        Projectile.scale = 1.2f;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.extraUpdates = 1;
    }
    public override void AI()
    {
        Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
        //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 45)
        {
            Projectile.velocity.Y += 0.15f;
        }
        Projectile.velocity *= 0.99f;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin;

        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * (-i * 2), Projectile.velocity.Y * (-i * 2)), frame, (lightColor * (1 - (i * 0.25f))) * 0.5f, Projectile.rotation * (1 - (i * 0.1f)), frameOrigin, Projectile.scale * (1 - (i * 0.1f)), SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if(bounces == 0)
        {
            Projectile.Kill();
        }
        bounces--;
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = -oldVelocity.X;
        }
        if (Projectile.velocity.Y != oldVelocity.Y)
        {
            Projectile.velocity.Y = -oldVelocity.Y;
        }
        Projectile.velocity *= 0.99f;
        SoundEngine.PlaySound(SoundID.NPCHit2, Projectile.Center);
        return false;
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 4; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Bone, -Projectile.velocity.X * 0.25f, -Projectile.velocity.Y * 0.25f, default, default, 0.9f);
        }
    }
}
