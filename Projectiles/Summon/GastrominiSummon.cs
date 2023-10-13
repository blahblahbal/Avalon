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
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
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

        var num820 = 0.05f;
        for (var num821 = 0; num821 < 1000; num821++)
        {
            if (num821 != Projectile.whoAmI && Main.projectile[num821].active && Main.projectile[num821].owner == Projectile.owner && Math.Abs(Projectile.position.X - Main.projectile[num821].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num821].position.Y) < Projectile.width)
            {
                if (Projectile.position.X < Main.projectile[num821].position.X)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num820;
                }
                else
                {
                    Projectile.velocity.X = Projectile.velocity.X + num820;
                }
                if (Projectile.position.Y < Main.projectile[num821].position.Y)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num820;
                }
                else
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num820;
                }
            }
        }
        var vector57 = Projectile.position;
        var num822 = 400f;
        var flag32 = false;
        if (Projectile.ai[0] != 1f)
        {
            Projectile.tileCollide = true;
        }
        for (var num823 = 0; num823 < 200; num823++)
        {
            var nPC5 = Main.npc[num823];
            if (nPC5.active && !nPC5.dontTakeDamage && !nPC5.friendly && nPC5.lifeMax > 5)
            {
                var num824 = Vector2.Distance(nPC5.Center, Projectile.Center);
                if (((Vector2.Distance(Projectile.Center, vector57) > num824 && num824 < num822)) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC5.position, nPC5.width, nPC5.height))
                {
                    num822 = num824;
                    vector57 = nPC5.Center;
                    flag32 = true;
                }
            }
        }
        var num825 = 500;
        if (flag32)
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
        if (flag32 && Projectile.ai[0] == 0f)
        {
            var vector58 = vector57 - Projectile.Center;
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
            if (Projectile.frameCounter <= 3)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.frameCounter <= 6)
            {
                Projectile.frame = 1;
            }
        }
        else if (Projectile.localAI[0] > 0)
        {
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
            if (flag32 && Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] += 1f;
                Projectile.localAI[0] = 1;
                if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, vector57, 0, 0))
                {
                    Projectile.spriteDirection = -(int)((vector57.X - Projectile.Center.X) / Math.Abs(vector57.X - Projectile.Center.X));
                    var value24 = vector57 - Projectile.Center;
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
