using Microsoft.Xna.Framework;
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
        NPC.lifeMax = 4150;
        NPC.defense = 30;
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
        Main.npc[MainBody].life -= damageDone;
        if (Main.npc[MainBody].life <= 0)
        {
            Main.npc[MainBody].life = 0;
            Main.npc[MainBody].checkDead();
            Main.npc[MainBody].HitEffect();
        }
    }
    public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
        Main.npc[MainBody].life -= damageDone;
        if (Main.npc[MainBody].life <= 0)
        {
            Main.npc[MainBody].life = 0;
            Main.npc[MainBody].checkDead();
            Main.npc[MainBody].HitEffect();
        }
    }
    public override void AI()
    {
        //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(NPC.position.ToString()), Color.White);

        // aaaaaaaaaa

        if (NPC.ai[2] == 1)
        {
            NPC.position.X = Main.npc[MainBody].Center.X - 77 - 31;
        }
        else if (NPC.ai[2] == 2)
        {
            NPC.position.X = Main.npc[MainBody].Center.X + 31;
        }

        if (Main.npc[MainBody].frame.Y == 0 * 178)
        {
            NPC.position.Y = Main.npc[MainBody].Center.Y - 31;
        }
        if (Main.npc[MainBody].frame.Y == 1 * 178)
        {
            NPC.position.Y = Main.npc[MainBody].Center.Y - 26;
        }
        if (Main.npc[MainBody].frame.Y == 2 * 178)
        {
            NPC.position.Y = Main.npc[MainBody].Center.Y - 12;
        }
        if (Main.npc[MainBody].frame.Y == 3 * 178)
        {
            NPC.position.Y = Main.npc[MainBody].Center.Y - 7;
        }
        if (Main.npc[MainBody].frame.Y == 4 * 178)
        {
            NPC.position.Y = Main.npc[MainBody].Center.Y - 22;
            if (NPC.ai[2] == 1)
            {
                NPC.position.X += 15;
            }
            else if (NPC.ai[2] == 2)
            {
                NPC.position.X -= 15;
            }
        }
        if (Main.npc[MainBody].frame.Y == 5 * 178)
        {
            NPC.position.Y = Main.npc[MainBody].Center.Y - 31;
            if (NPC.ai[2] == 1)
            {
                NPC.position.X += 30;
            }
            else if (NPC.ai[2] == 2)
            {
                NPC.position.X -= 30;
            }
        }
        if (Main.npc[MainBody].frame.Y == 6 * 178)
        {
            NPC.position.Y = Main.npc[MainBody].Center.Y - 34;
            if (NPC.ai[2] == 1)
            {
                NPC.position.X += 13;
            }
            else if (NPC.ai[2] == 2)
            {
                NPC.position.X -= 13;
            }
        }
        if (Main.npc[MainBody].frame.Y == 7 * 178)
        {
            NPC.position.Y = Main.npc[MainBody].Center.Y - 29;
            if (NPC.ai[2] == 1)
            {
                NPC.position.X += 5;
            }
            else if (NPC.ai[2] == 2)
            {
                NPC.position.X -= 5;
            }
        }

        float heightDiff = Main.npc[MainBody].Center.Y - NPC.Center.Y;
        float widthDiff = Main.npc[MainBody].Center.X - NPC.Center.X;
        if (NPC.ai[2] == 1)
        {
            NPC.position.Y -= heightDiff - widthDiff * (-Main.npc[MainBody].velocity.X * 0.05f);
        }
        else if (NPC.ai[2] == 2)
        {
            NPC.position.Y += -heightDiff - widthDiff * (Main.npc[MainBody].velocity.X * 0.05f);
        }

        //if (NPC.life <= 0)
        //{
        //    NPC.life = 0;
        //    NPC.active = false;
        //}
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0)
        {
            NPC.life = 0;
            NPC.active = false;
            NPC.checkDead();
        }
        if (Main.npc[MainBody].life <= 0)
        {
            Main.npc[MainBody].life = 0;
            Main.npc[MainBody].active = false;
            Main.npc[MainBody].checkDead();
        }
    }
}
