using Avalon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.PreHardmode.RedArowana;

public class RedArowana : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 6;
        Data.Sets.NPCSets.Watery[NPC.type] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2(8, 8),
			PortraitPositionXOverride = 0,
			PortraitPositionYOverride = 12,
			IsWet = true,
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
	}

    public override void SetDefaults()
	{
		NPC.CloneDefaults(NPCID.Piranha);
        AnimationType = NPCID.Pupfish;
        NPC.width = 55;
        SpawnModBiomes = [ModContent.GetInstance<Biomes.UndergroundTropics>().Type];
    }
   
    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        NPC.ai[1] = 1;
        NPC.ai[0] = 0;
        SoundEngine.PlaySound(SoundID.Item3, NPC.position);
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return spawnInfo.Water && Main.hardMode && spawnInfo.Player.InModBiome<Biomes.UndergroundTropics>() && !spawnInfo.Player.InPillarZone() ? 0.5f : 0f;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.RedArowana"))
        });
    }
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            string path = $"{Name}" + "Gore";
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>(path + "0").Type, NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity.RotatedByRandom(0.1f) * Main.rand.NextFloat(0.7f, 0.9f), Mod.Find<ModGore>(path + "1").Type, NPC.scale);

            for(int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                d.scale *= 2;
                d.noGravity = !Main.rand.NextBool(3);
                d.velocity += NPC.velocity * 0.3f;
                if (!d.noGravity)
                    d.fadeIn = Main.rand.NextFloat(2);
            }
        }
    }
}
public class RedArowana2 : RedArowana
{

}

