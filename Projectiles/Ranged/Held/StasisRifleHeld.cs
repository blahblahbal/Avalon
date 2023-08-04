using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Held
{
    public class StasisRifleHeld : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.scale = 0.85f;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
        }
        float frame = 0;
        bool Notify;
        public override void AI()
        {
            float Power = 0;
            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = player.direction;
            player.heldProj = Projectile.whoAmI;
            if (player.channel)
            {
                Projectile.ai[1]+= 0.02f;

                Power = MathHelper.Clamp(Projectile.ai[1], 0, 1);
                Projectile.timeLeft = 60;
                player.SetDummyItemTime(20);
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile.velocity = player.Center.DirectionTo(Main.MouseWorld);
                    player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
                    Projectile.position.X -= player.direction * 6;
                }
            }
            if(Power == 1 && Main.myPlayer == player.whoAmI && !Notify)
            {
                Notify = true;
                SoundEngine.PlaySound(SoundID.MaxMana);
            }
            else
            {
                if(Projectile.timeLeft == 59)
                {
                    SoundEngine.PlaySound(SoundID.Item75,player.position);
                    for (int i = 0; i < 10; i++)
                    {
                        Dust d= Dust.NewDustDirect(Projectile.Center + new Vector2(24,0).RotatedBy(Projectile.rotation) + new Vector2(0,-8), 0, 0, DustID.FrostStaff);
                        d.velocity *= 1.3f;
                        d.velocity += Projectile.velocity * 3;
                        d.noGravity = true;
                    }
                    //Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center + Projectile.velocity * 40 + new Vector2(0,-4), Projectile.velocity * player.HeldItem.shootSpeed, ModContent.ProjectileType<StasisShot>(), (Projectile.damage * Power) + 1, Projectile.knockBack, Projectile.owner);
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation();

            Vector2 vector = Vector2.Normalize(Projectile.velocity) * 20;
            Projectile.Center = player.Center + new Vector2(vector.X, vector.Y * 1f);
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, new Vector2(player.Center.X + player.direction * -6, player.Center.Y).DirectionTo(Projectile.Center).ToRotation() - MathHelper.PiOver2);
            if (player.channel)
                frame += Power;
            else
                frame += Projectile.timeLeft / 60f;

            if(frame > 2)
            {
                Projectile.frame++;
                frame = 0;
            }
            if (Projectile.frame > 3)
                Projectile.frame = 0;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects Flip = Projectile.direction == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            drawPos.Y += Main.player[Projectile.owner].gfxOffY;
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(0, -4), frame, lightColor, Projectile.rotation, new Vector2(texture.Width, frameHeight) / 2,Projectile.scale, Flip, 0);
            return false;
        }
    }
}
