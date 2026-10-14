using Avalon;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class Target : ModNPC
{
    public override void SetStaticDefaults()
    {
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs that you only want one entry for.
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
    }

    public override void SetDefaults()
    {
        NPC.damage = 65;
        NPC.scale = 1f;
        NPC.noTileCollide = true;
        NPC.lifeMax = 1000000000;
        NPC.defense = 0;
        NPC.noGravity = true;
        NPC.aiStyle = -1;
        NPC.width = NPC.height = 32;
        NPC.HitSound = SoundID.NPCHit3;
        NPC.DeathSound = SoundID.NPCDeath14;
        NPC.knockBackResist = 0f;
    }
}
public class TargetMirror : Target
{
	public override void AI()
	{
		NPC.ReflectProjectiles(NPC.Hitbox);
		NPC.dontTakeDamage = true;
	}
}
