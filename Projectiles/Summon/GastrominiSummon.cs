using System;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public abstract class GastrominiSummon : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 5;
        Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
        //ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = 22;
        Projectile.height = 36;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 2;
        Projectile.minion = true;
        Projectile.minionSlots = 1f;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        Projectile.aiStyle = -1;
    }
    public override bool? CanCutTiles()
    {
        return false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        return false;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        if (Main.player[Projectile.owner].dead)
        {
            player.ClearBuff(ModContent.BuffType<Buffs.Minions.Gastropod>());
        }
        if (player.HasBuff(ModContent.BuffType<Buffs.Minions.Gastropod>()) && Projectile.timeLeft <= 2)
        {
            Projectile.timeLeft = 2;
        }

        float idleAccel = 0.05f;
        for (var k = 0; k < Main.maxProjectiles; k++)
        {
            if (k != Projectile.whoAmI && Main.projectile[k].active && Main.projectile[k].owner == Projectile.owner && Math.Abs(Projectile.position.X - Main.projectile[k].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[k].position.Y) < Projectile.width)
            {
                if (Projectile.position.X < Main.projectile[k].position.X)
                {
                    Projectile.velocity.X = Projectile.velocity.X - idleAccel;
                }
                else
                {
                    Projectile.velocity.X = Projectile.velocity.X + idleAccel;
                }
                if (Projectile.position.Y < Main.projectile[k].position.Y)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - idleAccel;
                }
                else
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + idleAccel;
                }
            }
        }
        Vector2 targetPos = Projectile.position;
        float targetDist = 400f;
        bool target = false;
        if (Projectile.ai[0] != 1f)
        {
            Projectile.tileCollide = true;
        }
		bool cannotReachPlayerTarget = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
			{
				targetDist = Vector2.Distance(Projectile.Center, targetPos);
				targetPos = npc.Center;
				target = true;
			}
			else
			{
				cannotReachPlayerTarget = true;
			}
		}
		if (!player.HasMinionAttackTargetNPC || cannotReachPlayerTarget)
		{
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy(this, false))
				{
					float distance = Vector2.Distance(npc.Center, Projectile.Center);
					if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
					{
						targetDist = distance;
						targetPos = npc.Center;
						target = true;
					}
				}
			}
		}
        var num825 = 500;
        if (target)
        {
            num825 = 1000;
        }
        var player2 = Main.player[Projectile.owner];
        if (Vector2.Distance(player2.Center, Projectile.Center) > num825)
        {
            Projectile.ai[0] = 1f;
            Projectile.tileCollide = false;
            Projectile.netUpdate = true;
        }
        if (target && Projectile.ai[0] == 0f)
        {
            var vector58 = targetPos - Projectile.Center;
            var num826 = vector58.Length();
            vector58.Normalize();
            if (num826 > 200f)
            {
                var scaleFactor5 = 6f;
                vector58 *= scaleFactor5;
                Projectile.velocity = (Projectile.velocity * 40f + vector58) / 41f;
            }
            else
            {
                var num827 = 4f;
                vector58 *= -num827;
                Projectile.velocity = (Projectile.velocity * 40f + vector58) / 41f;
            }
        }
        else
        {
            var num828 = 6f;
            if (Projectile.ai[0] == 1f)
            {
                num828 = 15f;
            }
            var value22 = Projectile.Center;
            var vector59 = player2.Center - value22 + new Vector2(0f, -60f);
            var num829 = vector59.Length();
            if (num829 > 200f && num828 < 8f)
            {
                num828 = 8f;
            }
            if (num829 < 150f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.ai[0] = 0f;
                Projectile.netUpdate = true;
            }
            if (num829 > 2000f)
            {
                Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
                Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
                Projectile.netUpdate = true;
            }
            if (num829 > 70f)
            {
                vector59.Normalize();
                vector59 *= num828;
                Projectile.velocity = (Projectile.velocity * 40f + vector59) / 41f;
            }
            else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
            {
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.05f;
            }
        }
        if (Projectile.localAI[0] == 0)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter <= 20)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.frameCounter <= 40)
            {
                Projectile.frame = 1;
            }
			else
			{
				Projectile.frame = 0;
				Projectile.frameCounter = 0;
			}
			Projectile.spriteDirection = (Math.Abs(Projectile.velocity.X) > 2.25f || (!target && Projectile.Center.Distance(player.Center) > 300f)) ? -Math.Sign(Projectile.velocity.X) : Projectile.spriteDirection;
		}
        else if (Projectile.localAI[0] > 0)
		{
			Projectile.frameCounter = 0;
			Projectile.localAI[0]++;
            if (Projectile.localAI[0] <= 4)
            {
                Projectile.frame = 2;
            }
            else if (Projectile.localAI[0] <= 8)
            {
                Projectile.frame = 3;
            }
            else if (Projectile.localAI[0] <= 12)
            {
                Projectile.frame = 4;
            }
            else
            {
                Projectile.frame = 0;
                Projectile.localAI[0] = 0;
            }
        }
        if (Projectile.ai[1] > 0f)
        {
            Projectile.ai[1] += Main.rand.Next(1, 4);
        }
        if (Projectile.ai[1] > 90f)
        {
            Projectile.ai[1] = 0f;
            Projectile.netUpdate = true;
        }
        if (Projectile.ai[0] == 0f)
        {
            var scaleFactor7 = 8f;
            int num832 = ProjectileID.MiniRetinaLaser;
            if (target && Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] += 1f;
                Projectile.localAI[0] = 1;
				Projectile.frameCounter = 0;
				if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, targetPos, 0, 0))
                {
                    Projectile.spriteDirection = -(int)((targetPos.X - Projectile.Center.X) / Math.Abs(targetPos.X - Projectile.Center.X));
                    var value24 = targetPos - Projectile.Center;
                    value24.Normalize();
                    value24 *= scaleFactor7;
                    var num833 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value24.X, value24.Y, num832, (int)(Projectile.damage * 1.5f), 0f, Main.myPlayer, 0f, 0f);
                    Main.projectile[num833].timeLeft = 300;
                    Projectile.netUpdate = true;
                }
            }
        }
    }
}


public class GastrominiSummon0 : GastrominiSummon { }
public class GastrominiSummon1 : GastrominiSummon { }
public class GastrominiSummon2 : GastrominiSummon { }
public class GastrominiSummon3 : GastrominiSummon { }
