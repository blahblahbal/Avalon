using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.SolarSystem;

public class Sun : ModProjectile
{
    Vector2 mousePosition = Vector2.Zero;
    int planetSpawnTimer = 0;
    int dustTimer = 0;

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = -1;
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = 300;
        DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
        DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(planetSpawnTimer);
        writer.Write(dustTimer);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        planetSpawnTimer = reader.ReadInt32();
        dustTimer = reader.ReadInt32();
    }
    public override void AI()
    {
        dustTimer++;
        if (dustTimer > 30)
        {
            int num5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default(Color), 1.3f);
            Main.dust[num5].noGravity = true;
            Main.dust[num5].velocity.X *= Main.rand.Next(-2, 3);
            Main.dust[num5].velocity.Y *= Main.rand.Next(-2, 3);
            dustTimer = 0;
        }

        if (Main.player[Projectile.owner].channel)
        {
            Projectile.ai[0] = 1;
        }
        else
        {
            Projectile.ai[0] = 0; // not channeling
        }

        #region planet spawning
        if (Projectile.ai[2] == 1) // spawn planets
        {
            planetSpawnTimer++;
            switch (planetSpawnTimer)
            {
                case 50:
                    // mercury
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                        Projectile.velocity, ModContent.ProjectileType<Mercury>(), (int)(Projectile.damage * 1.2f),
                        Projectile.knockBack, ai0: 0, ai1: Projectile.whoAmI);
                    break;
                case 100:
                    // venus
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                        Projectile.velocity, ModContent.ProjectileType<Venus>(), (int)(Projectile.damage * 1.12f),
                        Projectile.knockBack, ai0: 1, ai1: Projectile.whoAmI);
                    break;
                case 150:
                    // earth
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                        Projectile.velocity, ModContent.ProjectileType<Earth>(), (int)(Projectile.damage * 1.04f),
                        Projectile.knockBack, ai0: 2, ai1: Projectile.whoAmI);
                    break;
                case 200:
                    // mars
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                        Projectile.velocity, ModContent.ProjectileType<Mars>(), (int)(Projectile.damage * 0.98f),
                        Projectile.knockBack, ai0: 3, ai1: Projectile.whoAmI);
                    break;
                case 250:
                    // jupiter
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                        Projectile.velocity, ModContent.ProjectileType<Jupiter>(), (int)(Projectile.damage * 0.9f),
                        Projectile.knockBack, ai0: 4, ai1: Projectile.whoAmI);
                    break;
                case 300:
                    // saturn
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                        Projectile.velocity, ModContent.ProjectileType<Saturn>(), (int)(Projectile.damage * 0.84f),
                        Projectile.knockBack, ai0: 5, ai1: Projectile.whoAmI);
                    break;
                case 350:
                    // uranus
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                        Projectile.velocity, ModContent.ProjectileType<Uranus>(), (int)(Projectile.damage * 0.77f),
                        Projectile.knockBack, ai0: 6, ai1: Projectile.whoAmI);
                    break;
                case 400:
                    // neptune
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                        Projectile.velocity, ModContent.ProjectileType<Neptune>(), (int)(Projectile.damage * 0.71f),
                        Projectile.knockBack, ai0: 7, ai1: Projectile.whoAmI);
                    Projectile.ai[2] = 2;
                    planetSpawnTimer = 0;
                    break;
            }
        }
        #endregion

        if (Projectile.ai[0] == 1)
        {
            if (Projectile.ai[1] == 0)
            {
                Projectile.timeLeft = 300;
                // assign the destination as the cursor
                float vecX = Main.mouseX + Main.screenPosition.X;
                float vecY = Main.mouseY + Main.screenPosition.Y;
                mousePosition = new Vector2(vecX, vecY);

                // get the inverse melee speed - yoyos do the same thing
                float inverseMeleeSpeed = 1 / Main.player[Projectile.owner].GetTotalAttackSpeed(DamageClass.Melee);
                float speed = (1f + inverseMeleeSpeed * 3f) / 4f;

                // move towards the destination
                Vector2 heading = mousePosition - Projectile.Center;
                heading.Normalize();
                heading *= new Vector2(speed * 10).Length();
                Projectile.velocity = heading;

                if (Vector2.Distance(Projectile.Center, mousePosition) < 20)
                {
                    Projectile.ai[1] = 1; // lock on cursor
                    return;
                }
            }
            else if (Projectile.ai[1] == 1) // sticking to cursor
            {
                Projectile.timeLeft = 300;
                float vecX = Main.mouseX + Main.screenPosition.X;
                float vecY = Main.mouseY + Main.screenPosition.Y;
                mousePosition = new Vector2(vecX, vecY);
                Projectile.Center = mousePosition;
                if (planetSpawnTimer == 0)
                {
                    Projectile.ai[2]++;
                }
                return;
            }
        }
        else if (Projectile.ai[0] == 0)
        {
            // head away from the player
            Vector2 heading = mousePosition - Main.player[Projectile.owner].Center;
            heading.Normalize();
            heading *= new Vector2(10).Length();
            Projectile.velocity = heading;

            // if the velocity's length is less than 2 (in other words, mostly still),
            // head away from the player in the direction of the cursor
            if (Projectile.velocity.Length() < 2f)
            {
                heading = mousePosition - Main.player[Projectile.owner].Center;
                heading.Normalize();
                heading *= new Vector2(10).Length();
                Projectile.velocity = heading;
            }
            // otherwise, head in the direction your cursor was heading before
            // releasing the mouse button
            else
            {
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.One) * 16;
            }
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        var texture = Mod.Assets.Request<Texture2D>("Projectiles/Melee/SolarSystem/SolarSystem_Chain");

        var position = Projectile.Center;
        var mountedCenter = Main.player[Projectile.owner].MountedCenter;
        var sourceRectangle = new Rectangle?();
        var origin = new Vector2(texture.Value.Width * 0.5f, texture.Value.Height * 0.5f);
        float num1 = texture.Value.Height;
        var vector2_4 = mountedCenter - position;
        var rotation = (float)Math.Atan2(vector2_4.Y, vector2_4.X) - 1.57f;
        var flag = true;
        if (float.IsNaN(position.X) && float.IsNaN(position.Y))
            flag = false;
        if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
            flag = false;
        while (flag)
        {
            if (vector2_4.Length() < num1 + 1.0)
            {
                flag = false;
            }
            else
            {
                var vector2_1 = vector2_4;
                vector2_1.Normalize();
                position += vector2_1 * num1;
                vector2_4 = mountedCenter - position;
                var color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                color2 = Projectile.GetAlpha(color2);
                Main.EntitySpriteDraw(texture.Value, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
            }
        }

        Texture2D sunTex = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle sunFrame = sunTex.Frame();
        Vector2 sunFrameOrigin = sunFrame.Size() / 2f;
        Main.EntitySpriteDraw(sunTex, Projectile.position - Main.screenPosition + sunFrameOrigin, sunFrame, Color.White, Projectile.rotation, sunFrameOrigin, 1f, SpriteEffects.None, 0);

        return false;
    }
}
