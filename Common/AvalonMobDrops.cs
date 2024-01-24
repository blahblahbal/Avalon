using Avalon.DropConditions;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Ammo;
using Avalon.Items.Armor.PreHardmode;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Items.Material.Shards;
using Avalon.Items.Material.TomeMats;
using Avalon.Items.Other;
using Avalon.Items.Placeable.Painting;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Potions.Buff;
using Avalon.Items.Tokens;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Magic.PreHardmode;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Avalon.NPCs.Hardmode;
using Avalon.NPCs.PreHardmode;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonMobDrops : GlobalNPC
{
    private const int RareChance = 700;
    private const int UncommonChance = 50;
    private const int VeryRareChance = 1000;
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        var notExpertCondition = new Conditions.NotExpert();
        var contagionCondition = new IsContagion();
        var corruptionCondition = new Conditions.IsCorruptionAndNotExpert();
        var crimsonNotExpert = new Combine(true, null, notExpertCondition, new CrimsonNotContagion());
        var contagionNotExpert = new Combine(true, null, notExpertCondition, contagionCondition);
        var corruptionNotContagion = new Combine(true, null, new Invert(contagionNotExpert), corruptionCondition);

        var sandstormCondition = new SandstormCondition();

        var hardModeCondition = new HardmodeOnly();
        var notFromStatueCondition = new Conditions.NotFromStatue();
        var preHardModeCondition = new Invert(hardModeCondition);
        //    var superHardModeCondition = new Superhardmode();
        //    var hardmodePreSuperHardmodeCondition =
        //        new Combine(true, null, hardModeCondition, new Invert(new Superhardmode()));

        if (npc.type is NPCID.Paladin)
        {
            npcLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(10, ModContent.ItemType<PaladinHelm>(), ModContent.ItemType<PaladinPlate>(), ModContent.ItemType<PaladinGreaves>()));
        }
        if (npc.type is NPCID.Corruptor or NPCID.IchorSticker or NPCID.ChaosElemental or NPCID.IceElemental
            or NPCID.AngryNimbus or NPCID.GiantTortoise or NPCID.RedDevil)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosCrystal>(), 100));
        }
        if (npc.type == NPCID.Plantera)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<LifeDew>(), 1, 14, 20));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.ChlorophyteOre, 1, 60, 120));
        }
        if (npc.type == NPCID.AngryBones || npc.type >= NPCID.AngryBonesBig && npc.type <= NPCID.AngryBonesBigHelmet)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlackWhetstone>(), 100));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MarrowMasher>(), 100));
        }
        if (npc.type == NPCID.GoblinArcher)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Longbow>(), 75));
        }
        if (npc.type == NPCID.SpikedIceSlime || npc.type == NPCID.IceGolem || npc.type == NPCID.IcyMerman)
        {
            npcLoot.Add(ItemDropRule.StatusImmunityItem(ItemID.HandWarmer, 100));
        }
        if (Data.Sets.NPC.Undead[npc.type])
        {
            ItemDropRule.ByCondition(hardModeCondition, ModContent.ItemType<SoullessLocket>(), 550);
        }

        switch (npc.type)
        {
            case NPCID.Hellbat:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BondrewdHelmet>(), 50));
                break;
            case NPCID.DarkCaster:
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<RodofCoalescence>(), 200, 175));
                break;
            case NPCID.GoblinSorcerer:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosTome>(), 40));
                break;
            case NPCID.ChaosElemental:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosDust>(), 7, 2, 4));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosCharm>(), 30));
                break;
            case NPCID.WallofFlesh:
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<FleshyTendril>(), 1, 13, 19));
                break;
            case NPCID.TheDestroyer:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SonicHat>(), 25, 1, 1));
                break;
            case NPCID.Retinazer:
            case NPCID.Spazmatism:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SonicShirt>(), 25, 1, 1));
                break;
            case NPCID.SkeletronPrime:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SonicShoes>(), 25, 1, 1));
                break;
            case NPCID.Duck or NPCID.Duck2 or NPCID.DuckWhite or NPCID.DuckWhite2:
                LeadingConditionRule Quack = new LeadingConditionRule(new CloverPotionActive());
                Quack.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Quack>(), 500), true);
                Quack.OnFailedConditions(ItemDropRule.Common(ModContent.ItemType<Quack>(), 1000));
                npcLoot.Add(Quack);
                break;
            case NPCID.EaterofSouls:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EvilOuroboros>(), RareChance));
                break;
            case NPCID.KingSlime:
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<BandofSlime>(), 3));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BirthofaMonster>(), 9));
                break;
            case NPCID.DialatedEye:
                npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 40));
                break;
            case NPCID.UndeadMiner:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinersPickaxe>(), 30));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinersSword>(), 30));
                break;
            case NPCID.WyvernHead:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MysticalTotem>(), 2));
                break;
            case NPCID.Harpy:
                npcLoot.Add(ItemDropRule.ByCondition(hardModeCondition, ItemID.ShinyRedBalloon, 50));
                break;
            case NPCID.Vulture:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Beak>(), 3));
                break;
            case NPCID.QueenBee:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FightoftheBumblebee>(), 8));
                break;
            case NPCID.EyeofCthulhu:
                npcLoot.Add(ItemDropRule.ByCondition(
                    preHardModeCondition,
                    ItemID.BloodMoonStarter,
                    10, 1, 1, 3));

                //npcLoot.Add(ItemDropRule.ByCondition(
                //    hardmodePreSuperHardmodeCondition,
                //    ItemID.BloodMoonStarter,
                //    100, 1, 1, 15));

                //npcLoot.Add(ItemDropRule.ByCondition(
                //    superHardModeCondition,
                //    ItemID.BloodMoonStarter,
                //    100, 1, 1, 7));

                break;
            case NPCID.GoblinThief:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoblinDagger>(), 100));
                break;
        }
        //adhesive bandage
        if (npc.type is NPCID.Werewolf or NPCID.AnglerFish or NPCID.RustyArmoredBonesAxe or
            NPCID.RustyArmoredBonesFlail or NPCID.RustyArmoredBonesSword or NPCID.RustyArmoredBonesSwordNoArmor)
        {
            LeadingConditionRule AdhesiveBandage = new LeadingConditionRule(new CloverPotionActive());
            AdhesiveBandage.OnSuccess(ItemDropRule.StatusImmunityItem(885, 50), true);
            AdhesiveBandage.OnFailedConditions(ItemDropRule.StatusImmunityItem(885, 100));
            npcLoot.Add(AdhesiveBandage);
        }
        //armor polish
        if (npc.type is NPCID.ArmoredSkeleton or NPCID.BlueArmoredBones or NPCID.BlueArmoredBonesMace or NPCID.BlueArmoredBonesNoPants or NPCID.BlueArmoredBonesSword)
        {
            LeadingConditionRule ArmorPolish = new LeadingConditionRule(new CloverPotionActive());
            ArmorPolish.OnSuccess(ItemDropRule.StatusImmunityItem(886, 50), true);
            ArmorPolish.OnFailedConditions(ItemDropRule.StatusImmunityItem(886, 100));
            npcLoot.Add(ArmorPolish);
        }
        //bezoar
        if (npc.type is NPCID.ToxicSludge or NPCID.MossHornet or NPCID.Hornet or NPCID.HornetFatty or NPCID.HornetHoney or NPCID.HornetLeafy or NPCID.HornetSpikey or NPCID.HornetStingy)
        {
            LeadingConditionRule Bezoar = new LeadingConditionRule(new CloverPotionActive());
            Bezoar.OnSuccess(ItemDropRule.StatusImmunityItem(887, 50), true);
            Bezoar.OnFailedConditions(ItemDropRule.StatusImmunityItem(887, 100));
            npcLoot.Add(Bezoar);
        }
        //blindfold
        if (npc.type is NPCID.CorruptSlime or NPCID.DarkMummy or NPCID.Crimslime or NPCID.BloodMummy)
        {
            LeadingConditionRule Blindfold = new LeadingConditionRule(new CloverPotionActive());
            Blindfold.OnSuccess(ItemDropRule.StatusImmunityItem(888, 50), true);
            Blindfold.OnFailedConditions(ItemDropRule.StatusImmunityItem(888, 100));
            npcLoot.Add(Blindfold);
        }
        //fast clock
        if (npc.type is NPCID.Mummy or NPCID.Wraith or NPCID.Pixie)
        {
            LeadingConditionRule FastClock = new LeadingConditionRule(new CloverPotionActive());
            FastClock.OnSuccess(ItemDropRule.StatusImmunityItem(889, 50), true);
            FastClock.OnFailedConditions(ItemDropRule.StatusImmunityItem(889, 100));
            npcLoot.Add(FastClock);
        }
        //megaphone
        if (npc.type is NPCID.GreenJellyfish or NPCID.Pixie or NPCID.DarkMummy or NPCID.BloodMummy)
        {
            LeadingConditionRule Megaphone = new LeadingConditionRule(new CloverPotionActive());
            Megaphone.OnSuccess(ItemDropRule.StatusImmunityItem(890, 50), true);
            Megaphone.OnFailedConditions(ItemDropRule.StatusImmunityItem(890, 100));
            npcLoot.Add(Megaphone);
        }
        //nazar
        if (npc.type is NPCID.CursedSkull or NPCID.CursedHammer or NPCID.EnchantedSword or NPCID.CrimsonAxe or NPCID.GiantCursedSkull)
        {
            LeadingConditionRule Nazar = new LeadingConditionRule(new CloverPotionActive());
            Nazar.OnSuccess(ItemDropRule.StatusImmunityItem(891, 50), true);
            Nazar.OnFailedConditions(ItemDropRule.StatusImmunityItem(891, 100));
            npcLoot.Add(Nazar);
        }
        //vitamins
        if (npc.type is NPCID.Corruptor or NPCID.FloatyGross)
        {
            LeadingConditionRule Vitamins = new LeadingConditionRule(new CloverPotionActive());
            Vitamins.OnSuccess(ItemDropRule.StatusImmunityItem(892, 50), true);
            Vitamins.OnFailedConditions(ItemDropRule.StatusImmunityItem(892, 100));
            npcLoot.Add(Vitamins);
        }
        //trifold map
        if (npc.type is NPCID.GiantBat or NPCID.Clown or NPCID.LightMummy)
        {
            LeadingConditionRule TrifoldMap = new LeadingConditionRule(new CloverPotionActive());
            TrifoldMap.OnSuccess(ItemDropRule.StatusImmunityItem(893, 50), true);
            TrifoldMap.OnFailedConditions(ItemDropRule.StatusImmunityItem(893, 100));
            npcLoot.Add(TrifoldMap);
        }
        //hidden blade
        if (npc.type is NPCID.BloodJelly or NPCID.Unicorn or NPCID.DarkMummy or NPCID.LightMummy or NPCID.BloodMummy || npc.type == ModContent.NPCType<ViralMummy>())
        {
            LeadingConditionRule HiddenBlade = new LeadingConditionRule(new CloverPotionActive());
            HiddenBlade.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<HiddenBlade>(), 50), true);
            HiddenBlade.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<HiddenBlade>(), 100));
            npcLoot.Add(HiddenBlade);
        }
        //ammo magazine
        if (npc.type == NPCID.Mummy || npc.type == NPCID.FungoFish || npc.type == NPCID.Clinger)
        {
            LeadingConditionRule AmmoMagazine = new LeadingConditionRule(new CloverPotionActive());
            AmmoMagazine.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<AmmoMagazine>(), 50), true);
            AmmoMagazine.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<AmmoMagazine>(), 100));
            npcLoot.Add(AmmoMagazine);
        }
        //greek extinguisher
        if (npc.type == NPCID.Clinger || npc.type == NPCID.Spazmatism || npc.type == ModContent.NPCType<CursedFlamer>())
        {
            LeadingConditionRule GreekExtinguisher = new LeadingConditionRule(new CloverPotionActive());
            GreekExtinguisher.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<GreekExtinguisher>(), 25), true);
            GreekExtinguisher.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<GreekExtinguisher>(), 50));
            npcLoot.Add(GreekExtinguisher);
        }

        //600 watt lightbulb
        if (npc.type == NPCID.RaggedCaster || npc.type == NPCID.RaggedCasterOpenCoat) // || npc.type == ModContent.NPCType<DarkMatterSlime>())
        {
            LeadingConditionRule SixHundredWattLightbulb = new LeadingConditionRule(new CloverPotionActive());
            SixHundredWattLightbulb.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<SixHundredWattLightbulb>(), 25), true);
            SixHundredWattLightbulb.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<SixHundredWattLightbulb>(), 50));
            npcLoot.Add(SixHundredWattLightbulb);
        }

        //vortex
        if (npc.type is NPCID.SkeletonArcher or NPCID.LavaSlime or NPCID.MeteorHead or NPCID.FireImp or NPCID.Hellbat or NPCID.Lavabat or NPCID.Demon or NPCID.HellArmoredBones or
            NPCID.HellArmoredBonesMace or NPCID.HellArmoredBonesSpikeShield or NPCID.HellArmoredBonesSword)
        {
            LeadingConditionRule Vortex = new LeadingConditionRule(new CloverPotionActive());
            Vortex.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<Vortex>(), 50), true);
            Vortex.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<Vortex>(), 100));
            npcLoot.Add(Vortex);
        }
        //surgical mask
        if (npc.type == ModContent.NPCType<Cougher>() || npc.type == ModContent.NPCType<ContaminatedGhoul>())
        {
            LeadingConditionRule SurgicalMask = new LeadingConditionRule(new CloverPotionActive());
            SurgicalMask.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<SurgicalMask>(), 50), true);
            SurgicalMask.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<SurgicalMask>(), 100));
            npcLoot.Add(SurgicalMask);
        }
        //golden shield
        if (npc.type is NPCID.IchorSticker)
        {
            LeadingConditionRule GoldenShield = new LeadingConditionRule(new CloverPotionActive());
            GoldenShield.OnSuccess(ItemDropRule.StatusImmunityItem(ModContent.ItemType<GoldenShield>(), 35), true);
            GoldenShield.OnFailedConditions(ItemDropRule.StatusImmunityItem(ModContent.ItemType<GoldenShield>(), 70));
            npcLoot.Add(GoldenShield);
        }
        //windshield
        if (npc.type is NPCID.DuneSplicerHead or NPCID.Tumbleweed or NPCID.WalkingAntlion or NPCID.FlyingAntlion or
            NPCID.SandElemental or NPCID.SandShark or NPCID.SandsharkCorrupt or NPCID.SandsharkCrimson or
            NPCID.SandsharkHallow)
        {
            npcLoot.Add(ItemDropRule.ByCondition(sandstormCondition, ModContent.ItemType<Windshield>(), 75));
        }

        if (npc.type == NPCID.EyeofCthulhu)
        {
            // remove corruption loot
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.DemoniteOre);
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CorruptSeeds);
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.UnholyArrow);

            // remove crimson loot
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimtaneOre);
            npcLoot.RemoveWhere(rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimsonSeeds);

            // add corruption loot back
            LeadingConditionRule corruptionRule = new LeadingConditionRule(corruptionNotContagion);
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.DemoniteOre, 1, 30, 90));
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.CorruptSeeds, 1, 1, 3));
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.UnholyArrow, 1, 20, 50));
            corruptionRule.OnSuccess(ItemDropRule.Common(ItemID.BloodMoonStarter, 12));
            npcLoot.Add(corruptionRule);

            // add crimson loot back
            LeadingConditionRule crimsonRule = new LeadingConditionRule(crimsonNotExpert);
            crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.CrimtaneOre, 1, 30, 90));
            crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.CrimsonSeeds, 1, 1, 3));
            crimsonRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BloodyArrow>(), 1, 20, 50));
            crimsonRule.OnSuccess(ItemDropRule.Common(ItemID.BloodMoonStarter, 12));
            npcLoot.Add(crimsonRule);

            // add contagion loot
            LeadingConditionRule contagionRule = new LeadingConditionRule(contagionNotExpert);
            contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 1, 30, 90));
            contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ContagionSeeds>(), 1, 1, 3));
            contagionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<IckyArrow>(), 1, 20, 50));
            contagionRule.OnSuccess(ItemDropRule.Common(ItemID.BloodMoonStarter, 12));
            npcLoot.Add(contagionRule);
        }

        // blood moon loot
        if ((npc.type is NPCID.BloodZombie or NPCID.Drippler or NPCID.EyeballFlyingFish or NPCID.ZombieMerman) ||
            npc.type == ModContent.NPCType<BloodshotEye>() || npc.type == ModContent.NPCType<FallenHero>())
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<BloodyWhetstone>(), 200));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<BloodBarrage>(), 200));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<SanguineKatana>(), 200));
            // haven't tested, maybe better idk
            //npcLoot.Add(ItemDropRule.OneFromOptions(200, ModContent.ItemType<BloodyWhetstone>(), ModContent.ItemType<BloodBarrage>(), ModContent.ItemType<SanguineKatana>()));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<SanguineKabuto>(), 300));
        }

        #region tome mats
        if (npc.type is NPCID.ManEater or NPCID.Snatcher or NPCID.AngryTrapper)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notFromStatueCondition, ModContent.ItemType<DewOrb>(), 25, 1, 1, 4));
        }

        if (npc.type is NPCID.GiantTortoise or NPCID.IceTortoise or NPCID.Vulture or NPCID.FlyingFish
            or NPCID.Unicorn)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementDust>(), 15));
        }

        if (npc.type is NPCID.Harpy or NPCID.CaveBat or NPCID.GiantBat or NPCID.JungleBat)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RubybeadHerb>(), 15));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MysticalClaw>(), 20));
        }

        if (npc.type is NPCID.Hornet or NPCID.BlackRecluse or NPCID.MossHornet or NPCID.HornetFatty
            or NPCID.HornetHoney
            or NPCID.HornetLeafy or NPCID.HornetSpikey or NPCID.HornetStingy or NPCID.JungleCreeper
            or NPCID.JungleCreeperWall or NPCID.BlackRecluseWall)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StrongVenom>(), 15));
        }

        if (npc.type is NPCID.Retinazer or NPCID.Spazmatism or NPCID.SkeletronPrime or NPCID.TheDestroyer)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScrollofTome>(), 8));
        }

        if (npc.type is NPCID.CorruptSlime or NPCID.Gastropod or NPCID.IlluminantSlime or NPCID.ToxicSludge
            or NPCID.Crimslime
            or NPCID.RainbowSlime or NPCID.FloatyGross)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notFromStatueCondition, ModContent.ItemType<DewofHerbs>(),
                25, 1, 1, 4));
        }
        if (npc.type is NPCID.ChaosElemental or NPCID.IceElemental or NPCID.IchorSticker or NPCID.Corruptor ||
            npc.type == ModContent.NPCType<Viris>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementDiamond>(), 6));
        }
        #endregion

        #region shards
        if (Data.Sets.NPC.Toxic[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToxinShard>(), 8));
        }

        if (Data.Sets.NPC.Undead[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UndeadShard>(), 11));
        }

        if (Data.Sets.NPC.Fiery[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireShard>(), 8));
        }

        if (Data.Sets.NPC.Watery[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterShard>(), 8));
        }

        if (Data.Sets.NPC.Earthen[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthShard>(), 12));
        }

        if (Data.Sets.NPC.Flyer[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BreezeShard>(), 13));
        }

        if (Data.Sets.NPC.Frozen[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostShard>(), 10));
        }

        if (Data.Sets.NPC.Wicked[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CorruptShard>(), 9));
        }

        if (Data.Sets.NPC.Arcane[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcaneShard>(), 7));
        }

        if (npc.type is NPCID.ChaosElemental or NPCID.IceElemental or NPCID.IchorSticker or NPCID.Corruptor)// ||
                                                                                                            // npc.type == ModContent.NPCType<Viris>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementDiamond>(), 6));
        }
        #endregion shards

        if (!NPCID.Sets.CountsAsCritter[npc.type] && !npc.townNPC && npc.aiStyle != 9 && npc.life > 5)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(600, ItemID.EndurancePotion, ItemID.GravitationPotion,
                ItemID.InfernoPotion,
                ModContent.ItemType<StarbrightPotion>(), ModContent.ItemType<StrengthPotion>(),
                ModContent.ItemType<AuraPotion>(), ItemID.IronskinPotion, ItemID.SwiftnessPotion,
                ModContent.ItemType<ShockwavePotion>(), ItemID.MiningPotion, ItemID.ObsidianSkinPotion,
                ItemID.NightOwlPotion, ItemID.RagePotion, ItemID.RegenerationPotion, ItemID.SpelunkerPotion,
                ItemID.SonarPotion, ItemID.WrathPotion, ItemID.SummoningPotion, ItemID.HunterPotion,
                ItemID.FlipperPotion, ModContent.ItemType<GPSPotion>(), ItemID.GillsPotion).HideFromBestiary());
        }

        if (npc.boss)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<StaminaCrystal>(), 4));
        }
    }
    public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    {
        var hardModeCondition = new HardmodeOnly();
        var desertPostBeakCondition = new DesertPostBeakDrop();
        var contagionCondition = new ZoneContagion();
        var soulCondition = new UndergroundContagionCondition();

        var zoneRockLayerCondition = new ZoneRockLayer();
        var snowCondition = new ZoneSnow();
        var undergroundSnow = new Combine(true, "Drops in the underground snow", snowCondition, zoneRockLayerCondition);
        var undergroundHardmodeSnow = new Combine(true, undergroundSnow.GetConditionDescription(), undergroundSnow,
            hardModeCondition);

        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumHeadgear>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumPlateMail>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumGreaves>(), 150));


        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.JungleKey);
        LeadingConditionRule JungleKeyRule = new LeadingConditionRule(new CloverPotionActive());
        JungleKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.JungleKey, 1250, 1, 1, new Conditions.JungleKeyCondition()), true);
        JungleKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.JungleKey, 2500, 1, 1, new Conditions.JungleKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CorruptionKey);
        LeadingConditionRule CorruptKeyRule = new LeadingConditionRule(new CloverPotionActive());
        CorruptKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.CorruptionKey, 1250, 1, 1, new Conditions.CorruptKeyCondition()), true);
        CorruptKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.CorruptionKey, 2500, 1, 1, new Conditions.CorruptKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.FrozenKey);
        LeadingConditionRule FrozenKeyRule = new LeadingConditionRule(new CloverPotionActive());
        FrozenKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.FrozenKey, 1250, 1, 1, new Conditions.FrozenKeyCondition()), true);
        FrozenKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.FrozenKey, 2500, 1, 1, new Conditions.FrozenKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimsonKey);
        LeadingConditionRule CrimsonKeyRule = new LeadingConditionRule(new CloverPotionActive());
        CrimsonKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.CrimsonKey, 1250, 1, 1, new Conditions.CrimsonKeyCondition()), true);
        CrimsonKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.CrimsonKey, 2500, 1, 1, new Conditions.CrimsonKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.HallowedKey);
        LeadingConditionRule HallowKeyRule = new LeadingConditionRule(new CloverPotionActive());
        HallowKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.HallowedKey, 1250, 1, 1, new Conditions.HallowKeyCondition()), true);
        HallowKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.HallowedKey, 2500, 1, 1, new Conditions.HallowKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.DungeonDesertKey);
        LeadingConditionRule DesertKeyRule = new LeadingConditionRule(new CloverPotionActive());
        DesertKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.DungeonDesertKey, 1250, 1, 1, new Conditions.DesertKeyCondition()), true);
        DesertKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ItemID.DungeonDesertKey, 2500, 1, 1, new Conditions.DesertKeyCondition()), true);

        LeadingConditionRule ContagionKeyRule = new LeadingConditionRule(new CloverPotionActive());
        ContagionKeyRule.OnSuccess(new ItemDropWithConditionRule(ModContent.ItemType<ContagionKey>(), 1250, 1, 1, new ContagionKeyCondition()), true);
        ContagionKeyRule.OnFailedConditions(new ItemDropWithConditionRule(ModContent.ItemType<ContagionKey>(), 2500, 1, 1, new ContagionKeyCondition()), true);

        globalLoot.Add(FrozenKeyRule);
        globalLoot.Add(JungleKeyRule);
        globalLoot.Add(CorruptKeyRule);
        globalLoot.Add(HallowKeyRule);
        globalLoot.Add(CrimsonKeyRule);
        globalLoot.Add(DesertKeyRule);

        globalLoot.Add(ItemDropRule.ByCondition(undergroundHardmodeSnow, ModContent.ItemType<SoulofIce>(), 10));

        globalLoot.Add(ItemDropRule.ByCondition(soulCondition, ItemID.SoulofNight, 5));

        if (ExxoAvalonOrigins.Tokens != null)
        {
            //globalLoot.Add(ItemDropRule.ByCondition(
            //    new PostPhantasmHellcastleTokenDrop(),
            //    ModContent.ItemType<HellcastleToken>(), 15));
            //globalLoot.Add(ItemDropRule.ByCondition(
            //    new SuperhardmodePreArmaTokenDrop(),
            //    ModContent.ItemType<SuperhardmodeToken>(), 15));
            //globalLoot.Add(ItemDropRule.ByCondition(new PostArmageddonTokenDrop(), ModContent.ItemType<DarkMatterToken>(),
            //    15));
            //globalLoot.Add(ItemDropRule.ByCondition(new PostMechastingTokenDrop(), ModContent.ItemType<MechastingToken>(),
            //    15));
            //globalLoot.Add(ItemDropRule.ByCondition(new ZoneTropicsToken(), ModContent.ItemType<TropicsToken>(), 15));
            globalLoot.Add(ItemDropRule.ByCondition(
                new UndergroundHardmodeContagionTokenDrop(contagionCondition),
                ModContent.ItemType<ContagionToken>(), 15));
        }
    }
}
