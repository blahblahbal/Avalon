using System.Collections.Generic;
using System.Linq;
using Avalon.Items.AdvancedPotions;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Placeable.Tile.LargeHerbs;
using Avalon.Items.Potions.Buff;
using Avalon.Items.Potions.Other;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Data;

public static class HerbologyData
{
    public const int BlahPotionCost = 2500;
    public const int BlahPotionSellPrice = 2500;
    public const int ElixirCost = 10;
    public const int ElixirSellPrice = 10;

    public const int HerbSeedCost = 1;

    public const int HerbSellPrice = 1;
    public const int HerbTier2Threshold = 250;
    public const int HerbTier3Threshold = 750;
    public const int HerbTier4Threshold = 1500;
    public const int LargeHerbSeedCost = 15;
    public const int LargeHerbSeedSellPrice = 15;
    public const int LargeHerbSellPrice = 20;

    public const int PotionCost = 3;

    public const int PotionSellPrice = 1;
    public const int RestorationPotionCost = 2;

    public static readonly int[] ElixirIds =
    {
        ModContent.ItemType<AdvAmmoReservationPotion>(), ModContent.ItemType<AdvArcheryPotion>(), ModContent.ItemType<AdvAuraPotion>(),
        ModContent.ItemType<AdvBattlePotion>(), ModContent.ItemType<AdvBloodCastPotion>(),
        ModContent.ItemType<AdvBuilderPotion>(), ModContent.ItemType<AdvCalmingPotion>(), ModContent.ItemType<AdvCratePotion>(),
        ModContent.ItemType<AdvDangersensePotion>(), ModContent.ItemType<AdvEndurancePotion>(), ModContent.ItemType<AdvFeatherfallPotion>(),
        ModContent.ItemType<AdvFishingPotion>(), ModContent.ItemType<AdvFlipperPotion>(),
        ModContent.ItemType<AdvForceFieldPotion>(), ModContent.ItemType<AdvFuryPotion>(), ModContent.ItemType<AdvGamblerPotion>(),
        ModContent.ItemType<AdvGauntletPotion>(), ModContent.ItemType<AdvGillsPotion>(), ModContent.ItemType<AdvGPSPotion>(),
        ModContent.ItemType<AdvGravitationPotion>(), ModContent.ItemType<AdvHeartreachPotion>(), ModContent.ItemType<AdvHeartsickPotion>(),
        ModContent.ItemType<AdvHunterPotion>(), ModContent.ItemType<AdvInfernoPotion>(),
        ModContent.ItemType<AdvInvisibilityPotion>(), ModContent.ItemType<AdvIronskinPotion>(), ModContent.ItemType<AdvLeapingPotion>(),
        ModContent.ItemType<AdvLifeforcePotion>(), ModContent.ItemType<AdvMagicPowerPotion>(), ModContent.ItemType<AdvMagnetPotion>(),
        ModContent.ItemType<AdvManaRegenerationPotion>(), ModContent.ItemType<AdvMiningPotion>(), ModContent.ItemType<AdvNightOwlPotion>(),
        ModContent.ItemType<AdvNinjaPotion>(), ModContent.ItemType<AdvObsidianSkinPotion>(), ModContent.ItemType<AdvRagePotion>(),
        ModContent.ItemType<AdvRegenerationPotion>(), ModContent.ItemType<AdvRoguePotion>(), ModContent.ItemType<AdvShadowPotion>(),
        ModContent.ItemType<AdvShinePotion>(), ModContent.ItemType<AdvShockwavePotion>(), ModContent.ItemType<AdvSonarPotion>(),
        ModContent.ItemType<AdvSpelunkerPotion>(), ModContent.ItemType<AdvStarbrightPotion>(), ModContent.ItemType<AdvStrengthPotion>(),
        ModContent.ItemType<AdvSummoningPotion>(), ModContent.ItemType<AdvSupersonicPotion>(), ModContent.ItemType<AdvSwiftnessPotion>(),
        ModContent.ItemType<AdvThornsPotion>(), ModContent.ItemType<AdvTimeShiftPotion>(), ModContent.ItemType<AdvTitanPotion>(),
        ModContent.ItemType<AdvTitanskinPotion>(), ModContent.ItemType<AdvVisionPotion>(), ModContent.ItemType<AdvWarmthPotion>(),
        ModContent.ItemType<AdvWaterWalkingPotion>(), ModContent.ItemType<AdvWisdomPotion>(), ModContent.ItemType<AdvWrathPotion>()
    };

    public static readonly Dictionary<int, int> HerbIdByLargeHerbId = new()
    {
        { ModContent.ItemType<LargeDaybloom>(), ItemID.Daybloom },
        { ModContent.ItemType<LargeMoonglow>(), ItemID.Moonglow },
        { ModContent.ItemType<LargeBlinkroot>(), ItemID.Blinkroot },
        { ModContent.ItemType<LargeDeathweed>(), ItemID.Deathweed },
        { ModContent.ItemType<LargeWaterleaf>(), ItemID.Waterleaf },
        { ModContent.ItemType<LargeFireblossom>(), ItemID.Fireblossom },
        { ModContent.ItemType<LargeShiverthorn>(), ItemID.Shiverthorn },
        { ModContent.ItemType<LargeBloodberry>(), ModContent.ItemType<Bloodberry>() },
        { ModContent.ItemType<LargeSweetstem>(), ModContent.ItemType<Sweetstem>() },
        { ModContent.ItemType<LargeBarfbush>(), ModContent.ItemType<Barfbush>() },
        { ModContent.ItemType<LargeHolybird>(), ModContent.ItemType<Holybird>() },
        //{ ModContent.ItemType<LargeTwilightPlume>(), ModContent.ItemType<TwilightPlume>() },
    };

