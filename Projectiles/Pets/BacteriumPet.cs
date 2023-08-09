using System;
using System.IO;
using Avalon.Buffs.Pets;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Pets;

public class BacteriumPet : ModProjectile
{
    public int eyeCounter = 0;
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 13;
        Main.projPet[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = 32;
        Projectile.height = 40;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 2;
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
        if (Main.player[Projectile.owner].HasBuff(ModContent.BuffType<Bacterium>()))
            Projectile.timeLeft = 2;

        if (Main.rand.NextBool(10))
        {
            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, 0, 0, 128);
            d.noGravity = true;
            d.velocity *= 0.3f;
            d.velocity += Projectile.velocity;
            d.fadeIn = 1.2f;
        }
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
            //Projectile.ai[1] = 0f;
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

        if (Projectile.velocity.X > 0)
        {
            if (++Projectile.frameCounter % 5 == 0)
            {
                if (Projectile.frame < 7)
                {
                    Projectile.frame = 7;
                }
                if (++Projectile.frame > 12)
                {
                    Projectile.frame = 7;
                }
                eyeCounter = Projectile.frameCounter / 5;
                if (eyeCounter > 5)
                {
                    eyeCounter = 0;
                }
                Projectile.ai[0]++;
                if (Projectile.ai[0] >= 20 && Projectile.ai[0] <= 30)
                {
                    if (Projectile.frameCounter >= 30)
                    {
                        Projectile.frameCounter = 0;
                    }
                    if (Projectile.ai[0] == 30)
                    {
                        Projectile.ai[0] = 0;
                    }
                }
            }
        }
        else if (Projectile.velocity.X < 0)
        {
            if (++Projectile.frameCounter % 5 == 0)
            {
                if (++Projectile.frame > 6)
                {
                    Projectile.frame = 1;
                }
                eyeCounter = Projectile.frameCounter / 5;
                if (eyeCounter > 5)
                {
                    eyeCounter = 0;
                }
                Projectile.ai[0]++;
                if (Projectile.ai[0] >= 20 && Projectile.ai[0] <= 30)
                {
                    if (Projectile.frameCounter >= 20)
                    {
                        Projectile.frameCounter = 0;
                    }
                    if (Projectile.ai[0] == 30)
                    {
                        Projectile.ai[0] = 0;
                    }
                }
            }
        }
        else
        {
            Projectile.frame = 0;
            eyeCounter = 0;
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture + "Eye").Value;
        Texture2D texBack = ModContent.Request<Texture2D>(Texture).Value;

        Color c = new Color(lightColor.R, lightColor.G, lightColor.B, 255);

        float num138 = (texture.Width - Projectile.width) * 0.5f + Projectile.width * 0.5f;
        SpriteEffects dir = SpriteEffects.None;
        if (Projectile.spriteDirection == -1)
        {
            dir = SpriteEffects.FlipHorizontally;
        }
        float num140 = Projectile.velocity.X * 0.5f;
        float num141 = Projectile.velocity.Y * 0.5f;
        Color alpha = Projectile.GetAlpha(c);
        int num143 = texture.Height / 6;
        int y11 = num143 * eyeCounter;
        int main = num143 * Projectile.frame;

        Main.EntitySpriteDraw(texBack, new Vector2(Projectile.position.X - Main.screenPosition.X + num138 + (float)1 - num140,
           Projectile.position.Y - Main.screenPosition.Y + (float)(Projectile.height / 2) + Projectile.gfxOffY - num141),
           (Rectangle?)new Rectangle(0, main, texture.Width, num143), alpha, Projectile.rotation,
           new Vector2(num138 + 1, (float)(Projectile.height / 2)), Projectile.scale, dir, 0f);

        Main.EntitySpriteDraw(texture, new Vector2(Projectile.position.X - Main.screenPosition.X + num138 + (float)1 - num140,
            Projectile.position.Y - Main.screenPosition.Y + (float)(Projectile.height / 2) + Projectile.gfxOffY - num141),
            (Rectangle?)new Rectangle(0, y11, texture.Width, num143), alpha, Projectile.rotation,
            new Vector2(num138 + 1, (float)(Projectile.height / 2)), Projectile.scale, dir, 0f);
        return false;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(eyeCounter);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        eyeCounter = reader.ReadInt32();
    }
}
