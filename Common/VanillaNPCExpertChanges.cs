using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Avalon.Common
{
    public class VanillaNPCExpertChanges : GlobalNPC
    {
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (npc.netID == NPCID.BlazingWheel)
            {
                npc.scale *= 1.5f;
                npc.Size *= 1.5f;
            }
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if(Main.rand.NextBool(5) && Main.expertMode && npc.netID is -6 or 16 or -5) 
            {
                target.AddBuff(BuffID.Blackout, 60 * 10);
            }
        }
        public override void AI(NPC npc)
        {
            if (Main.expertMode)
            {
                if(npc.netID is 25 or 30 or 33)
                {
                    npc.dontTakeDamage = true;
                }
                if (npc.netID is -9 or -8 or -7 or -6 or 81 or -1 or 200 or -2 or 180 or 141) // lots of slimes
                {
                    npc.ai[0]++;
                }
            }
        }
    }
}