    public static readonly Dictionary<int, int> LargeHerbIdByLargeHerbSeedId = new()
    {
        { ModContent.ItemType<LargeDaybloomSeed>(), ModContent.ItemType<LargeDaybloom>() },
        { ModContent.ItemType<LargeMoonglowSeed>(), ModContent.ItemType<LargeMoonglow>() },
        { ModContent.ItemType<LargeBlinkrootSeed>(), ModContent.ItemType<LargeBlinkroot>() },
        { ModContent.ItemType<LargeDeathweedSeed>(), ModContent.ItemType<LargeDeathweed>() },
        { ModContent.ItemType<LargeWaterleafSeed>(), ModContent.ItemType<LargeWaterleaf>() },
        { ModContent.ItemType<LargeFireblossomSeed>(), ModContent.ItemType<LargeFireblossom>() },
        { ModContent.ItemType<LargeShiverthornSeed>(), ModContent.ItemType<LargeShiverthorn>() },
        { ModContent.ItemType<LargeBloodberrySeed>(), ModContent.ItemType<LargeBloodberry>() },
        { ModContent.ItemType<LargeSweetstemSeed>(), ModContent.ItemType<LargeSweetstem>() },
        { ModContent.ItemType<LargeBarfbushSeed>(), ModContent.ItemType<LargeBarfbush>() },
        { ModContent.ItemType<LargeHolybirdSeed>(), ModContent.ItemType<LargeHolybird>() },
        //{ ModContent.ItemType<LargeTwilightPlumeSeed>(), ModContent.ItemType<LargeTwilightPlume>() },
    };

    public static readonly Dictionary<int, int> LargeHerbSeedIdByHerbId = new()
    {
        { ItemID.Daybloom, ModContent.ItemType<LargeDaybloomSeed>() },
        { ItemID.Moonglow, ModContent.ItemType<LargeMoonglowSeed>() },
        { ItemID.Blinkroot, ModContent.ItemType<LargeBlinkrootSeed>() },
        { ItemID.Deathweed, ModContent.ItemType<LargeDeathweedSeed>() },
        { ItemID.Waterleaf, ModContent.ItemType<LargeWaterleafSeed>() },
        { ItemID.Fireblossom, ModContent.ItemType<LargeFireblossomSeed>() },
        { ItemID.Shiverthorn, ModContent.ItemType<LargeShiverthornSeed>() },
        { ModContent.ItemType<Bloodberry>(), ModContent.ItemType<LargeBloodberrySeed>() },
        { ModContent.ItemType<Sweetstem>(), ModContent.ItemType<LargeSweetstemSeed>() },
        { ModContent.ItemType<Barfbush>(), ModContent.ItemType<LargeBarfbushSeed>() },
        { ModContent.ItemType<Holybird>(), ModContent.ItemType<LargeHolybirdSeed>() },
        //{ ModContent.ItemType<TwilightPlume>(), ModContent.ItemType<LargeTwilightPlumeSeed>() },
    };

    public static readonly Dictionary<int, int> LargeHerbSeedIdByHerbSeedId = new()
    {
        { ItemID.DaybloomSeeds, ModContent.ItemType<LargeDaybloomSeed>() },
        { ItemID.MoonglowSeeds, ModContent.ItemType<LargeMoonglowSeed>() },
        { ItemID.BlinkrootSeeds, ModContent.ItemType<LargeBlinkrootSeed>() },
        { ItemID.DeathweedSeeds, ModContent.ItemType<LargeDeathweedSeed>() },
        { ItemID.WaterleafSeeds, ModContent.ItemType<LargeWaterleafSeed>() },
        { ItemID.FireblossomSeeds, ModContent.ItemType<LargeFireblossomSeed>() },
        { ItemID.ShiverthornSeeds, ModContent.ItemType<LargeShiverthornSeed>() },
        { ModContent.ItemType<BloodberrySeeds>(), ModContent.ItemType<LargeBloodberrySeed>() },
        { ModContent.ItemType<SweetstemSeeds>(), ModContent.ItemType<LargeSweetstemSeed>() },
        { ModContent.ItemType<BarfbushSeeds>(), ModContent.ItemType<LargeBarfbushSeed>() },
        { ModContent.ItemType<HolybirdSeeds>(), ModContent.ItemType<LargeHolybirdSeed>() },
        //{ ModContent.ItemType<TwilightPlumeSeeds>(), ModContent.ItemType<LargeTwilightPlumeSeed>() },
    };

