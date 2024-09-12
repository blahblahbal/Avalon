using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System;
using Avalon.Particles;
using ReLogic.Content;
using Terraria.GameContent;

namespace Avalon.Projectiles.Melee;

public class QuantumBeam : ModProjectile
{
	private static Asset<Texture2D> texture;
	private static Asset<Texture2D> texture2;
	public override void SetStaticDefaults()
	{
		texture = TextureAssets.Projectile[Type];
		texture2 = ModContent.Request<Texture2D>(Texture + "2");
	}
	public override void SetDefaults()
    {
        Projectile.width = 25;
        Projectile.height = 25;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.timeLeft = 300;
        Projectile.scale = 1f;
        Projectile.tileCollide = false;
        Projectile.penetrate = 5;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        /*
        Color[] CoolColors = {
                new Color(255, 0, 255, 0),
                new Color(128, 0, 255, 0),
                new Color(128, 0, 128, 0),
                new Color(255, 0, 128, 0),
            };
        int numColors = CoolColors.Length;
        float fade = (Main.GameUpdateCount % 25) / 60f;
        int index = (int)((Main.GameUpdateCount / 25) % numColors);
        int nextIndex = (index + 1) % numColors;

        return Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade);
        */
        return Color.Black;
    }
    public override bool? CanHitNPC(NPC target)
    {
        if (Projectile.ai[1] >= 0)
            return base.CanHitNPC(target);
        else
            return false;
    }
    public override bool CanHitPvp(Player target)
    {
        if (Projectile.ai[1] >= 0)
            return base.CanHitPvp(target);
        else
            return false;
    }
    public override bool ShouldUpdatePosition()
    {
        if (Projectile.ai[1] >= 0)
            return base.ShouldUpdatePosition();
        else
            return false;
    }
    public override void AI()
    {
        Projectile.ai[1]++;
        if (Projectile.ai[1] < -1 && Projectile.ai[2] == 0)
        {
            Projectile.ai[2]++;
            ParticleSystem.AddParticle(new QuantumPortal(), Projectile.Center, default, default);
        }
        if (Projectile.ai[1] == -1)
        {
            int NPC = ClassExtensions.FindClosestNPC(Projectile, 400, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly || !Collision.CanHit(npc, Projectile));
            float speed = Projectile.velocity.Length();
            if(NPC != -1)
            {
                Projectile.velocity = Projectile.Center.DirectionTo(Main.npc[NPC].Center) * speed;
            }

            for (int j = 0; j < 20; j++)
            {
                int DustType = DustID.CorruptTorch;
                if (Main.rand.NextBool())
                    DustType = DustID.HallowedTorch;

                Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustType);
                D.noGravity = true;
                D.fadeIn = Main.rand.NextFloat(0, 1);
                D.velocity = Vector2.Normalize(Projectile.velocity).RotatedByRandom(0.3f) * Main.rand.NextFloat(1, 6);
            }
            //Projectile.extraUpdates++;
            Projectile.penetrate = 1;
        }
        if (Projectile.ai[1] > 40)
        {
            Projectile.velocity *= 0.95f;
        }
        if (Projectile.ai[1] > 60)
        {
            Projectile.Kill();
        }
        if (Projectile.ai[1] >= 0)
        {
            int DustType = DustID.CorruptTorch;
            if (Main.rand.NextBool())
                DustType = DustID.HallowedTorch;

            if (Main.rand.NextBool(2))
            {
                Dust CoolDust1 = Dust.NewDustDirect(Projectile.position + Vector2.Normalize(Projectile.velocity) * 30, Projectile.width, Projectile.height, DustType);
                CoolDust1.noGravity = true;
                CoolDust1.velocity = Projectile.velocity;
                CoolDust1.fadeIn = 1;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            Projectile.alpha = (int)(Projectile.alpha * 0.86f);

            if (!Projectile.tileCollide && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.tileCollide = true;
            }
        }
    }

    public SoundStyle StarSoundReal = new SoundStyle("Terraria/Sounds/Item_9")
    {
        Volume = 0.6f,
        Pitch = -1f,
        PitchVariance = 0.1f,
        MaxInstances = 10,
    };

