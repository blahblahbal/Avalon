using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.Templates
{
    public abstract class LongbowTemplate : ModProjectile
    {
        public static Color NotificationColor = new Color(128, 128, 128, 0);
        public override bool? CanDamage()
        {
            return false;
        }

        public virtual SoundStyle shootSound => new SoundStyle("Avalon/Sounds/Item/LongbowShot") { Pitch = 0.2f, Volume = 0.7f };
        public virtual float HowFarShouldTheBowBeHeldFromPlayer => 25;
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.alpha = 0;
            Projectile.scale = 1;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public static float Power;
        bool Notified;
        public static float FullPowerGlow;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        SlotId BowPullSound = SlotId.Invalid;
        public override void AI()
        {
            MoveHands();
            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = player.direction;
            int ammo = (int)Projectile.ai[0];
            player.heldProj = Projectile.whoAmI;
            if (Projectile.ai[1] == 0)
            {
                SoundStyle PullSound = new SoundStyle("Avalon/Sounds/Item/LongbowPull")
                {
                    Volume = 0.3f,
                    Pitch = -(float)(player.itemAnimationMax * 0.015f),
                    MaxInstances = 2,
                };
                BowPullSound = SoundEngine.PlaySound(PullSound, Projectile.position);
            }
            if ((!player.channel || Notified) && SoundEngine.TryGetActiveSound(BowPullSound, out ActiveSound sound) && sound != null && sound.IsPlaying)
            {
                sound.Volume = 0;
            }
            if (player.channel)
            {
                Projectile.ai[1]++;

                Power = MathHelper.Clamp(Projectile.ai[1] / player.itemAnimationMax, 0, 1);
                Projectile.timeLeft = 20;
                player.SetDummyItemTime(20);
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile.velocity = player.MountedCenter.DirectionTo(Main.MouseWorld);
                    player.direction = Main.MouseWorld.X < player.MountedCenter.X ? -1 : 1;
                }
            }
            else
            {
                if (Power < 0.05f)
                    Power = 0.05f;
                if (Main.myPlayer == Projectile.owner && Projectile.timeLeft == 19)
                {
                    Shoot(Projectile.GetSource_FromThis(), player.MountedCenter, Projectile.velocity * player.HeldItem.shootSpeed * Power, ammo, (int)(Projectile.damage * (0.1f + (Power * 1.65f))), Projectile.knockBack * (0.5f + Power), Power);
                    //Projectile.NewProjectile(Projectile.GetSource_FromThis(),player.MountedCenter,Projectile.velocity * player.HeldItem.shootSpeed * Power,ammo,(int)(Projectile.damage * (0.1f + Power)),Projectile.knockBack * (0.5f + Power), player.whoAmI);
                }
                if (Projectile.timeLeft == 19)
                    SoundEngine.PlaySound(shootSound, Projectile.Center);
            }
            Vector2 vector = Vector2.Normalize(Projectile.velocity) * HowFarShouldTheBowBeHeldFromPlayer;
            Projectile.Center = player.MountedCenter + new Vector2(vector.X, vector.Y * 0.9f);
            Projectile.position.X -= player.direction * 6;
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Power == 1 && !Notified && player.whoAmI == Main.myPlayer)
            {
                Notified = true;
                FullPowerGlow = 1;
                SoundEngine.PlaySound(SoundID.MaxMana);
            }
            FullPowerGlow -= 0.05f;
            // Arrow light
            Vector2 drawPos = Projectile.Center + new Vector2((Projectile.frame * -2) + 8, 0).RotatedBy(Projectile.rotation);
            Projectile AmmoProj = new Projectile();
            AmmoProj.SetDefaults(ammo);
            Lighting.AddLight(drawPos, new Vector3(AmmoProj.light));
        }

        public virtual void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float Power)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Projectile.owner);
        }
        public void MoveHands()
        {
            Player player = Main.player[Projectile.owner];

            Vector2 ProjPosition = Projectile.Center;
            var CompositeArm = Player.CompositeArmStretchAmount.Full;

            float RotationOffset = -MathHelper.PiOver2;
            if (player.gravDir < 0)
            {
                Vector2 vector = Vector2.Normalize(Projectile.velocity) * HowFarShouldTheBowBeHeldFromPlayer;
                ProjPosition = player.MountedCenter + new Vector2(vector.X, vector.Y * -0.9f);
            }

            player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, new Vector2(player.MountedCenter.X + player.direction * 6, player.MountedCenter.Y).DirectionTo(ProjPosition + player.velocity).ToRotation() + RotationOffset);
            if (player.channel)
            {
                if (Power < 0.33)
                {
                    CompositeArm = Player.CompositeArmStretchAmount.Full;
                    Projectile.frame = 0;
                }
                else if (Power < 0.66f)
                {
                    CompositeArm = Player.CompositeArmStretchAmount.ThreeQuarters;
                    Projectile.frame = 1;
                }
                else if (Power < 0.99f)
                {
                    CompositeArm = Player.CompositeArmStretchAmount.Quarter;
                    Projectile.frame = 2;
                }
                else
                {
                    CompositeArm = Player.CompositeArmStretchAmount.None;
                    Projectile.frame = 3;
                }
            }
            else
            {
                CompositeArm = Player.CompositeArmStretchAmount.None;
                Projectile.frame = 0;
            }
            player.SetCompositeArmFront(true, CompositeArm, new Vector2(player.MountedCenter.X + player.direction * -6, player.MountedCenter.Y).DirectionTo(ProjPosition + player.velocity).ToRotation() + RotationOffset);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            DefaultBowDraw(lightColor, Vector2.Zero);
            if (FullPowerGlow > 0 && Main.myPlayer == Projectile.owner)
            {
                DefaultBowDraw(NotificationColor * FullPowerGlow, Vector2.Zero);
            }
            if (Main.player[Projectile.owner].channel)
            {
                DrawArrow(lightColor, Vector2.Zero);
            }
            return false;
        }
        public void DefaultBowDraw(Color lightColor, Vector2 Offset, float Scale = 1)
        {
            SpriteEffects Flip = Projectile.direction == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture);
            int frameHeight = texture.Value.Height / Main.projFrames[Projectile.type];
            Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Value.Width, frameHeight);
            Vector2 drawPos = Projectile.Center - Main.screenPosition + Offset;
            drawPos.Y += Main.player[Projectile.owner].gfxOffY;
            //Stretch 
            Main.EntitySpriteDraw(texture.Value, drawPos - new Vector2(Projectile.frame, 0).RotatedBy(Projectile.rotation), frame, lightColor, Projectile.rotation, new Vector2(texture.Value.Width, frameHeight) / 2, new Vector2(1 + (Projectile.frame * 0.06f), 1) * Projectile.scale * Scale, Flip, 0);
            //No stretch
            //Main.EntitySpriteDraw(texture, drawPos, frame, lightColor, Projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, Flip, 0);
        }
        public void DrawArrow(Color lightColor, Vector2 Offset, bool OverwriteGetAlphaOfArrow = false)
        {
            Offset.Y *= Projectile.spriteDirection;
            int ammo = (int)Projectile.ai[0];
            Projectile AmmoProj = new Projectile();
            AmmoProj.type = ammo;
            SpriteEffects Flip = Projectile.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[ammo].Value;
            if (texture == null)
            {
                Main.instance.LoadProjectile(ammo);
                texture = TextureAssets.Projectile[ammo].Value;
            }
            int frameHeight = texture.Height / Main.projFrames[ammo];
            Rectangle frame = new Rectangle(0, frameHeight * 0, texture.Width, frameHeight);
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2((Projectile.frame * -3) + 8 + Offset.X, Offset.Y).RotatedBy(Projectile.rotation);
            drawPos.Y += Main.player[Projectile.owner].gfxOffY;
            if(!OverwriteGetAlphaOfArrow)
                Main.EntitySpriteDraw(texture, drawPos, frame, AmmoProj.GetAlpha(lightColor), Projectile.rotation + MathHelper.PiOver2, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
            else
                Main.EntitySpriteDraw(texture, drawPos, frame,lightColor, Projectile.rotation + MathHelper.PiOver2, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
