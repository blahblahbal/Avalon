using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class CrystalUnityShard : ModProjectile
{
    const int amber = 0;
    const int amethyst = 1;
    const int diamond = 2;
    const int emerald = 3;
    const int peridot = 4;
    const int ruby = 5;
    const int sapphire = 6;
    const int topaz = 7;
    const int tourmaline = 8;
    const int zircon = 9;
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 10;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(200,200,200,128);
    }
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.scale = 1f;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 3600;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.timeLeft = 60 * 3;
    }
    int GemType = 1;

    int[] DustIds = { DustID.AmberBolt, DustID.GemAmethyst, DustID.GemDiamond, DustID.GemEmerald, ModContent.DustType<PeridotDust>(), DustID.GemRuby, DustID.GemSapphire, DustID.GemTopaz, ModContent.DustType<TourmalineDust>(), ModContent.DustType<ZirconDust>() };
    Color[] Colors = { Color.OrangeRed, Color.Purple, Color.White, Color.MediumSeaGreen, Color.GreenYellow, Color.Red, Color.Blue, Color.Orange, Color.Cyan, Color.RosyBrown };
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 12; i++)
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustIds[GemType], Main.rand.NextVector2Circular(5, 5));
            d.noGravity = true;
            //d.velocity += Projectile.velocity;
        }
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        ParticleSystem.AddParticle(new ColorableSparkle(), Projectile.Center, default, new Color(Colors[GemType].R, Colors[GemType].G, Colors[GemType].B, 0));
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        if (Projectile.ai[1] == 0f)
        {
            GemType = (int)Projectile.ai[2];

            if (GemType == diamond)
            {
                Projectile.penetrate = 4;
            }
        }

        Projectile.ai[1]++;
        Projectile.frame = GemType;
        if (Projectile.ai[1] > 1)
        {
            for (var i = 0; i < 3; i++)
            {
                var dust = Dust.NewDustPerfect(Vector2.Lerp(Projectile.position, Projectile.oldPosition, i / 2f) + Projectile.Size / 2 + Vector2.Normalize(Projectile.velocity) * 3, DustIds[GemType], Projectile.velocity, 50, default, 1f);
                dust.frame.Y = 10;
                if (dust.type == DustID.AmberBolt)
                {
                    dust.frame.Y += 60;
                }
                dust.scale = (float)Math.Sin(Projectile.ai[1] * 0.2f) / 5 + 1;

                dust.scale *= MathHelper.Clamp(Projectile.ai[1] * 0.1f,0,1);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
            }
        }
        Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.ai[0] * 0.01f);
        Projectile.ai[0] *= 0.99f;
        //Lighting.AddLight(new Vector2((int)((Projectile.position.X + Projectile.width / 2) / 16f), (int)((Projectile.position.Y + Projectile.height / 2) / 16f)), new Color(252, 193, 45).ToVector3());
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        switch (GemType)
        {
            case amber:
                target.AddBuff(BuffID.OnFire, 60 * 5);
                break;
            case amethyst:
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.AmethystShardBuff>(), 60 * 5);
                break;
            case emerald:
                target.AddBuff(BuffID.Midas, 60 * 10);
                break;
            case peridot:
                target.AddBuff(BuffID.Poisoned, 60 * 5);
                break;
            case ruby:
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.RubyShardBuff>(), 60 * 5);
                break;
            case sapphire:
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.SapphireShardBuff>(), 60 * 5);
                break;
            case topaz:
                target.AddBuff(BuffID.Ichor, 60 * 4);
                break;
        }
    }
    
    public override void OnHitPlayer(Player target, Player.HurtInfo info) {
        if (!info.PvP) {
            return;
        }
        switch (GemType)
        {
            case amber:
                target.AddBuff(BuffID.OnFire, 60 * 5);
                break;
            case peridot:
                target.AddBuff(BuffID.Poisoned, 60 * 5);
                break;
            case ruby:
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.RubyShardBuff>(), 60 * 5);
                break;
            case sapphire:
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.SapphireShardBuff>(), 60 * 5);
                break;
            case topaz:
                target.AddBuff(BuffID.Ichor, 60 * 4);
                break;
        }
    }
}
