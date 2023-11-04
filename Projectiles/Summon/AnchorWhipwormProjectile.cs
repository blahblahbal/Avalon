using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon
{
    public class AnchorWhipwormProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // This makes the projectile use whip collision detection and allows flasks to be applied to it.
            ProjectileID.Sets.IsAWhip[Type] = true;
        }

        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            Projectile.DefaultToWhip();
            Projectile.width = 40;
            // use these to change from the vanilla defaults
             Projectile.WhipSettings.Segments = 30;
             Projectile.WhipSettings.RangeMultiplier = 0.53f;
        }

        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        private float ChargeTime
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        // This example uses PreAI to implement a charging mechanic.
        // If you remove this, also remove Item.channel = true from the item's SetDefaults.
        public override bool PreAI()
        {
            return true; // Prevent the vanilla whip AI from running.
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Pathogen>(), 240);
            Projectile.localAI[2] = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].type == ModContent.ProjectileType<AnchorWorm>() && Main.projectile[i].active && Main.projectile[i].ai[0] == target.whoAmI)
                {
                    Projectile.localAI[2]++;
                }
            }
            if (Projectile.localAI[2] < 5)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, target.Center.DirectionTo(Main.player[Projectile.owner].Center).RotatedByRandom(0.4f), ModContent.ProjectileType<AnchorWorm>(), (int)Main.player[Projectile.owner].GetTotalDamage(DamageClass.Summon).ApplyTo(20), 0, Projectile.owner,target.whoAmI);
            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            Projectile.damage = (int)(Projectile.damage * 0.9f); // Multihit penalty. Decrease the damage the more enemies the whip hits.
        }
        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);
            int randomSegment = Main.rand.Next(10, Projectile.WhipSettings.Segments);
            if (Vector2.Distance(list[randomSegment], player.position) < 60)
                return;
            if (Main.rand.NextBool(3))
            {
                Vector2 speed = list[randomSegment].DirectionFrom(list[randomSegment - 1]).RotatedByRandom(0.3) * 5;
                int d = Dust.NewDust(list[Projectile.WhipSettings.Segments], 0, 0, ModContent.DustType<Dusts.AnchorWormDust>(), speed.X, speed.Y, Scale: 0.85f);
                Main.dust[d].alpha = 128;
            }
        }
        // This method draws a line between all points of the whip, in case there's empty space between the sprites.
        private void DrawLine(List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;
            Rectangle frame = texture.Frame();
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];
            for (int i = 0; i < list.Count - 2; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates(), new Color(109, 72, 182));
                Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

                pos += diff;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);

            DrawLine(list);

            //Main.DrawWhip_WhipBland(Projectile, list);
            // The code below is for custom drawing.
            // If you don't want that, you can remove it all and instead call one of vanilla's DrawWhip methods, like above.
            // However, you must adhere to how they draw if you do.

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 pos = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                // These two values are set to suit this projectile's sprite, but won't necessarily work for your own.
                // You can change them if they don't!
                Rectangle frame = new Rectangle(0, 0, 40, 30); // The size of the Handle (measured in pixels)
                Vector2 origin = new Vector2(20, 15); // Offset for where the player's hand will start measured from the top left of the image.
                float scale = 1;

                // These statements determine what part of the spritesheet to draw for the current segment.
                // They can also be changed to suit your sprite.
                if (i == list.Count - 2)
                {
                    // This is the head of the whip. You need to measure the sprite to figure out these values.
                    frame.Y = 64; // Distance from the top of the sprite to the start of the frame.
                    frame.Height = 22; // Height of the frame.

                    // For a more impactful look, this scales the tip of the whip up when fully extended, and down when curled up.
                    Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                    float t = Timer / timeToFlyOut;
                    scale = MathHelper.Lerp(0.5f, 1.2f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
                }
                //else if (i > 10)
                //{
                //    // Third segment
                //    frame.Y = 58;
                //    frame.Height = 16;
                //}
                else if (i > 0)
                {
                    // Second Segment
                    frame.Y = 40;
                    frame.Height = 16;
                    //frame.Width = 40;
                }
                else if (i == 0)
                {
                    // First Segment
                    frame.Y = 2;
                    frame.Height = 28;
                }

                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is used to correct rotation.
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

                pos += diff;
            }
            return false;
        }
    }
}
