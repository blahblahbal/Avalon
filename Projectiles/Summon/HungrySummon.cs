using System;
using Avalon.Buffs.Minions;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;

namespace Avalon.Projectiles.Summon;

public class HungrySummon : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 6;
        Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
    }
    public override bool? CanCutTiles()
    {
        return false;
    }

    // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
    public override bool MinionContactDamage()
    {
        return true;
    }
    public override void SetDefaults()
    {
        //Projectile.aiStyle = ProjAIStyleID.MiniTwins;
        //AIType = ProjectileID.Spazmamini;
        Projectile.aiStyle = -1;
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 5;
        Projectile.minion = true;
        Projectile.minionSlots = 1;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity= true;
        Projectile.localNPCHitCooldown = 20;
        DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
        DrawOriginOffsetY = -(int)(((dims.Height / Main.projFrames[Projectile.type]) / 2) - (Projectile.Size.Y / 2));
        //Main.projPet[projectile.type] = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        return false;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        if (Projectile.type == ModContent.ProjectileType<HungrySummon>())
        {
            if (Main.player[Projectile.owner].dead)
            {
                player.ClearBuff(ModContent.BuffType<Hungry>());
            }
            if (player.HasBuff(ModContent.BuffType<Hungry>()))
            {
                Projectile.timeLeft = 2;
            }
        }

        if (Projectile.timeLeft != 5 && Main.rand.NextBool(3))
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(10, 0).RotatedBy(Projectile.rotation) + Main.rand.NextVector2Circular(3, 3), DustID.Blood);
            d.velocity = new Vector2(MathHelper.Clamp(Projectile.velocity.Length(), 3, 10), 0).RotatedBy(Projectile.rotation).RotatedByRandom(0.1f);
            d.noGravity = !Main.rand.NextBool(5);
            d.fadeIn = Main.rand.NextFloat(0, 0.5f);
        }

        if (++Projectile.frameCounter >= 5)
        {
            Projectile.frameCounter = 0;
            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }
        }

        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;
        if (Projectile.ai[1] != -1)
        {
            if (!Main.npc[(int)Projectile.ai[1]].active)
            {
                Projectile.ai[2] = 0;
                Projectile.ai[1] = -1;
            }
        }

        Vector2 TargetPos;
        Projectile.ai[0]++;

        bool TargetingEnemy;
        int TargetNPC = ClassExtensions.FindClosestNPC(Projectile, 2000, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);

        if (TargetNPC != -1)
        {
            TargetingEnemy = true;
            TargetPos = Main.npc[TargetNPC].Center;
        }
        else
        {
            TargetingEnemy = false;
            TargetPos = player.Center + new Vector2(0, -50);
        }
        if (player.HasMinionAttackTargetNPC)
        {
            TargetNPC = player.MinionAttackTargetNPC;
            TargetingEnemy = true;
            TargetPos = Main.npc[TargetNPC].Center;
        }
        Projectile.ai[1] = TargetNPC;
        if (Projectile.ai[2] == 0)
        {
            if (Projectile.Center.Distance(TargetPos) > 100 && !TargetingEnemy)
            {
                Projectile.velocity = Vector2.SmoothStep(Projectile.velocity + Projectile.Center.DirectionTo(TargetPos) * 0.1f, Projectile.Center.DirectionTo(TargetPos) * Projectile.velocity.Length(), 0.3f);
            }
            else if (TargetingEnemy)
            {
                Projectile.velocity = Vector2.SmoothStep(Projectile.velocity + Projectile.Center.DirectionTo(TargetPos) * 0.3f, Projectile.Center.DirectionTo(TargetPos) * Projectile.velocity.Length(), 0.3f);
            }
            if (Projectile.position.Distance(player.position) > 5000)
            {
                Projectile.Center = player.Center;
            }

            if (Projectile.velocity.Length() >= 7f)
            {
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 7f;
            }
            if (Projectile.velocity.Length() <= 2)
            {
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 2f;
            }
        }
        else if (Projectile.ai[1] != -1)//Biting
        {
            if (Main.npc[(int)Projectile.ai[1]].active && Main.npc[(int)Projectile.ai[1]].Distance(Projectile.Center) < 100) 
            {
                Dust d2 = Dust.NewDustPerfect(Projectile.Center + new Vector2(2).RotatedBy(Projectile.rotation),DustID.Blood);
                d2.velocity = Projectile.velocity.RotatedByRandom(1) * Main.rand.NextFloat(-6f,-1f);
                d2.noGravity = Main.rand.NextBool();
                d2.fadeIn = 1;

                Projectile.frameCounter += 2;
                //Projectile.position += Main.npc[(int)Projectile.ai[1]].velocity;
                Projectile.Center = Main.npc[TargetNPC].Hitbox.ClosestPointInRect(Projectile.Center + Main.npc[TargetNPC].velocity);
            }
            else
            {
                Projectile.ai[1] = 0;
                Projectile.ai[2] = 0;
            }
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (target.whoAmI == Projectile.ai[1] && Projectile.ai[2] != 1)
        {
            Projectile.ai[2] = 1;
        }
        Projectile.velocity = Projectile.Center.DirectionTo(target.Center);
    }
    public override bool ShouldUpdatePosition()
    {
        return Projectile.ai[2] == 0;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        //for (int npcIndex = 0; npcIndex < 200; npcIndex++)
        //{
        //    NPC npc = Main.npc[npcIndex];
        //    if (Projectile.Hitbox.Intersects(npc.Hitbox) && !npc.friendly)
        //    {
        //        Projectile.position = Projectile.position + npc.velocity;
        //        Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 0.4f;
        //    }
        //}
    }
}
