using Avalon.Items.Material.Herbs;
using Avalon.Items.Placeable.Wall;
using Avalon.Items.Potions.Buff;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Items.Weapons.Melee.Hardmode;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Data.Sets
{
    internal class Item
    {
        public static readonly bool[] HerbSeeds = ItemID.Sets.Factory.CreateBoolSet(
            ItemID.BlinkrootSeeds,
            ItemID.DaybloomSeeds,
            ItemID.WaterleafSeeds,
            ItemID.FireblossomSeeds,
            ItemID.DeathweedSeeds,
            ItemID.MoonglowSeeds,
            ItemID.ShiverthornSeeds,
            ModContent.ItemType<BarfbushSeeds>(),
            ModContent.ItemType<BloodberrySeeds>(),
            ModContent.ItemType<SweetstemSeeds>(),
            ModContent.ItemType<HolybirdSeeds>(),
            ModContent.ItemType<TwilightPlumeSeeds>());

        public static List<int> CraftingStationsItemID = new List<int>();

        public static readonly bool[] Breakdawn = ItemID.Sets.Factory.CreateBoolSet(
            ModContent.ItemType<Breakdawn>()
        );

        public static readonly bool[] DungeonWallItems = ItemID.Sets.Factory.CreateBoolSet(
            ItemID.PinkBrickWall,
            ItemID.PinkSlabWall,
            ItemID.PinkTiledWall,
            ModContent.ItemType<OrangeBrickWall>(),
            ModContent.ItemType<OrangeSlabWall>(),
            ModContent.ItemType<OrangeTiledWall>(),
            ModContent.ItemType<YellowBrickWall>(),
            ModContent.ItemType<YellowSlabWall>(),
            ModContent.ItemType<YellowTiledWall>(),
            ItemID.GreenBrickWall,
            ItemID.GreenSlabWall,
            ItemID.GreenTiledWall,
            ItemID.BlueBrickWall,
            ItemID.BlueSlabWall,
            ItemID.BlueTiledWall,
            ModContent.ItemType<PurpleBrickWall>(),
            ModContent.ItemType<PurpleSlabWall>(),
            ModContent.ItemType<PurpleTiledWall>()
        );

        public static readonly bool[] Longbows = ItemID.Sets.Factory.CreateBoolSet(
            ModContent.ItemType<Longbow>(),
            ModContent.ItemType<Longbone>(),
            ModContent.ItemType<RhodiumLongbow>(),
            ModContent.ItemType<OsmiumLongbow>(),
            ModContent.ItemType<IridiumLongbow>(),
            ModContent.ItemType<Moonforce>()
        );

        public static readonly bool[] EarthRelatedItems = ItemID.Sets.Factory.CreateBoolSet(
            ItemID.StaffofEarth,
            ItemID.Picksaw,
            ItemID.HeatRay,
            ItemID.GolemFist,
            ItemID.Stynger,
            ItemID.PossessedHatchet,
            ModContent.ItemType<PossessedFlamesaw>()
        );

        public static bool[] Herbs = ItemID.Sets.Factory.CreateBoolSet(
            ItemID.Daybloom,
            ItemID.Blinkroot,
            ItemID.Moonglow,
            ItemID.Waterleaf,
            ItemID.Fireblossom,
            ItemID.Deathweed,
            ItemID.Shiverthorn,
            ModContent.ItemType<Bloodberry>(),
            ModContent.ItemType<Sweetstem>(),
            ModContent.ItemType<Holybird>(),
            ModContent.ItemType<Barfbush>(),
            ModContent.ItemType<TwilightPlume>()
        );

        public static bool[] Potions = ItemID.Sets.Factory.CreateBoolSet(
            ItemID.AmmoReservationPotion, ItemID.ArcheryPotion, ModContent.ItemType<AuraPotion>(),
            ItemID.BattlePotion, ModContent.ItemType<BloodCastPotion>(),
            ItemID.BuilderPotion, ItemID.CalmingPotion, ItemID.CratePotion, ItemID.TrapsightPotion,
            ItemID.EndurancePotion, ItemID.FeatherfallPotion, ItemID.FishingPotion, ItemID.FlipperPotion,
            ModContent.ItemType<ForceFieldPotion>(), ModContent.ItemType<FuryPotion>(),
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
        );
    }
}
