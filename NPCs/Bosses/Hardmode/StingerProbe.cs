using System;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode;

public class StingerProbe : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 1;
        NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            Hide = true // Hides this NPC from the bestiary
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, bestiaryData);
    }
    public override void SetDefaults()
    {
        NPC.npcSlots = 1;
        NPC.width = 46;
        NPC.height = 32;
        NPC.aiStyle = -1;
        NPC.timeLeft = 750;
        NPC.damage = 55;
        NPC.defense = 15;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        NPC.lifeMax = 300;
        NPC.scale = 1;
        NPC.knockBackResist = 0.1f;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.friendly = false;

    }
    //public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    //{
    //    NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
    //    NPC.damage = (int)(NPC.damage * 0.65f);
    //}
    public override void AI()
    {
        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest(true);
		}

		Player player = NPC.PlayerTarget();

		int closest = AvalonGlobalProjectile.FindClosest(NPC.position, 16 * 20, false);
		if (closest != -1)
		{
			Projectile targ = Main.projectile[closest];
			NPC.velocity = Vector2.Normalize(targ.Center - NPC.Center) * 20f;
		}



		float num146 = 6f;
        float num147 = 0.05f;
		Vector2 NPCCenter = NPC.Center;
		Vector2 targetPlayerCenter = NPC.PlayerTarget().Center;

        //float targetPlayerCenter.X = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
        //float targetPlayerCenter.Y = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;
        targetPlayerCenter.X = (int)(targetPlayerCenter.X / 8f) * 8;
		targetPlayerCenter.Y = (int)(targetPlayerCenter.Y / 8f) * 8;
        NPCCenter.X = (int)(NPCCenter.X / 8f) * 8;
        NPCCenter.Y = (int)(NPCCenter.Y / 8f) * 8;


		targetPlayerCenter.X -= NPCCenter.X;
		targetPlayerCenter.Y -= NPCCenter.Y;
        float num151 = (float)Math.Sqrt((double)(targetPlayerCenter.X * targetPlayerCenter.X + targetPlayerCenter.Y * targetPlayerCenter.Y));
        float num152 = num151;
        if (num151 == 0f)
        {
            targetPlayerCenter.X = NPC.velocity.X;
            targetPlayerCenter.Y = NPC.velocity.Y;
        }
        else
        {
            num151 = num146 / num151;
            targetPlayerCenter.X *= num151;
            targetPlayerCenter.Y *= num151;
        }
        if (Main.player[NPC.target].dead)
        {
            targetPlayerCenter.X = NPC.direction * num146 / 2f;
            targetPlayerCenter.Y = -num146 / 2f;
        }
        if (NPC.velocity.X < targetPlayerCenter.X)
        {
            NPC.velocity.X = NPC.velocity.X + num147;
        }
        else if (NPC.velocity.X > targetPlayerCenter.X)
        {
            NPC.velocity.X = NPC.velocity.X - num147;
        }
        if (NPC.velocity.Y < targetPlayerCenter.Y)
        {
            NPC.velocity.Y = NPC.velocity.Y + num147;
        }
        else if (NPC.velocity.Y > targetPlayerCenter.Y)
        {
            NPC.velocity.Y = NPC.velocity.Y - num147;
        }
        //if (NPC.type == ModContent.NPCType<StingerProbe>())
        {
            NPC.localAI[0]++;
            if (NPC.justHit)
            {
                NPC.localAI[0] = 0f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[0] >= 120f)
            {
                NPC.localAI[0] = 0f;
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    //Player player = Main.player[NPC.target];
                    Vector2 position = NPC.Center;
                    Vector2 targetPosition = player.Center;
                    Vector2 direction = targetPosition - position;
                    Vector2 perturbedSpeed = direction * 2f;


                    int num153 = 55;
                    int num154 = ModContent.ProjectileType<Projectiles.Hostile.Mechasting.Mechastinger>();
                    int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPCCenter.X, NPCCenter.Y, perturbedSpeed.X, perturbedSpeed.Y, num154, num153, 0f, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].friendly = false;
                    Main.projectile[proj].tileCollide = true;
                    //if (type == 480) Main.projectile[proj].notReflect = true;
                }
            }
            int num155 = (int)NPC.position.X + NPC.width / 2;
            int num156 = (int)NPC.position.Y + NPC.height / 2;
            num155 /= 16;
            num156 /= 16;
            if (!WorldGen.SolidTile(num155, num156))
            {
                Lighting.AddLight((int)((NPC.position.X + NPC.width / 2) / 16f), (int)((NPC.position.Y + NPC.height / 2) / 16f), 0.3f, 0.1f, 0.05f);
            }
            if (targetPlayerCenter.X > 0f)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = (float)Math.Atan2((double)targetPlayerCenter.Y, (double)targetPlayerCenter.X);
            }
            if (targetPlayerCenter.X < 0f)
            {
                NPC.spriteDirection = -1;
                NPC.rotation = (float)Math.Atan2((double)targetPlayerCenter.Y, (double)targetPlayerCenter.X) + 3.14f;
            }
        }
    }
	private Vector2 GetRandomDirectionInCone(Vector2 baseDirection, float coneAngle)
	{
		// Get a random angle within the cone range
		float randomAngle = Main.rand.NextFloat(-coneAngle / 2f, coneAngle / 2f);

		// Rotate the base direction by the random angle
		return baseDirection.RotatedBy(randomAngle);
	}
}