    public SoundStyle Impac = new SoundStyle("Terraria/Sounds/Item_72")
    {
        Volume = 0.6f,
        MaxInstances = 10,
    };
    public override void OnSpawn(IEntitySource source)
    {
        SoundEngine.PlaySound(StarSoundReal, Projectile.position);
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i <= 20; i++)
        {
            int DustType = DustID.CorruptTorch;
            if (Main.rand.NextBool())
                DustType = DustID.HallowedTorch;

            Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustType);
            D.noGravity = !Main.rand.NextBool(3);
            if(D.noGravity)
                D.fadeIn = Main.rand.NextFloat(1, 2);
            D.velocity = Main.rand.NextVector2Circular(4,4);
        }
        SoundEngine.PlaySound(Impac, Projectile.position);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.ShadowFlame, 300);

        if (hit.Crit)
        {
            Vector2 SwordSpawn = Projectile.Center + Main.rand.NextVector2Circular(300, 300);
            //ParticleSystem.AddParticle(new QuantumPortal(), SwordSpawn, default, default);
            Projectile P = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), SwordSpawn, SwordSpawn.DirectionTo(target.Center) * (Projectile.velocity.Length() * Main.rand.NextFloat(1.1f,1.3f)), ModContent.ProjectileType<Projectiles.Melee.QuantumBeam>(), (int)(Projectile.damage * 0.6f), Projectile.knockBack, Projectile.owner, 0, Main.rand.Next(-20, -10));
        }
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.ShadowFlame, 300);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Main.spriteBatch.End();
        BlendState BlendS = new BlendState
        {
            ColorBlendFunction = BlendFunction.ReverseSubtract,
            ColorDestinationBlend = Blend.One,
            ColorSourceBlend = Blend.SourceAlpha,
            AlphaBlendFunction = BlendFunction.ReverseSubtract,
            AlphaDestinationBlend = Blend.One,
            AlphaSourceBlend = Blend.SourceAlpha
        };
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendS, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 offset = new Vector2((Projectile.width / 1) - frameOrigin.X, Projectile.height - frame.Height);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Main.EntitySpriteDraw(texture.Value, drawPos, frame, Color.Lerp(new Color(64,255,64), new Color(128, 255, 64), Main.masterColor) * Projectile.Opacity, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);

        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

        Main.EntitySpriteDraw(texture2.Value, drawPos, frame, Color.Lerp(new Color(255,64,255),new Color(128,64,255),Main.masterColor) * Projectile.Opacity * 0.4f, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
        Main.EntitySpriteDraw(texture2.Value, drawPos, frame, new Color(255, 255, 255, 0) * Projectile.Opacity * 0.2f, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);

        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
        return false;
    }
}

//public class QuantumBeam2 : QuantumBeam
//{
//    public override void AI()
//    {
//        Color[] CoolColors = {
//                new Color(255, 0, 255, 0),
//                new Color(128, 0, 255, 0),
//                new Color(128, 0, 128, 0),
//                new Color(255, 0, 128, 0),
//            };
//        int numColors = CoolColors.Length;
//        float fade = (Main.GameUpdateCount % 25) / 25f;
//        int index = (int)((Main.GameUpdateCount / 25) % numColors);
//        int nextIndex = (index + 1) % numColors;
//        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
//        Vector2 PartingofthePurpleSea1 = new Vector2(3).RotatedBy(Projectile.rotation);
//        Vector2 PartingofthePurpleSea2 = new Vector2(-3).RotatedBy(Projectile.rotation);
//        if (Main.rand.NextBool(10))
//        {
//            int CoolDust1 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0, 0, DustID.FireworksRGB, PartingofthePurpleSea1.X, PartingofthePurpleSea1.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
//            Main.dust[CoolDust1].noGravity = true;
//            int CoolDust2 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0, 0, DustID.FireworksRGB, PartingofthePurpleSea2.X, PartingofthePurpleSea2.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
//            Main.dust[CoolDust2].noGravity = true;
//        }
//        Projectile.alpha = (int)(Projectile.alpha * 0.95f);
//        if (!Projectile.tileCollide && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
//        {
//            Projectile.tileCollide = true;
//        }
//    }
//    public override void SetDefaults()
//    {
//        Projectile.width = 25;
//        Projectile.height = 25;
//        Projectile.aiStyle = -1;
//        Projectile.DamageType = DamageClass.Melee;
//        Projectile.alpha = 255;
//        Projectile.friendly = true;
//        Projectile.timeLeft = 300;
//        Projectile.scale = 0.8f;
//        Projectile.tileCollide = false;
//    }
//}
