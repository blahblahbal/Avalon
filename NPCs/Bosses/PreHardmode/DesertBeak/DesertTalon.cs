using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode.DesertBeak
{
    public class DesertTalon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 6;
            NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers() {
				Hide = true // Hides this NPC from the bestiary
			};
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, bestiaryData);
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Vulture);
            NPC.lifeMax = (int)(NPC.lifeMax * 0.9f);
            AnimationType = NPCID.Vulture;
            NPC.noTileCollide = true;
            NPC.aiStyle = NPCAIStyleID.DemonEye;
            NPC.HitSound = new SoundStyle("Terraria/Sounds/NPC_Hit_28") { Pitch = 0.16f, Volume = 0.5f };
            NPC.DeathSound = new SoundStyle("Terraria/Sounds/NPC_Killed_31") { Pitch = 0.16f, Volume = 0.5f };
        }

        public override bool? CanFallThroughPlatforms()
        {
            return true;
        }
        public override void AI()
        {
            if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                NPC.noTileCollide = false;
            }
            if (Main.player[NPC.target].dead)
                NPC.TargetClosest();
            if (Main.player[NPC.target].dead)
                NPC.ai[3] = 1;

            if (NPC.ai[3] != 0)
            {
                NPC.velocity.Y -= 0.5f;
                NPC.alpha++;
                if(NPC.alpha > 255)
                    NPC.active = false;
            }

                NPC.ai[2]++;

            if (NPC.ai[2] > 200)
            {
                NPC.velocity *= 0.9f;
            }
            if (NPC.ai[2] > 240 && !Main.player[NPC.target].dead)
            {
                NPC.velocity = NPC.Center.DirectionTo(Main.player[NPC.target].Center) * 12;
                NPC.ai[2] = Main.rand.Next(-100, 100);
                NPC.netUpdate= true;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int num507 = 0; (double)num507 < hit.Damage / (double)NPC.lifeMax * 100.0; num507++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f);
                }
                return;
            }
            for (int num508 = 0; num508 < 50; num508++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, 2 * hit.HitDirection, -2f);
            }
            Gore.NewGore(NPC.GetSource_Death(),NPC.position, NPC.velocity, Mod.Find<ModGore>("DesertTalon1").Type);
            Gore.NewGore(NPC.GetSource_Death(),new Vector2(NPC.position.X + 14f, NPC.position.Y), NPC.velocity, Mod.Find<ModGore>("DesertTalon2").Type);
            Gore.NewGore(NPC.GetSource_Death(),new Vector2(NPC.position.X + 14f, NPC.position.Y), NPC.velocity, Mod.Find<ModGore>("DesertTalon3").Type);
        }
    }
}
