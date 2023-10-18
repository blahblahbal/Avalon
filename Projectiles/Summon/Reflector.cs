using System.Collections.Generic;
using System.IO;
using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Network;
using Avalon.Network.Handlers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public class Reflector : ModProjectile
{
    private int hostPosition = -1;
    private LinkedListNode<int> positionNode;
    private int deactivateTimer;
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 20;
    }
    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 36;
        Projectile.netImportant = true;
        Projectile.friendly = true;
        Projectile.ignoreWater = true;
        Projectile.minionSlots = 1f;
        Projectile.timeLeft = 18000;
        Projectile.penetrate = -1;
        Projectile.scale = 0.9f;
        Projectile.timeLeft *= 5;
        Projectile.damage = 0;
        Projectile.minion = true;
        Projectile.tileCollide = false;
        deactivateTimer = 300;
    }
    public override bool? CanCutTiles()
    {
        return false;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        if (deactivateTimer < 300)
        {
            return new Color(100, 100, 100, 255);
        }
        return base.GetAlpha(lightColor);
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        AvalonPlayer summonPlayer = Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>();
        positionNode ??= summonPlayer.HandleReflectorSummon();
        writer.Write(positionNode.Value);
        writer.Write(deactivateTimer);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        base.ReceiveExtraAI(reader);
        hostPosition = reader.ReadInt32();
        deactivateTimer = reader.ReadInt32();
    }

    public override void OnKill(int timeLeft)
    {
        Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>().RemoveReflectorSummon(positionNode);
        base.OnKill(timeLeft);
    }
    public override void AI()
    {
        Projectile.frameCounter++;
        if (deactivateTimer < 300)
        {
            Projectile.frame = 10;
        }
        else if (Main.player[Projectile.owner].ownedProjectileCounts[Type] == 1)
        {
            if (Projectile.ai[2] -60 < - 55)
            {
                Projectile.frame = 3;
            }
            else if (Projectile.ai[2] - 60 < -40)
            {
                Projectile.frame = 2;
            }
            else if (Projectile.ai[2] - 60 < -20)
            {
                Projectile.frame = 1;
            }
            else if (Projectile.ai[2] - 60 < 0)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.ai[2] - 60 < 20)
            {
                Projectile.frame = 19;
            }
            else if (Projectile.ai[2] - 60 < 40)
            {
                Projectile.frame = 18;
            }
            else if (Projectile.ai[2] - 60 < 55)
            {
                Projectile.frame = 17;
            }
            else if (Projectile.ai[2] - 60 < 60)
            {
                Projectile.frame = 16;
            }
        }
        else
        {
            if (Projectile.frameCounter > 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 19)
            {
                Projectile.frame = 0;
            }
            if (Projectile.frame < 1)
            {
                Projectile.frame = 0;
            }
        }
        Projectile.damage = 0;
        Player player = Main.player[Projectile.owner];
        AvalonPlayer modPlayer = player.GetModPlayer<AvalonPlayer>();

        // Check if should be alive
        if (player.dead || !player.active)
        {
            player.ClearBuff(ModContent.BuffType<Buffs.Minions.Reflector>());
            return;
        }

        Projectile.ai[0]++;
        if (Projectile.owner == Main.myPlayer && Main.netMode != NetmodeID.SinglePlayer && Projectile.ai[0] % 300 == 1)
        {
            Projectile.netUpdate = true;
            ModContent.GetInstance<SyncReflectorStaff>().Send(new BasicPlayerNetworkArgs(player));
        }

        if (player.HasBuff(ModContent.BuffType<Buffs.Minions.Reflector>()))
        {
            Projectile.timeLeft = 2;
        }

        // Get position in circle
        if (hostPosition == -1)
        {
            positionNode ??= modPlayer.HandleReflectorSummon();
        }
        else
        {
            positionNode ??= modPlayer.ObtainExistingReflectorSummon(hostPosition);
        }
        deactivateTimer++;
        int closest = AvalonGlobalProjectile.FindClosestHostile(Projectile.Center, 540f); // 240
        if (closest != -1 && deactivateTimer >= 300)
        {
            Projectile targ = Main.projectile[closest];
            Projectile.velocity = Vector2.Normalize(targ.Center - Projectile.Center) * 8f;
            if (Vector2.Distance(Projectile.Center, targ.Center) < 160)
            {
                Rectangle proj = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                Rectangle targetProj = new Rectangle((int)targ.position.X, (int)targ.position.Y, targ.width, targ.height);
                if (proj.Intersects(targetProj) && !targ.bobber && !Data.Sets.Projectile.DontReflect[targ.type])
                {
                    targ.hostile = false;
                    targ.friendly = true;
                    targ.damage *= 3; // (int)MathHelper.Clamp(targ.damage, targ.damage * 3, 150);
                    targ.velocity *= -1f;
                    deactivateTimer = 0;
                }
            }
        }
        else if (Main.player[Projectile.owner].ownedProjectileCounts[Type] == 1)
        {
            if (Projectile.ai[1] == 0)
            {
                if (Projectile.ai[2] < 10 || Projectile.ai[2] > 110)
                {
                    Projectile.ai[2]++;
                }
                else
                {
                    Projectile.ai[2] += 2f;
                }
            }    
            else if (Projectile.ai[1] == 1)
            {
                if (Projectile.ai[2] < 10 || Projectile.ai[2] > 110)
                {
                    Projectile.ai[2]--;
                }
                else
                {
                    Projectile.ai[2] -= 2f;
                }
            }
            if (Projectile.ai[2] == 120)
            {
                Projectile.ai[1] = 1;
            }
            if (Projectile.ai[2] == 0)
            {
                Projectile.ai[1] = 0;
            }
            Projectile.Center = player.position - new Vector2(Projectile.ai[2] - 60, 80);
        }
        else
        {
            const int radius = 60;
            const float speed = 0.1f;
            int count = modPlayer.Reflectors.Count;
            Vector2 point = new Vector2(player.Center.X - 10, player.Center.Y - 30);
            Vector2 target = point +
                             (Vector2.One.RotatedBy(
                                 (MathHelper.TwoPi / count * positionNode.Value) +
                                 modPlayer.ReflectorStaffRotation) * radius);
            Vector2 error = target - Projectile.Center;

            Projectile.velocity = player.velocity + (error * speed);
        }
    }
}
