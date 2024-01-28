using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode;

internal class DesertBeakWingNPC : ModNPC
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
        NPC.damage = 45;
        NPC.noTileCollide = true;
        NPC.lifeMax = 4650;
        NPC.defense = 40;
        NPC.noGravity = true;
        NPC.width = 77;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.value = 0f;
        NPC.timeLeft = 22500;
        NPC.height = 40;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit28;
        NPC.DeathSound = SoundID.NPCDeath31;
        NPC.scale = 1f;
    }
    public int MainBody
    {
        get => (int)NPC.ai[1];
        set => NPC.ai[1] = value;
    }
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        return false;
    }
    public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
    {
        Main.npc[(int)NPC.ai[1]].life -= damageDone;
    }
    public override void AI()
    {
        //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(NPC.position.ToString()), Color.White);
        if (NPC.ai[2] == 1)
        {
            NPC.position.X = Main.npc[(int)NPC.ai[1]].Center.X - 77 - 31;
        }
        else if (NPC.ai[2] == 2)
        {
            NPC.position.X = Main.npc[(int)NPC.ai[1]].Center.X + 31;
        }

        if (Main.npc[(int)NPC.ai[1]].frame.Y == 0 * 178)
        {
            NPC.position.Y = Main.npc[(int)NPC.ai[1]].Center.Y - 39 - 15;
        }
        if (Main.npc[(int)NPC.ai[1]].frame.Y == 1 * 178)
        {
            NPC.position.Y = Main.npc[(int)NPC.ai[1]].Center.Y - 39 - 3;
        }
        if (Main.npc[(int)NPC.ai[1]].frame.Y == 2 * 178)
        {
            NPC.position.Y = Main.npc[(int)NPC.ai[1]].Center.Y - 9;
        }
        if (Main.npc[(int)NPC.ai[1]].frame.Y == 3 * 178)
        {
            NPC.position.Y = Main.npc[(int)NPC.ai[1]].Center.Y + 2;
        }
        if (Main.npc[(int)NPC.ai[1]].frame.Y == 4 * 178)
        {
            NPC.position.Y = Main.npc[(int)NPC.ai[1]].Center.Y - 39 + 17;
        }
        if (Main.npc[(int)NPC.ai[1]].frame.Y == 5 * 178)
        {
            NPC.position.Y = Main.npc[(int)NPC.ai[1]].Center.Y - 39 + 7;
        }
        if (Main.npc[(int)NPC.ai[1]].frame.Y == 6 * 178)
        {
            NPC.position.Y = Main.npc[(int)NPC.ai[1]].Center.Y - 39 + 3;
        }
        if (Main.npc[(int)NPC.ai[1]].frame.Y == 7 * 178)
        {
            NPC.position.Y = Main.npc[(int)NPC.ai[1]].Center.Y - 39 - 5;
        }

        //if (NPC.ai[2] == 1)
        //{
        //    NPC.position.X = Main.npc[MainBody].Center.X - 77 - 31;
        //}
        //else if (NPC.ai[2] == 2)
        //{
        //    NPC.position.X = Main.npc[MainBody].Center.X + 31;
        //}
        //if (Main.npc[MainBody].frame.Y == 0 * 178)
        //{
        //    NPC.position.Y = Main.npc[MainBody].Center.Y - 31;
        //}
        //if (Main.npc[MainBody].frame.Y == 1 * 178)
        //{
        //    NPC.position.Y = Main.npc[MainBody].Center.Y - 26;
        //}
        //if (Main.npc[MainBody].frame.Y == 2 * 178)
        //{
        //    NPC.position.Y = Main.npc[MainBody].Center.Y - 12;
        //}
        //if (Main.npc[MainBody].frame.Y == 3 * 178)
        //{
        //    NPC.position.Y = Main.npc[MainBody].Center.Y - 7;
        //}
        //if (Main.npc[MainBody].frame.Y == 4 * 178)
        //{
        //    NPC.position.Y = Main.npc[MainBody].Center.Y - 22;
        //    if (NPC.position.X > Main.npc[MainBody].Center.X)
        //    {
        //        NPC.position.X -= 15;
        //    }
        //    if (NPC.position.X < Main.npc[MainBody].Center.X)
        //    {
        //        NPC.position.X += 15;
        //    }
        //}
        //if (Main.npc[MainBody].frame.Y == 5 * 178)
        //{
        //    NPC.position.Y = Main.npc[MainBody].Center.Y - 31;
        //    if (NPC.position.X > Main.npc[MainBody].Center.X)
        //    {
        //        NPC.position.X -= 30;
        //    }
        //    if (NPC.position.X < Main.npc[MainBody].Center.X)
        //    {
        //        NPC.position.X += 30;
        //    }
        //}
        //if (Main.npc[MainBody].frame.Y == 6 * 178)
        //{
        //    NPC.position.Y = Main.npc[MainBody].Center.Y - 34;
        //    if (NPC.position.X > Main.npc[MainBody].Center.X)
        //    {
        //        NPC.position.X -= 13;
        //    }
        //    if (NPC.position.X < Main.npc[MainBody].Center.X)
        //    {
        //        NPC.position.X += 13;
        //    }
        //}
        //if (Main.npc[MainBody].frame.Y == 7 * 178)
        //{
        //    NPC.position.Y = Main.npc[MainBody].Center.Y - 29;
        //    if (NPC.position.X > Main.npc[MainBody].Center.X)
        //    {
        //        NPC.position.X -= 5;
        //    }
        //    if (NPC.position.X < Main.npc[MainBody].Center.X)
        //    {
        //        NPC.position.X += 5;
        //    }
        //}

        //float heightDiff = Main.npc[MainBody].Center.Y - NPC.Center.Y;
        //float widthDiff = Main.npc[MainBody].Center.X - NPC.Center.X;
        //if (NPC.position.X < Main.npc[MainBody].Center.X)
        //{
        //    NPC.position.Y -= heightDiff - widthDiff * (-Main.npc[MainBody].velocity.X * 0.05f);
        //}
        //if (NPC.position.X > Main.npc[MainBody].Center.X)
        //{
        //    NPC.position.Y += -heightDiff - widthDiff * (Main.npc[MainBody].velocity.X * 0.05f);
        //}


        ////ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(NPC.ai[1].ToString()), Color.White);
        ////ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Main.npc[(int)NPC.ai[1]].position.ToString()), Color.White);
        ////ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Main.player[NPC.target].position.ToString()), Color.White);
        //if (NPC.life <= 0)
        //{
        //    NPC.life = 0;
        //    NPC.active = false;
        //}
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (Main.npc[(int)NPC.ai[1]].life <= 0 || NPC.life <= 0)
        {
            NPC.life = 0;
            NPC.active = false;
        }
    }
}
