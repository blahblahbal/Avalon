using Avalon.NPCs.Bosses.Hardmode;
using Avalon.NPCs.Hardmode;
using Avalon.NPCs.PreHardmode;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Data.Sets;

public static class NPCSets
{
	//public static bool[] DontDropPotions = NPCID.Sets.Factory.CreateBoolSet(false, ModContent.NPCType<HomingRocket>());

	public static readonly bool[] SilenceCandleStopSpawns = NPCID.Sets.Factory.CreateBoolSet(
		ModContent.NPCType<CursedFlamer>(), ModContent.NPCType<ViralMummy>(), ModContent.NPCType<Cougher>(),
		ModContent.NPCType<Shadlopod>(), ModContent.NPCType<Viris>(), ModContent.NPCType<Bactus>(),
		ModContent.NPCType<PyrasiteHead>(), ModContent.NPCType<Ickslime>(), ModContent.NPCType<InfectedPickaxe>(),
		ModContent.NPCType<Gargoyle>(), ModContent.NPCType<HellboundLizard>(), ModContent.NPCType<EctoHand>(),
		ModContent.NPCType<ContaminatedGhoul>(), ModContent.NPCType<MineralSlime>(), ModContent.NPCType<OreSlime>(),
		ModContent.NPCType<VenusFlytrap>(), ModContent.NPCType<PoisonDartFrog>(), ModContent.NPCType<RedArowana>(),
		ModContent.NPCType<RedArowana2>(), ModContent.NPCType<Mosquito>(), ModContent.NPCType<MosquitoDroopy>(),
		ModContent.NPCType<MosquitoPainted>(), ModContent.NPCType<MosquitoSmall>(), ModContent.NPCType<TropicalSlimeGrassy>(),
		ModContent.NPCType<TropicalSlimeShroomy>(), ModContent.NPCType<AmberSlime>(), ModContent.NPCType<InfestedAmberSlime>(),
		ModContent.NPCType<Rafflesia>(), NPCID.RedDevil, NPCID.Demon, NPCID.GiantWalkingAntlion, NPCID.WalkingAntlion,
		NPCID.Antlion, NPCID.DuneSplicerHead, NPCID.DesertDjinn, NPCID.DesertLamiaDark, NPCID.DesertBeast);

	public static readonly bool[] NoAcidDamage = NPCID.Sets.Factory.CreateBoolSet(
		NPCID.Skeleton, NPCID.SkeletonAlien, NPCID.SkeletonArcher, NPCID.SkeletonAstonaut, NPCID.SkeletonCommando, NPCID.SkeletonMerchant,
		NPCID.SkeletonSniper, NPCID.SkeletonTopHat, NPCID.ArmoredSkeleton, NPCID.BoneThrowingSkeleton, NPCID.BoneThrowingSkeleton2,
		NPCID.BoneThrowingSkeleton3, NPCID.BoneThrowingSkeleton4, NPCID.HeadacheSkeleton, NPCID.GreekSkeleton, NPCID.PantlessSkeleton,
		NPCID.TacticalSkeleton, NPCID.AngryBones, NPCID.AngryBonesBig, NPCID.AngryBonesBigHelmet, NPCID.AngryBonesBigMuscle,
		NPCID.HellArmoredBones, NPCID.HellArmoredBonesMace, NPCID.HellArmoredBonesSpikeShield, NPCID.HellArmoredBonesSword,
		NPCID.BlueArmoredBones, NPCID.BlueArmoredBonesMace, NPCID.BlueArmoredBonesNoPants, NPCID.BlueArmoredBonesSword,
		NPCID.RustyArmoredBonesAxe, NPCID.RustyArmoredBonesFlail, NPCID.RustyArmoredBonesSword, NPCID.RustyArmoredBonesSwordNoArmor,
		NPCID.Paladin, NPCID.Necromancer, NPCID.NecromancerArmored, NPCID.RaggedCaster, NPCID.RaggedCasterOpenCoat,
		NPCID.DiabolistRed, NPCID.DiabolistWhite, NPCID.DarkCaster);

