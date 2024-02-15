using System;
using System.Collections.Generic;
using System.IO;
using Avalon.Buffs;
using Avalon.Network;
using Avalon.Network.Handlers;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public class StingerProbeMinion : ModProjectile
{
    private int hostPosition = -1;
    private LinkedListNode<int> positionNode;

    private Random syncedRandom = new();

    private int syncedRandomSeed;

    private int ProjTimer
    {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    private int SyncedRandomSeed
    {
        get => syncedRandomSeed;
        set
        {
            syncedRandomSeed = value;
            syncedRandom = new Random(syncedRandomSeed);
        }
    }

    public override void SetStaticDefaults()
    {
        Main.projPet[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;

        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.light = 0.3f;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;
        Projectile.netImportant = true;
        Projectile.DamageType = DamageClass.Summon;
    }

    public override bool MinionContactDamage()
    {
        return false;
    }

    public override bool? CanCutTiles()
    {
        return false;
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
        SyncedRandomSeed = Main.rand.Next();
        AvalonPlayer summonPlayer = Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>();
        positionNode ??= summonPlayer.HandleStingerProbe();
        writer.Write(positionNode.Value);
        writer.Write(SyncedRandomSeed);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        base.ReceiveExtraAI(reader);
        hostPosition = reader.ReadInt32();
        SyncedRandomSeed = reader.ReadInt32();
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

        for (int i = 0; i < 2; i++)
        {
            int randomSize = syncedRandom.Next(1, 4) / 2;
            int num161 = Gore.NewGore(Projectile.GetSource_FromThis(),
                new Vector2(Projectile.position.X, Projectile.position.Y), default,
                syncedRandom.Next(61, 64));
            Gore gore30 = Main.gore[num161];
            Gore gore40 = gore30;
            gore40.velocity *= 0.3f;
            gore40.scale *= randomSize;
            Main.gore[num161].velocity.X += syncedRandom.Next(-1, 2);
            Main.gore[num161].velocity.Y += syncedRandom.Next(-1, 2);
        }

        Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>().RemoveStingerProbe(positionNode);

        if (Projectile.owner != Main.myPlayer)
        {
            return;
        }

        int bomb = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
            Vector2.Zero, ProjectileID.Grenade, 50, 3f);
        Main.projectile[bomb].timeLeft = 1;

        base.OnKill(timeLeft);
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        AvalonPlayer buffPlayer = player.GetModPlayer<AvalonPlayer>();
        AvalonPlayer summonPlayer = player.GetModPlayer<AvalonPlayer>();

        // Check if should be alive
        if (player.dead || !player.active)
        {
            player.ClearBuff(ModContent.BuffType<StingerProbe>());
            return;
        }

        if (player.HasBuff(ModContent.BuffType<StingerProbe>()))
        {
            Projectile.timeLeft = 2;
        }

        // Get position in circle
        if (hostPosition == -1)
        {
            positionNode ??= summonPlayer.HandleStingerProbe();
        }
        else
        {
            positionNode ??= summonPlayer.ObtainExistingStingerProbe(hostPosition);
        }

        if (player.HasBuff(ModContent.BuffType<StingerProbe>()))
        {
            Projectile.timeLeft = 2;
        }

        #region reflect

        // yoinked from reflex charm
        var projWS = new Rectangle((int)Projectile.Center.X - 32, (int)Projectile.Center.Y - 32, 64, 64);
        foreach (Projectile Pr in Main.projectile)
        {
            if (!Pr.friendly && !Pr.bobber && !Data.Sets.Projectile.DontReflect[Pr.type])
            {
                var proj2 = new Rectangle((int)Pr.position.X, (int)Pr.position.Y, Pr.width, Pr.height);
                bool reflect = false, check = false;

                if (proj2.Intersects(projWS) && !reflect)
                {
                    reflect = true;
                }

                else
                {
                    check = true;
                }

                if (reflect && !check)
                {
                    for (int thingy = 0; thingy < 5; thingy++)
                    {
                        int dust = Dust.NewDust(Pr.position, Pr.width, Pr.height, DustID.MagicMirror, 0f, 0f, 100);
                        Main.dust[dust].noGravity = true;
                    }

                    Pr.hostile = false;
                    Pr.friendly = true;
                    Pr.velocity.X *= -1f;
                    Pr.velocity.Y *= -1f;

                    Projectile.Kill();
                }
            }
        }

        foreach (NPC N in Main.npc)
        {
            if (!N.friendly && N.aiStyle == 9)
            {
                var npc = new Rectangle((int)N.position.X, (int)N.position.Y, N.width, N.height);
                bool reflect = false, check = false;
                int rn = syncedRandom.Next(2);
                if (rn == 0)
                {
                    if (npc.Intersects(projWS) && !reflect)
                    {
                        reflect = true;
                    }
                }
                else
                {
                    check = true;
                }

                if (reflect && !check)
                {
                    for (int varlex = 0; varlex < 5; varlex++)
                    {
                        int dust = Dust.NewDust(N.position, N.width, N.height, DustID.MagicMirror, 0f, 0f, 100);
                        Main.dust[dust].noGravity = true;
                    }

                    N.friendly = true;
                    N.velocity.X *= -1f;
                    N.velocity.Y *= -1f;

                    Projectile.Kill();
                }
            }
        }

        #endregion

        #region movement

        int radius = 75;
        float speed = 0.1f;
        Vector2 target = player.Center +
                         (Vector2.One.RotatedBy(
                             (MathHelper.TwoPi / summonPlayer.StingerProbes.Count * positionNode.Value) +
                             buffPlayer.StingerProbeRotation) * radius);
        Vector2 error = target - Projectile.Center;

        Projectile.velocity = player.velocity + (error * speed);
        //projectile.Center = target;

        #endregion

        #region projectile

        Vector2 dirToCursor =
            (player.GetModPlayer<AvalonPlayer>().MousePosition - Projectile.Center).SafeNormalize(-Vector2.UnitY);
        Projectile.rotation = dirToCursor.ToRotation() + MathHelper.ToRadians(180f);

        if (ProjTimer-- < 0)
        {
            ProjTimer = 0;
        }

        if (player.itemAnimation != 0 && player.HeldItem.damage != 0 &&
            ProjTimer == 0)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                int laser = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                    dirToCursor * 36f, ProjectileID.GreenLaser, Projectile.damage, 0f,
                    Projectile.owner);

                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    ModContent.GetInstance<SyncMouse>().Send(new BasicPlayerNetworkArgs(player));
                    ModContent.GetInstance<SyncStingerProbe>().Send(new BasicPlayerNetworkArgs(player));
                }
            }

            ProjTimer = 120 + syncedRandom.Next(60);
        }

        #endregion
    }
}
