using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon
{
    public class AnchorWorm : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.timeLeft = 60 * 7;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if(target.whoAmI == Projectile.ai[0])
                return base.CanHitNPC(target);
            else
                return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override void AI()
        {
            NPC target = Main.npc[(int)Projectile.ai[0]];

            if (!target.active)
                Projectile.Kill();
            Projectile.Center = target.Hitbox.ClosestPointInRect((Vector2.Normalize(Projectile.velocity) * MathHelper.Max(target.width,target.height)) + target.Center);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}
