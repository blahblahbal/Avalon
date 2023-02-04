using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Common.Templates
{
    public abstract class SpearTemplate : ModProjectile // Thanks example mod ! ! ! 
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear);
            //Projectile.aiStyle = -1;
        }
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 96f;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

            player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

            float halfDuration = duration * 0.5f;
            float progress;
            //if(duration == halfDuration) 
            //{
            //    Projectile.ResetLocalNPCHitImmunity();
            //}

            // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / (duration * 0.5f);
            }

            // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
            Projectile.Center = player.MountedCenter + Vector2.Lerp(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);
            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1)
            {
                // If sprite is facing left, rotate 45 degrees
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            else
            {
                // If sprite is facing right, rotate 135 degrees
                Projectile.rotation += MathHelper.ToRadians(135f);
            }
            if(Projectile.timeLeft <= duration * 0.25f)
            {
                Projectile.timeLeft = 0;
                player.SetDummyItemTime(0);
            }
        }
        //public void DrawPointyStabbyLight(int Intensity, Color color, Vector2 Scale, Vector2 Offset)
        //{
        //    Offset = Offset.RotatedBy(Projectile.rotation);
        //    Offset.X *= -Projectile.direction;
        //    Player player = Main.player[Projectile.owner];
        //    Texture2D texture = ModContent.Request<Texture2D>("ExxoAvalonOrigins/Assets/Sparkly").Value;
        //    Rectangle frame = texture.Frame();
        //    Vector2 frameOrigin = frame.Size() / 2f;

        //    float halfDuration = player.itemAnimationMax * 0.5f;
        //    float timeTillRemove = (float)player.itemAnimation / player.itemAnimationMax;
        //    Scale.X *= (timeTillRemove * 2);
        //    Scale.Y /= timeTillRemove;
        //    if (Projectile.timeLeft > player.itemAnimationMax / 2)
        //    {
        //        int j = Intensity;
        //        for (int i = 1; i < Intensity; i++)
        //        {
        //            j--;
        //            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + Offset + new Vector2(0, i * 10 * Scale.Y * -Projectile.direction).RotatedBy(Projectile.rotation), frame, color * (j / Intensity), Projectile.rotation, frameOrigin, Scale - new Vector2(i / Intensity), SpriteEffects.None);
        //        }
        //    }
        //}

        // Jeo make original code challenge IMPOSSIBLE
        public static void DrawProj_Spear(Projectile proj, Microsoft.Xna.Framework.Color color, SpriteEffects dir, Vector2 offset)
        {
            dir = SpriteEffects.None;
            float num = (float)Math.Atan2(proj.velocity.Y, proj.velocity.X) + 2.355f;
            Asset<Texture2D> val = TextureAssets.Projectile[proj.type];
            Player player = Main.player[proj.owner];
            Microsoft.Xna.Framework.Rectangle value = val.Frame();
            Microsoft.Xna.Framework.Rectangle rect = proj.getRect();
            Vector2 vector = Vector2.Zero;
            if (player.direction > 0)
            {
                dir = SpriteEffects.FlipHorizontally;
                vector.X = val.Width();
                num -= (float)Math.PI / 2f;
            }
            if (player.gravDir == -1f)
            {
                if (proj.direction == 1)
                {
                    dir = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                    vector = new Vector2(val.Width(), val.Height());
                    num -= (float)Math.PI / 2f;
                }
                else if (proj.direction == -1)
                {
                    dir = SpriteEffects.FlipVertically;
                    vector = new Vector2(0f, val.Height());
                    num += (float)Math.PI / 2f;
                }
            }
            Vector2.Lerp(vector, value.Center.ToVector2(), 0.25f);
            float num2 = 0f;
            Vector2 vector2 = proj.Center + new Vector2(0f, proj.gfxOffY);
            if (proj.AI_019_Spears_GetExtensionHitbox(out var extensionBox))
            {
                float num3 = extensionBox.Size().Length() / proj.Hitbox.Size().Length();
                _ = new Microsoft.Xna.Framework.Color(255, 255, 255, 0) * 1f;
                float num4 = Utils.Remap(player.itemAnimation, player.itemAnimationMax, (float)player.itemAnimationMax / 3f, 0f, 1f);
                float num5 = Utils.Remap(num4, 0f, 0.3f, 0f, 1f) * Utils.Remap(num4, 0.3f, 1f, 1f, 0f);
                num5 = 1f - (1f - num5) * (1f - num5);
                Vector2 vector3 = extensionBox.Center.ToVector2() + new Vector2(0f, proj.gfxOffY);
                Vector2.Lerp(player.MountedCenter, vector3, 1.1f);
                Texture2D value2 = TextureAssets.Extra[98].Value;
                Vector2 origin = value2.Size() / 2f;
                color = new Microsoft.Xna.Framework.Color(255, 255, 255, 0) * 0.5f;
                //switch (proj.type)
                //{
                //    case 105:
                //        color = new Microsoft.Xna.Framework.Color(255, 220, 80, 0);
                //        break;
                //    case 46:
                //        color = new Microsoft.Xna.Framework.Color(180, 80, 255, 0);
                //        break;
                //    case 342:
                //        color = new Microsoft.Xna.Framework.Color(80, 140, 255, 0);
                //        break;
                //    case 153:
                //        color = new Microsoft.Xna.Framework.Color(255, 50, 30, 15);
                //        break;
                //}
                float num6 = num - (float)Math.PI / 4f * (float)proj.spriteDirection;
                if (player.gravDir < 0f)
                {
                    num6 -= (float)Math.PI / 2f * (float)proj.spriteDirection;
                }
                Main.EntitySpriteDraw(value2, Vector2.Lerp(vector3, vector2, 0.5f) - Main.screenPosition, null, color * num5, num6, origin, new Vector2(num5 * num3, num3) * proj.scale * num3, dir);
                Main.EntitySpriteDraw(value2, Vector2.Lerp(vector3, vector2, 1f) - Main.screenPosition, null, color * num5, num6, origin, new Vector2(num5 * num3, num3 * 1.5f) * proj.scale * num3, dir);
                Main.EntitySpriteDraw(value2, Vector2.Lerp(player.MountedCenter, vector2, num4 * 1.5f - 0.5f) - Main.screenPosition + new Vector2(0f, 2f), null, color * num5, num6, origin, new Vector2(num5 * num3 * 1f * num5, num3 * 2f * num5) * proj.scale * num3, dir);
                for (float num7 = 0.4f; num7 <= 1f; num7 += 0.1f)
                {
                    Vector2 vector4 = Vector2.Lerp(player.MountedCenter, vector3, num7 + 0.2f);
                    Main.EntitySpriteDraw(value2, vector4 - Main.screenPosition + new Vector2(0f, 2f), null, color * num5 * 0.75f * num7, num6, origin, new Vector2(num5 * num3 * 1f * num5, num3 * 2f * num5) * proj.scale * num3, dir);
                }
                extensionBox.Offset((int)(0f - Main.screenPosition.X), (int)(0f - Main.screenPosition.Y));
                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, extensionBox, Microsoft.Xna.Framework.Color.Gold * num2);
            }
            Main.EntitySpriteDraw(val.Value, vector2 - Main.screenPosition, value, proj.GetAlpha(Color.White), num, vector, proj.scale, dir);
            rect.Offset((int)(0f - Main.screenPosition.X), (int)(0f - Main.screenPosition.Y));
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, rect, Microsoft.Xna.Framework.Color.White * num2);
        }
    }
}
