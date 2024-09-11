using System;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class EnergyLaser : ModProjectile
{
	private static Asset<Texture2D> tex;
	public override void SetStaticDefaults()
    {
        ProjectileID.Sets.DrawScreenCheckFluff[Type] = 4800;
        tex = ModContent.Request<Texture2D>(Texture);
    }
    public override void SetDefaults()
    {
        Projectile.width = 9;
        Projectile.height = 9;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.timeLeft = 20000;
        Projectile.extraUpdates = 600;
        Projectile.alpha = 0;
    }
    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = 0; height = 0;
        return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }
    public override void AI()
    {
        Projectile.ai[2]++;

        if (Projectile.ai[2] == 600)
        {
            Projectile.damage = 0;
            ParticleSystem.AddParticle(new EnergyRevolverParticle(), Projectile.Center, default, new Color(64, 255, 255, 0),0,1,14);
            Projectile.velocity = Vector2.Zero;
            for(int i = 0; i < 10; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(3, 3), 24);
                d.scale *= 2;
                d.noGravity = true;
            }
        }
        if(Projectile.timeLeft % 30 == 0)
            Projectile.alpha += 1;
        if(Projectile.alpha >= 255)
        {
            Projectile.Kill();
        }
        // fix to the laser not rendering if fired near the edges of the world, PROBABLY won't have issues with world size mods but... idk
        if (Projectile.position.X <= 16 || Projectile.position.X >= (Main.maxTilesX - 1) * 16 || Projectile.position.Y <= 16 || Projectile.position.Y >= (Main.maxTilesY - 1) * 16)
        {
            Projectile.velocity = Vector2.Zero;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.damage = (int)(Projectile.damage * 0.9f);
        if (Main.rand.NextBool(6))
            target.AddBuff(BuffID.OnFire3, 60 * 3);
        ParticleSystem.AddParticle(new EnergyRevolverParticle(), Projectile.Center, default, new Color(64, 128, 255, 0), 0, Main.rand.NextFloat(0.9f,1.1f), 14);
        for (int i = 0; i < 10; i++)
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(3, 3), 24);
            d.scale *= 2;
            d.noGravity = true;
        }
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        if (Main.rand.NextBool(6))
            target.AddBuff(BuffID.OnFire3, 60 * 3);
        ParticleSystem.AddParticle(new EnergyRevolverParticle(), Projectile.Center, default, new Color(64, 128, 255, 0), 0, Main.rand.NextFloat(0.9f, 1.1f), 14);
        for (int i = 0; i < 10; i++)
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(3, 3), 24);
            d.scale *= 2;
            d.noGravity = true;
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity = Vector2.Zero;
        return false;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Vector2 StartPos = new Vector2(Projectile.ai[0], Projectile.ai[1]);
        Main.EntitySpriteDraw(tex.Value,Projectile.Center - Main.screenPosition,new Rectangle(0,0,tex.Value.Width,tex.Value.Height),new Color(Projectile.Opacity, Projectile.Opacity, 1f,0),Projectile.Center.DirectionTo(StartPos).ToRotation() + MathHelper.PiOver2,new Vector2(tex.Value.Width / 2f,tex.Value.Height),new Vector2(Projectile.Opacity * 1.3f, Projectile.Center.Distance(StartPos)),SpriteEffects.None);
        return false;
    }
}