    public static readonly bool[] Arcane = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.Pixie,
        NPCID.LightMummy,
        NPCID.EnchantedSword,
        NPCID.Unicorn,
        NPCID.ChaosElemental,
        NPCID.Gastropod,
        NPCID.IlluminantBat,
        NPCID.IlluminantSlime,
        NPCID.PigronHallow,
        NPCID.RainbowSlime,
        NPCID.DesertGhoulHallow,
        NPCID.SandsharkHallow,
        NPCID.DesertLamiaLight);

    public static readonly bool[] Earthen = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.GiantWormHead,
        NPCID.MotherSlime,
        NPCID.ManEater,
        NPCID.CaveBat,
        NPCID.Snatcher,
        NPCID.Antlion,
        NPCID.GiantBat,
        NPCID.DiggerHead,
        NPCID.GiantTortoise,
        NPCID.WallCreeper,
        NPCID.WallCreeperWall,
        NPCID.BlackRecluse,
        NPCID.BlackRecluseWall,
        NPCID.GiantFlyingAntlion,
        NPCID.FlyingAntlion,
        NPCID.WalkingAntlion,
        NPCID.GiantWalkingAntlion);

    public static readonly bool[] Fiery = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.FireImp,
        NPCID.LavaSlime,
        NPCID.Hellbat,
        NPCID.Demon,
        NPCID.VoodooDemon,
        NPCID.Lavabat,
        NPCID.RedDevil);
    //ModContent.NPCType<ArmoredHellTortoise>());

    public static readonly bool[] Flyer = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.DemonEye,
        NPCID.EaterofSouls,
        NPCID.Harpy,
        NPCID.CaveBat,
        NPCID.JungleBat,
        NPCID.Pixie,
        NPCID.WyvernHead,
        NPCID.GiantBat,
        NPCID.Crimera,
        NPCID.CataractEye,
        NPCID.SleepyEye,
        NPCID.DialatedEye,
        NPCID.GreenEye,
        NPCID.PurpleEye,
        NPCID.Moth,
        NPCID.FlyingFish,
        NPCID.FlyingSnake,
        NPCID.AngryNimbus,
        NPCID.Drippler);
        //ModContent.NPCType<VampireHarpy>(),
        //ModContent.NPCType<Dragonfly>());

    public static readonly bool[] Frozen = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.IceSlime,
        NPCID.IceBat,
        NPCID.IceTortoise,
        NPCID.Wolf,
        NPCID.UndeadViking,
        NPCID.IceElemental,
        NPCID.PigronCorruption,
        NPCID.PigronHallow,
        NPCID.PigronCrimson,
        NPCID.SpikedIceSlime,
        NPCID.SnowFlinx,
        NPCID.IcyMerman,
        NPCID.IceGolem);

    public static readonly int[] VanillaNoOneHitKill = new int[]
    {
        NPCID.Everscream,
        NPCID.IceQueen,
        NPCID.SantaNK1,
        NPCID.MourningWood,
        NPCID.Pumpking,
        NPCID.PumpkingBlade,
        NPCID.DungeonGuardian
    };

    public static readonly bool[] Slimes = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.BlueSlime,
        NPCID.MotherSlime,
        NPCID.LavaSlime,
        NPCID.DungeonSlime,
        NPCID.CorruptSlime,
        NPCID.Slimer,
        NPCID.IlluminantSlime,
        NPCID.IceSlime,
        NPCID.Crimslime,
        NPCID.SpikedIceSlime,
        NPCID.SpikedJungleSlime,
        NPCID.UmbrellaSlime,
        NPCID.RainbowSlime,
        NPCID.SlimeMasked,
        NPCID.SlimeRibbonWhite,
        NPCID.SlimeRibbonYellow,
        NPCID.SlimeRibbonGreen,
        NPCID.SlimeRibbonRed,
        NPCID.SlimeSpiked,
        NPCID.SandSlime);
        //ModContent.NPCType<DarkMotherSlime>(),
        //ModContent.NPCType<DarkMatterSlime>());

    //public static readonly int[] SuperHardmodeMobs =
    //{
    //    NPCID.Creeper, NPCID.Pumpking, NPCID.SantaNK1, ModContent.NPCType<AegisHallowor>(),
    //    ModContent.NPCType<ArmageddonSlime>(), ModContent.NPCType<ArmoredHellTortoise>(),
    //    ModContent.NPCType<ArmoredWraith>(), ModContent.NPCType<BactusMinion>(), // remove later
    //    ModContent.NPCType<BombBones>(), ModContent.NPCType<BombSkeleton>(), ModContent.NPCType<CloudBat>(),
    //    ModContent.NPCType<CometTail>(), ModContent.NPCType<CrystalBones>(), ModContent.NPCType<CrystalSpectre>(),
    //    ModContent.NPCType<CursedMagmaSkeleton>(), ModContent.NPCType<DarkMatterSlime>(),
    //    ModContent.NPCType<DarkMotherSlime>(), ModContent.NPCType<Dragonfly>(), ModContent.NPCType<DragonLordBody>(),
    //    ModContent.NPCType<DragonLordBody2>(), ModContent.NPCType<DragonLordBody3>(),
    //    ModContent.NPCType<DragonLordHead>(), ModContent.NPCType<DragonLordLegs>(),
    //    ModContent.NPCType<DragonLordTail>(), ModContent.NPCType<Ectosphere>(), ModContent.NPCType<EyeBones>(),
    //    ModContent.NPCType<GuardianBones>(), ModContent.NPCType<GuardianCorruptor>(),
    //    ModContent.NPCType<ImpactWizard>(), ModContent.NPCType<Juggernaut>(), ModContent.NPCType<JuggernautSorcerer>(),
    //    ModContent.NPCType<MatterMan>(), ModContent.NPCType<MechanicalDiggerBody>(),
    //    ModContent.NPCType<MechanicalDiggerHead>(), ModContent.NPCType<MechanicalDiggerTail>(),
    //    ModContent.NPCType<Mechasting>(), ModContent.NPCType<ProtectorWheel>(), ModContent.NPCType<QuickCaribe>(),
    //    ModContent.NPCType<RedAegisBonesHelmet>(), ModContent.NPCType<RedAegisBonesHorned>(),
    //    ModContent.NPCType<RedAegisBonesSparta>(), ModContent.NPCType<RedAegisBonesSpike>(),
    //    ModContent.NPCType<UnstableAnomaly>(), ModContent.NPCType<UnvolanditeMite>(),
    //    ModContent.NPCType<UnvolanditeMiteDigger>(), ModContent.NPCType<Valkyrie>(), ModContent.NPCType<VampireHarpy>(),
    //    ModContent.NPCType<VorazylcumMite>(), ModContent.NPCType<VorazylcumMiteDigger>(),
    //};

    public static readonly bool[] Toxic = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.Hornet,
        NPCID.ManEater,
        NPCID.GiantTortoise,
        NPCID.AngryTrapper,
        NPCID.MossHornet,
        NPCID.SpikedJungleSlime,
        NPCID.HornetFatty,
        NPCID.HornetHoney,
        NPCID.HornetLeafy,
        NPCID.HornetSpikey,
        NPCID.HornetStingy,
        NPCID.JungleCreeper,
        NPCID.DesertScorpionWalk,
        NPCID.DesertScorpionWall);

    public static readonly bool[] Undead = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.Zombie,
        NPCID.Skeleton,
        NPCID.AngryBones,
        NPCID.DarkCaster,
        NPCID.CursedSkull,
        NPCID.UndeadMiner,
        NPCID.Tim,
        NPCID.DoctorBones,
        NPCID.TheGroom,
        NPCID.ArmoredSkeleton,
        NPCID.Mummy,
        NPCID.Wraith,
        NPCID.SkeletonArcher,
        NPCID.BaldZombie,
        NPCID.PossessedArmor,
        NPCID.VampireBat,
        NPCID.Vampire,
        NPCID.ZombieEskimo,
        NPCID.UndeadViking,
        NPCID.RuneWizard,
        NPCID.PincushionZombie,
        NPCID.SlimedZombie,
        NPCID.SwampZombie,
        NPCID.TwiggyZombie,
        NPCID.ArmoredViking,
        NPCID.FemaleZombie,
        NPCID.HeadacheSkeleton,
        NPCID.MisassembledSkeleton,
        NPCID.PantlessSkeleton,
        NPCID.ZombieRaincoat,
        NPCID.Eyezor,
        NPCID.Reaper,
        NPCID.ZombieMushroom,
        NPCID.ZombieMushroomHat,
        NPCID.ZombieDoctor,
        NPCID.ZombieSuperman,
        NPCID.ZombiePixie,
        NPCID.SkeletonTopHat,
        NPCID.SkeletonAstonaut,
        NPCID.SkeletonAlien,
        NPCID.ZombieXmas,
        NPCID.ZombieSweater,
        NPCID.ArmedZombie,
        NPCID.ArmedTorchZombie,
        NPCID.ArmedZombieCenx,
        NPCID.ArmedZombieEskimo,
        NPCID.ArmedZombiePincussion,
        NPCID.ArmedZombieSlimed,
        NPCID.ArmedZombieSwamp,
        NPCID.ArmedZombieTwiggy,
        NPCID.BoneThrowingSkeleton,
        NPCID.BoneThrowingSkeleton2,
        NPCID.BoneThrowingSkeleton3,
        NPCID.BoneThrowingSkeleton4,
        NPCID.GreekSkeleton,
        NPCID.BloodZombie,
        NPCID.TheBride,
        NPCID.ZombieMerman,
        NPCID.TorchZombie,
        NPCID.BloodMummy,
        NPCID.DarkMummy,
        NPCID.LightMummy,
        NPCID.MaggotZombie,
        NPCID.SporeSkeleton);

    public static readonly bool[] Watery = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.Piranha,
        NPCID.BlueJellyfish,
        NPCID.PinkJellyfish,
        NPCID.Shark,
        NPCID.Crab,
        NPCID.GreenJellyfish,
        NPCID.Arapaima,
        NPCID.SeaSnail,
        NPCID.Squid,
        NPCID.AnglerFish);

    public static readonly bool[] Wicked = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.EaterofSouls,
        NPCID.DevourerHead,
        NPCID.CorruptBunny,
        NPCID.CorruptGoldfish,
        NPCID.DarkMummy,
        NPCID.CorruptSlime,
        NPCID.CursedHammer,
        NPCID.Corruptor,
        NPCID.SeekerHead,
        NPCID.Clinger,
        NPCID.Slimer,
        NPCID.PigronCorruption,
        NPCID.Crimera,
        NPCID.Herpling,
        NPCID.CrimsonAxe,
        NPCID.PigronCrimson,
        NPCID.FaceMonster,
        NPCID.FloatyGross,
        NPCID.Crimslime,
        NPCID.BloodCrawler,
        NPCID.BloodCrawlerWall,
        NPCID.BloodFeeder,
        NPCID.BloodJelly,
        NPCID.IchorSticker,
        NPCID.BigMimicCorruption,
        NPCID.BigMimicCrimson,
        NPCID.DesertGhoulCorruption,
        NPCID.DesertGhoulCrimson,
        NPCID.SandsharkCorrupt,
        NPCID.SandsharkCrimson,
        NPCID.BloodMummy,
        NPCID.DesertLamiaDark);
        //ModContent.NPCType<GuardianCorruptor>(),
}
