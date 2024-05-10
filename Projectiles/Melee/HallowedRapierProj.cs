using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
namespace Avalon.Projectiles.Melee;

using Microsoft.CodeAnalysis;
using Terraria.Graphics.Shaders;


public class HallowedRapierProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
    }
    public Player player => Main.player[Projectile.owner];
    public override Color? GetAlpha(Color lightColor)
    {
        return lightColor;
    }
    public override void SetDefaults()
    {
        Projectile.width = 40;
        Projectile.height = 40;
        Projectile.aiStyle = 75;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = -1;
        Projectile.ownerHitCheck = true;
    }
    private int soundDelay;
    public override void AI()
    {
        int num2 = 9;
        Vector2 vector = player.RotatedRelativePoint(player.MountedCenter);
        float scale = Projectile.ai[1];
        Projectile.ai[0] += 1f;
        
        float num3 = Main.rand.NextFloatDirection() * ((float)Math.PI * 2f) * 0.02f;
        soundDelay--;
        if (soundDelay <= 0)
        {
            SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
            soundDelay = 6;
        }
        if (Main.myPlayer == Projectile.owner)
        {
            if (player.channel && !player.noItems && !player.CCed)
            {
                float num46 = 1f;
                if (player.inventory[player.selectedItem].shoot == Projectile.type)
                {
                    num46 = player.inventory[player.selectedItem].shootSpeed;
                }
                Vector2 vec3 = Main.MouseWorld - vector;
                vec3.Normalize();
                if (vec3.HasNaNs())
                {
                    vec3 = Vector2.UnitX * player.direction;
                }
                vec3 *= num46;
                if (vec3.X != Projectile.velocity.X || vec3.Y != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.velocity = vec3;
            }
            else
            {
                Projectile.Kill();
            }
            
        }
        Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.PiOver4/2);
        player.SetDummyItemTime(num2);
        DelegateMethods.v3_1 = new Vector3(0.5f, 0.5f, 0.5f);
        Utils.PlotTileLine(Projectile.Center - Projectile.velocity, Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.Zero) * 80f, 16f, DelegateMethods.CastLightOpen);
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection * Main.player[Projectile.owner].gravDir;
        int HalfSpriteWidth = TextureAssets.Projectile[Type].Value.Width / 2;

        int HalfProjWidth = Projectile.width / 2;

        if (Projectile.spriteDirection == 1)
        {
            DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth) * Main.player[Projectile.owner].gravDir;
            DrawOffsetX = (int)-DrawOriginOffsetX * 2;
            DrawOffsetX -= Main.player[Projectile.owner].gravDir == 1 ? 0 : HalfSpriteWidth;
        }
        else
        {
            DrawOriginOffsetX = (HalfProjWidth - HalfSpriteWidth) * Main.player[Projectile.owner].gravDir;
            DrawOffsetX = Main.player[Projectile.owner].gravDir == 1 ? 0 : -HalfSpriteWidth;
        }
        DrawOriginOffsetY = -(TextureAssets.Projectile[Type].Value.Width / 6);
        DrawOffsetX -= Projectile.spriteDirection * 4;
        Projectile.position += Vector2.Normalize(Projectile.velocity) * 40;

        for (float num8 = 0f; num8 <= 1f; num8 += 0.05f)
        {
            float num9 = Utils.Remap(num8, 0f, 1f, 1f, 5f);
            Rectangle rectangle = Projectile.Hitbox;
            Vector2 vector5 = Projectile.velocity.SafeNormalize(Vector2.Zero) * Projectile.width * num9 * Projectile.scale;
            rectangle.Offset((int)vector5.X, (int)vector5.Y);



            Vector2 location = new Vector2(Main.rand.NextFloat(rectangle.X, rectangle.X + rectangle.Width), Main.rand.NextFloat(rectangle.Y, rectangle.Y + rectangle.Height));

            Projectile.NewProjectile(Projectile.InheritSource(Projectile), location, location.DirectionTo(player.Center), ModContent.ProjectileType<HallowedRapierVis>(), 0, Projectile.knockBack, Projectile.owner, Projectile.rotation);
        }
        if (Projectile.ai[0] % 4f == 1)
        {

            

            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center + (Projectile.velocity * Main.rand.Next(1, 7)), Projectile.velocity / 50, ModContent.ProjectileType<HallowedRapierVis>(), 0, Projectile.knockBack, Projectile.owner, Projectile.rotation);

        }


        if (Projectile.ai[0] >= 8f)
            {
                Projectile.ai[0] = 0f;
            
        }
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        for (float num8 = 0f; num8 <= 1f; num8 += 0.05f)
        {
            float num9 = Utils.Remap(num8, 0f, 1f, 1f, 5f);
            Rectangle rectangle = projHitbox;
            Vector2 vector5 = Projectile.velocity.SafeNormalize(Vector2.Zero) * Projectile.width * num9 * Projectile.scale;
            rectangle.Offset((int)vector5.X, (int)vector5.Y);
            if (rectangle.Intersects(targetHitbox))
            {
                Vector2 location = new Vector2(Main.rand.NextFloat(rectangle.X, rectangle.X + rectangle.Width), Main.rand.NextFloat(rectangle.Y, rectangle.Y + rectangle.Height));

                Projectile.NewProjectile(Projectile.InheritSource(Projectile), location, Projectile.velocity / 5, ModContent.ProjectileType<HallowedRapierVis>(), 0, Projectile.knockBack, Projectile.owner, Projectile.rotation, 1);

                return true;
            }
            

        }
       
        return false;
    }
    public override void CutTiles()
    {
        Vector2 end = Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.UnitX) * 220f * Projectile.scale;
        Utils.PlotTileLine(Projectile.Center, end, 80f * Projectile.scale, DelegateMethods.CutTiles);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        return base.PreDraw(ref lightColor);
    }

}
