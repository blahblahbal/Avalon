using System;
using Avalon.Items.Material;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.Enums;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Avalon.NPCs.PreHardmode;

internal class Mosquito : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 3;
    }

    public override void SetDefaults()
    {
        NPC.damage = 25;
        NPC.lifeMax = 51;
        NPC.defense = 12;
        NPC.noGravity = true;
        NPC.width = 70;
        NPC.aiStyle = -1;
        NPC.npcSlots = 1f;
        NPC.height = 46;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 200;
        AnimationType = NPCID.Hornet;
        NPC.knockBackResist = 0.5f;
    }
}
