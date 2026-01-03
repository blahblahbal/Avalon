using Avalon.Common;
using Avalon.NPCs.Hardmode.Cougher;
using Avalon.NPCs.Hardmode.Ickslime;
using Avalon.NPCs.Hardmode.Viris;
using Avalon.NPCs.PreHardmode.Bactus;
using Avalon.NPCs.PreHardmode.Pyrasite;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class MobRift : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 8;
		var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Hide = true
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
	}

    public override void SetDefaults()
    {
        NPC.width = NPC.height = 20;
        NPC.noTileCollide = NPC.noGravity = true;
        NPC.npcSlots = 0f;
        NPC.damage = 0;
        NPC.lifeMax = 100;
        NPC.dontTakeDamage = true;
        NPC.alpha = 120;
        NPC.defense = 0;
        NPC.aiStyle = -1;
        NPC.value = 0;
        NPC.knockBackResist = 0f;
        NPC.scale = 1f;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath39;
    }

    public override void FindFrame(int frameHeight)
    {
        if (++NPC.frameCounter > 8)
        {
            NPC.frameCounter = 0;
            NPC.frame.Y += frameHeight;
            if (NPC.frame.Y > frameHeight * 7)
            {
                NPC.frame.Y = 0;
            }
        }
    }

    public override void AI()
    {
        NPC.velocity *= 0f;
        NPC.ai[0]++;
        //evil
        if (NPC.ai[1] == 0)
        {
            if (NPC.ai[0] < 40)
            {
                NPC.alpha -= 3;
            }
            if (NPC.ai[0] > 150)
            {
                NPC.alpha += 5;
            }
            if (NPC.ai[0] % 60 == 0)
            {
                Player p = Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)];
				//TODO: altlib support
                if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Corruption)
                {
                    if (Main.rand.NextBool(2)) // crimson mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Crimslime);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Herpling);
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IchorSticker);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.FloatyGross);
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(3);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Crimera);
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.FaceMonster);
                            if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.BloodCrawler);
                        }
                    }
                    else // contagion mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Ickslime>());
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Cougher>());
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                int t = Main.rand.Next(3);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Ickslime>());
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Cougher>());
                                if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Viris>());
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(2);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Bactus>());
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<PyrasiteHead>());
                            //if (t == 2) NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.BloodCrawler);
                        }
                    }
                }
                else if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion) // contagion world
                {
                    if (Main.rand.NextBool(2)) // crimson mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Crimslime);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Herpling);
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IchorSticker);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.FloatyGross);
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(3);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Crimera);
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.FaceMonster);
                            if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.BloodCrawler);
                        }
                    }
                    else // corruption mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(3);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.CorruptSlime);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Slimer);
                                if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Corruptor);
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                int t = Main.rand.Next(3);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.SeekerHead);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.CorruptSlime);
                                if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Corruptor);
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(2);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.EaterofSouls);
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DevourerHead);
                            //if (t == 2) NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.BloodCrawler);
                        }
                    }
                }
                else if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Crimson) // crimson
                {
                    if (Main.rand.NextBool(2)) // corruption mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(3);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.CorruptSlime);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Slimer);
                                if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Corruptor);
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                int t = Main.rand.Next(3);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.SeekerHead);
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.CorruptSlime);
                                if (t == 2) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Corruptor);
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(2);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.EaterofSouls);
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DevourerHead);
                            //if (t == 2) NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.BloodCrawler);
                        }
                    }
                    else // contagion mobs
                    {
                        if (Main.hardMode)
                        {
                            if (p.position.Y < Main.worldSurface)
                            {
                                int t = Main.rand.Next(2);
                                if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Ickslime>());
                                if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Cougher>());
                            }
                            else if (p.ZoneRockLayerHeight)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Viris>());
                            }
                        }
                        else
                        {
                            int t = Main.rand.Next(2);
                            if (t == 0) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Bactus>());
                            if (t == 1) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<PyrasiteHead>());
                        }
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    int num893 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
                    Main.dust[num893].velocity *= 2f;
                    Main.dust[num893].scale = 0.9f;
                    Main.dust[num893].noGravity = true;
                    Main.dust[num893].fadeIn = 3f;
                }
                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
            }
        }
        //confection/hallow
        else if (NPC.ai[1] == 1)
        {
            if (NPC.ai[0] < 40)
            {
                NPC.alpha -= 3;
            }
            if (NPC.ai[0] > 150)
            {
                NPC.alpha += 5;
            }
            if (NPC.ai[0] % 60 == 0)
            {
                Player p = Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)];
                if (ExxoAvalonOrigins.Confection != null)
                {
                    #region hallow to confection
                    if (p.ZoneHallow && !p.ZoneDesert && !p.ZoneUndergroundDesert)
                    {
                        // surface
                        if (p.position.Y < Main.worldSurface)
                        {
                            if (Main.dayTime)
                            {
                                switch (Main.rand.Next(2))
                                {
                                    case 0:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("Rollercookie").Type);
                                        break;
                                    case 1:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("Sprinkler").Type);
                                        break;
                                }
                            }
                            else
                            {
                                switch (Main.rand.Next(3))
                                {
                                    case 0:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("Rollercookie").Type);
                                        break;
                                    case 1:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("Sprinkler").Type);
                                        break;
                                    case 2:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("WildWilly").Type);
                                        break;
                                }
                            }
                        }
                        // underground
                        else
                        {
                            switch (Main.rand.Next(5))
                            {
                                case 0:
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("Iscreamer").Type);
                                    break;
                                case 1:
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("FoaminFloat").Type);
                                    break;
                                //case 2:
                                //    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("IcecreamGal").Type);
                                //    break;
                                case 2:
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("ParfaitSlime").Type);
                                    break;
                                case 3:
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("Prickster").Type);
                                    break;
                            }
                        }
                    }
                    else if (p.ZoneRain && p.ZoneHallow)
                    {
                        switch (Main.rand.Next(3))
                        {
                            case 0:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("Rollercookie").Type);
                                break;
                            case 1:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("Sprinkler").Type);
                                break;
                            case 2:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ExxoAvalonOrigins.Confection.Find<ModNPC>("SherbetSlime").Type);
                                break;
                        }
                    }
                    #endregion

                    #region confection to hallow
                    if (p.InModBiome(ExxoAvalonOrigins.Confection.Find<ModBiome>("ConfectionBiome")))
                    {
                        // surface
                        if (p.position.Y < Main.worldSurface)
                        {
                            if (Main.dayTime)
                            {
                                switch (Main.rand.Next(2))
                                {
                                    case 0:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Unicorn);
                                        break;
                                    case 1:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Pixie);
                                        break;
                                }
                            }
                            else
                            {
                                switch (Main.rand.Next(3))
                                {
                                    case 0:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Unicorn);
                                        break;
                                    case 1:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Pixie);
                                        break;
                                    case 2:
                                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Gastropod);
                                        break;
                                }
                            }
                        }
                        // underground
                        else
                        {
                            switch (Main.rand.Next(3))
                            {
                                case 0:
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IlluminantBat);
                                    break;
                                case 1:
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.IlluminantSlime);
                                    break;
                                case 2:
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.ChaosElemental);
                                    break;
                            }
                        }
                    }
                    // raining
                    else if (p.InModBiome(ExxoAvalonOrigins.Confection.Find<ModBiome>("ConfectionBiome")) && p.ZoneRain)
                    {
                        switch (Main.rand.Next(3))
                        {
                            case 0:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Unicorn);
                                break;
                            case 1:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.Pixie);
                                break;
                            case 2:
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.RainbowSlime);
                                break;
                        }
                    }
                    #endregion
                }
            }
        }
        if (NPC.ai[0] >= 300) NPC.active = false;
    }
}