    public static readonly int[] PotionIds =
    {
        ItemID.AmmoReservationPotion, ItemID.ArcheryPotion, ModContent.ItemType<AuraPotion>(),
        ItemID.BattlePotion, ModContent.ItemType<BloodCastPotion>(),
        ItemID.BuilderPotion, ItemID.CalmingPotion, ItemID.CratePotion, ItemID.TrapsightPotion,
        ItemID.EndurancePotion, ItemID.FeatherfallPotion, ItemID.FishingPotion, ItemID.FlipperPotion,
        ModContent.ItemType<ForceFieldPotion>(), ModContent.ItemType<FuryPotion>(), ModContent.ItemType<GamblerPotion>(),
        ModContent.ItemType<GauntletPotion>(), ItemID.GillsPotion, ModContent.ItemType<GPSPotion>(),
        ItemID.GravitationPotion, ItemID.HeartreachPotion, ModContent.ItemType<HeartsickPotion>(),
        ItemID.HunterPotion, ItemID.InfernoPotion, ModContent.ItemType<InvincibilityPotion>(),
        ItemID.InvisibilityPotion, ItemID.IronskinPotion, ModContent.ItemType<LeapingPotion>(),
        ItemID.LifeforcePotion, ItemID.MagicPowerPotion, ModContent.ItemType<MagnetPotion>(),
        ItemID.ManaRegenerationPotion, ItemID.MiningPotion, ItemID.NightOwlPotion,
        ModContent.ItemType<NinjaPotion>(), ItemID.ObsidianSkinPotion, ItemID.RagePotion, ItemID.RegenerationPotion,
        ModContent.ItemType<RejuvenationPotion>(), ModContent.ItemType<RoguePotion>(), ModContent.ItemType<ShadowPotion>(),
        ItemID.ShinePotion, ModContent.ItemType<ShockwavePotion>(), ItemID.SonarPotion, ItemID.SpelunkerPotion,
        ModContent.ItemType<StarbrightPotion>(), ModContent.ItemType<StrengthPotion>(), ItemID.SummoningPotion,
        ModContent.ItemType<SupersonicPotion>(), ItemID.SwiftnessPotion, ItemID.ThornsPotion,
        ModContent.ItemType<TimeShiftPotion>(), ItemID.TitanPotion, ModContent.ItemType<TitanskinPotion>(),
        ModContent.ItemType<VisionPotion>(), ItemID.WarmthPotion, ItemID.WaterWalkingPotion,
        ModContent.ItemType<WisdomPotion>(), ItemID.WrathPotion
    };

    public static readonly int[] SuperRestorationIDs =
    {
        ItemID.SuperHealingPotion, ItemID.SuperManaPotion, ModContent.ItemType<SuperStaminaPotion>() /*, ModContent.ItemType<SuperRestorationPotion>()*/
    };

    public static readonly int[] RestorationIDs =
    {
        ItemID.HealingPotion, ItemID.ManaPotion, ModContent.ItemType<StaminaPotion>(), ItemID.RestorationPotion
    };

    public static int GetBaseHerbType(Item item)
    {
        if (HerbIdByLargeHerbId.ContainsValue(item.type))
        {
            return item.type;
        }

        if (LargeHerbSeedIdByHerbSeedId.ContainsKey(item.type))
        {
            return HerbIdByLargeHerbId[LargeHerbIdByLargeHerbSeedId[LargeHerbSeedIdByHerbSeedId[item.type]]];
        }

        if (LargeHerbIdByLargeHerbSeedId.ContainsKey(item.type))
        {
            return HerbIdByLargeHerbId[LargeHerbIdByLargeHerbSeedId[item.type]];
        }

        return -1;
    }

    public static int GetItemCost(Item item, int amount)
    {
        if (LargeHerbSeedIdByHerbSeedId.ContainsKey(item.type))
        {
            return amount * HerbSeedCost;
        }

        if (LargeHerbSeedIdByHerbSeedId.ContainsValue(item.type))
        {
            return amount * LargeHerbSeedCost;
        }

        if (PotionIds.Contains(item.type))
        {
            return amount * PotionCost;
        }

        if (ElixirIds.Contains(item.type))
        {
            return amount * ElixirCost;
        }

        if (item.type == ModContent.ItemType<BlahPotion>())
        {
            return amount * BlahPotionCost;
        }

        if (RestorationIDs.Contains(item.type))
        {
            return amount * RestorationPotionCost;
        }

        if (SuperRestorationIDs.Contains(item.type))
        {
            return amount * RestorationPotionCost * 2;
        }

        return 0;
    }

    public static bool ItemIsPotion(Item item) => PotionIds.Contains(item.type) || ElixirIds.Contains(item.type)
        || item.type == ModContent.ItemType<BlahPotion>();

    public static bool ItemIsHerb(Item item) => LargeHerbSeedIdByHerbSeedId.ContainsKey(item.type) ||
                                                LargeHerbIdByLargeHerbSeedId.ContainsValue(item.type) ||
                                                LargeHerbIdByLargeHerbSeedId.ContainsKey(item.type) ||
                                                HerbIdByLargeHerbId.ContainsValue(item.type);
}
