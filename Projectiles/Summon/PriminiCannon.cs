using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public class PriminiCannon : ModProjectile
{
    int scaleSize1;
    int scaleSize2;
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        Main.projPet[Projectile.type] = true;
        Main.projFrames[Projectile.type] = 3;
    }
    public override bool MinionContactDamage()
    {
        return false;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = 42;
        Projectile.height = 42;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.timeLeft *= 5;
        Projectile.minion = true;
        Projectile.minionSlots = 0f;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        Main.projPet[Projectile.type] = true;
        scaleSize1 = (int)(Projectile.width * 1.35f);
        scaleSize2 = (int)(Projectile.height * 1.7f);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        var tex = TextureAssets.Projectile[Type].Value;
        Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition,
            new Rectangle(0, tex.Height / 3 * Projectile.frame, tex.Width, tex.Height / 3),
            lightColor, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 6),
            Projectile.scale, SpriteEffects.None);
        return false;
    }
    public override void AI()
    {
        
        Player owner = Main.player[Projectile.owner];

        if (Projectile.type == ModContent.ProjectileType<PriminiCannon>())
        {
            if (owner.dead)
            {
                owner.GetModPlayer<AvalonPlayer>().PrimeMinion = false;
            }
            if (owner.GetModPlayer<AvalonPlayer>().PrimeMinion)
            {
                Projectile.timeLeft = 2;
            }
        }
        AvalonGlobalProjectile.ModifyProjectileStats(Projectile, ModContent.ProjectileType<PrimeArmsCounter>(), 50, 3, 1f, 0.1f);

        if (Projectile.frame == 1)
        {
            Projectile.Hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, scaleSize1, scaleSize1);
        }
        if (Projectile.frame == 2)
        {
            Projectile.Hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, scaleSize2, scaleSize2);
        }

        if (Projectile.position.Y > Main.player[Projectile.owner].Center.Y - Main.rand.Next(60, 80) - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
        {
            if (Projectile.velocity.Y > 0f)
            {
                Projectile.velocity.Y *= 0.96f;
            }
            Projectile.velocity.Y -= 0.3f;
            if (Projectile.velocity.Y > 6f)
            {
                Projectile.velocity.Y = 6f;
            }
        }
        else if (Projectile.position.Y < Main.player[Projectile.owner].Center.Y - Main.rand.Next(60, 80) - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
        {
            if (Projectile.velocity.Y < 0f)
            {
                Projectile.velocity.Y *= 0.96f;
            }
            Projectile.velocity.Y += 0.2f;
            if (Projectile.velocity.Y < -6f)
            {
                Projectile.velocity.Y = -6f;
            }
        }
        if (Projectile.Center.X > Main.player[Projectile.owner].Center.X - Main.rand.Next(45, 65) - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
        {
            if (Projectile.velocity.X > 0f)
            {
                Projectile.velocity.X *= 0.94f;
            }
            Projectile.velocity.X -= 0.3f;
            if (Projectile.velocity.X > 9f)
            {
                Projectile.velocity.X = 9f;
            }
        }
        if (Projectile.Center.X < Main.player[Projectile.owner].Center.X - Main.rand.Next(45, 65) - Projectile.OwnerProjCounts(ModContent.ProjectileType<PrimeArmsCounter>()) * 2)
        {
            if (Projectile.velocity.X < 0f)
            {
                Projectile.velocity.X *= 0.94f;
            }
            Projectile.velocity.X += 0.2f;
            if (Projectile.velocity.X < -8f)
            {
                Projectile.velocity.X = -8f;
            }
        }
        Projectile.ai[0]++;
        var num954 = Projectile.FindClosestNPC(640, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
        if (num954 == -1)
        {
            Projectile.rotation = 0.78539816f; // -
            return;
        }
        Projectile.rotation = Vector2.Normalize(Main.npc[num954].Center - Projectile.Center).ToRotation() + MathHelper.Pi;
        if (Projectile.ai[0] > 240f && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num954].position, Main.npc[num954].width, Main.npc[num954].height))
        {
            var num955 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, 0f, 0f, ProjectileID.Grenade, 80, 4.5f, Projectile.owner, 0f, 0f);
            Main.projectile[num955].velocity = Vector2.Normalize(Main.npc[num954].Center - Projectile.Center) * new Vector2(12f, 1f);
            Main.projectile[num955].timeLeft = 100;
            Main.projectile[num955].friendly = true;
            Main.projectile[num955].hostile = false;
            Projectile.ai[0] = 0f;
            return;
        }
    }
}
