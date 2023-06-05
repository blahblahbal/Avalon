using Avalon.DropConditions;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Armor.PreHardmode;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Material.Shards;
using Avalon.Items.Material.TomeMats;
using Avalon.Items.Other;
using Avalon.Items.Placeable.Painting;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Magic.PreHardmode;
using Avalon.Items.Weapons.Melee.PreHardmode;
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
        var hardModeCondition = new HardmodeOnly();
        var preHardModeCondition = new Invert(hardModeCondition);
        var notFromStatueCondition = new Conditions.NotFromStatue();
        var notExpertCondition = new Conditions.NotExpert();

        if (npc.type == NPCID.Plantera)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<LifeDew>(), 1, 14, 20));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.ChlorophyteOre, 1, 60, 120));
        }
        if(npc.type == NPCID.AngryBones || npc.type >= NPCID.AngryBonesBig && npc.type <= NPCID.AngryBonesBigHelmet)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlackWhetstone>(), 100));
        }
        if(npc.type == NPCID.SpikedIceSlime || npc.type == NPCID.IceGolem || npc.type == NPCID.IcyMerman)
        {
            npcLoot.Add(ItemDropRule.StatusImmunityItem(ItemID.HandWarmer, 100));
        }

        switch (npc.type)
        {
            case NPCID.Unicorn:
            case NPCID.BloodJelly:
            case NPCID.LightMummy:
            case NPCID.DarkMummy:
            case NPCID.BloodMummy:
                npcLoot.Add(ItemDropRule.StatusImmunityItem(ModContent.ItemType<HiddenBlade>(), 100));
                break;
            case NPCID.Mummy:
            case NPCID.FungoFish:
            case NPCID.Clinger:
                npcLoot.Add(ItemDropRule.StatusImmunityItem(ModContent.ItemType<AmmoMagazine>(), 100));
                break;
            case NPCID.Duck or NPCID.Duck2 or NPCID.DuckWhite or NPCID.DuckWhite2:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Quack>(), VeryRareChance));
                break;
            case NPCID.EaterofSouls:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EvilOuroboros>(), RareChance));
                break;
            case NPCID.KingSlime:
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<BandofSlime>(),
                    3));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BirthofaMonster>(), 9));
                break;
            case NPCID.DialatedEye:
                npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 40));
                break;
            case NPCID.UndeadMiner:
                //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinersPickaxe>(), 30));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinersSword>(), 30));
                break;
            case NPCID.BoneSerpentHead:
                npcLoot.Add(ItemDropRule.Common(ItemID.Sunfury, 20));
                break;
            case NPCID.WyvernHead:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MysticalTotem>(), 2));
                break;
            case NPCID.Harpy:
                npcLoot.Add(ItemDropRule.ByCondition(hardModeCondition, ItemID.ShinyRedBalloon, UncommonChance));
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
        #region mystical tome mats
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
        #endregion mystical tome mats

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

        if (npc.boss)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<StaminaCrystal>(), 4));
        }
    }
    public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    {
        var desertPostBeakCondition = new DesertPostBeakDrop();

        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumHeadgear>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumPlateMail>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumGreaves>(), 150));

        globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(),ModContent.ItemType<BloodyWhetstone>(), 160));
        globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<BloodBarrage>(), 160));
        globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<SanguineKatana>(), 160));
        globalLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<SanguineKabuto>(), 160));
    }
}
