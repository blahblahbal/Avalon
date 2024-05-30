using System.Collections.Generic;
using Avalon.Items.Consumables;
using Avalon.Items.Material.Bars;
using Avalon.Items.OreChunks;
using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.Ores;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Data.Sets
{
    internal class Tile
    {
        public static List<int> ThreeOrePerBar = new() { TileID.Copper, TileID.Tin, TileID.Meteorite,
            TileID.Demonite, TileID.Crimtane, TileID.Hellstone, TileID.Cobalt, TileID.Palladium, TileID.Iron, TileID.Lead,
            //ModContent.TileType<BronzeOre>(), ModContent.TileType<NickelOre>(), ModContent.TileType<DurataniumOre>(),
            //ModContent.TileType<BacciliteOre>()
        };

        public static List<int> FourOrePerBar = new() { TileID.Silver, TileID.Tungsten, TileID.Gold,
            TileID.Platinum, TileID.Mythril, TileID.Orichalcum, TileID.LunarOre, //ModContent.TileType<ZincOre>(),
            //ModContent.TileType<BismuthOre>(), ModContent.TileType<NaquadahOre>(), ModContent.TileType<RhodiumOre>(),
            //ModContent.TileType<OsmiumOre>(), ModContent.TileType<IridiumOre>()
        };

        public static List<int> FiveOrePerBar = new() { TileID.Adamantite, TileID.Titanium, TileID.Chlorophyte,
            //ModContent.TileType<TroxiniumOre>(), ModContent.TileType<HallowedOre>(), ModContent.TileType<ShroomiteOre>(),
			//ModContent.TileType<PyroscoricOre>(), ModContent.TileType<TritanoriumOre>(), ModContent.TileType<XanthophyteOre>()
        };

		//public static List<int> SixOrePerBar = new() { ModContent.TileType<UnvolanditeOre>(), ModContent.TileType<VorazylcumOre>() };

		//public static List<int> EightOrePerBar = new() { ModContent.TileType<CaesiumOre>() };

		public static List<ushort> LivingBlocks = new()
        {
            TileID.LivingFire, TileID.LivingCursedFire, TileID.LivingDemonFire, TileID.LivingFrostFire, TileID.LivingIchor,
            TileID.LivingUltrabrightFire, (ushort)ModContent.TileType<LivingLightning>()
        };

        public static readonly bool[] Chunkstone = TileID.Sets.Factory.CreateBoolSet(
            ModContent.TileType<Chunkstone>()
        );

        public static readonly bool[] Altar = TileID.Sets.Factory.CreateBoolSet(
            ModContent.TileType<IckyAltar>()
        );

        public static readonly bool[] Orb = TileID.Sets.Factory.CreateBoolSet(
            ModContent.TileType<SnotOrb>()
        );

        public static readonly HashSet<int> Stalac = new() { ModContent.TileType<ContagionStalactgmites>() };

        public static readonly HashSet<int> IckyAltar = new() { ModContent.TileType<IckyAltar>() };

        public static bool[] SuitableForPlantingHerbs = TileID.Sets.Factory.CreateBoolSet(TileID.ClayPot, TileID.PlanterBox);

        public static bool[] AvalonPlanterBoxes = TileID.Sets.Factory.CreateBoolSet(
            ModContent.TileType<Tiles.Herbs.BarfbushPlanterBox>(),
            ModContent.TileType<Tiles.Herbs.SweetstemPlanterBox>(),
            ModContent.TileType<Tiles.Herbs.TwilightPlumePlanterBox>(),
            ModContent.TileType<Tiles.Herbs.HolybirdPlanterBox>()
        );
        public class Conversion
        {
            public static bool[] ShortGrass = TileID.Sets.Factory.CreateBoolSet(
            TileID.Plants,
            TileID.CorruptPlants,
            TileID.CrimsonPlants,
            TileID.HallowedPlants,
            TileID.JunglePlants
            );
        }
        
        public static bool[] RiftOres = TileID.Sets.Factory.CreateBoolSet(TileID.Copper, TileID.Tin,
            TileID.Iron, TileID.Lead, TileID.Silver, TileID.Tungsten, TileID.Gold, TileID.Platinum,
            TileID.Demonite, TileID.Crimtane, TileID.Cobalt, TileID.Palladium, TileID.Mythril, TileID.Orichalcum,
            TileID.Adamantite, TileID.Titanium);

        public static List<int> CraftingStations = new List<int>();

        public static readonly bool[] NoPlacingGemStashesOnThese = TileID.Sets.Factory.CreateBoolSet(
            TileID.RollingCactus,
            TileID.BreakableIce,
            TileID.IceBlock,
            TileID.Sand,
            TileID.SnowBlock,
            TileID.Ebonstone,
            TileID.Crimstone,
            TileID.Dirt
        );

        public static readonly bool[] OnlyPlaceGemStashesOnThese = TileID.Sets.Factory.CreateBoolSet(
            TileID.Stone, TileID.Sandstone
        );

        /// <summary>
        /// Used to add extra tiles to the Contagion's worldgen, such as modded grass tiles converting to ickgrass tiles upon hardmode
        /// <br/> Example:
        /// <br/> Data.Sets.Tiles.ConvertsToContagion[Type] = TileID.Clay;
        /// <br/> This will turn the modded tile of choice into clay blocks if the contagion's worldgen hits directly ontop of the modded tile
        /// </summary>
        public static int[] ConvertsToContagion = TileID.Sets.Factory.CreateIntSet(-1);

        //public static Dictionary<int, bool> RiftOres = new Dictionary<int, bool>
        //{
        //    { TileID.Copper, true },
        //    { TileID.Tin, true },
        //    { ModContent.TileType<BronzeOre>(), true },
        //    { TileID.Iron, true },
        //    { TileID.Lead, true },
        //    { ModContent.TileType<NickelOre>(), true },
        //    { TileID.Silver, true },
        //    { TileID.Tungsten, true },
        //    { ModContent.TileType<ZincOre>(), true },
        //    { TileID.Gold, true },
        //    { TileID.Platinum, true },
        //    { ModContent.TileType<BismuthOre>(), true },
        //    { TileID.Demonite, true },
        //    { TileID.Crimtane, true },
        //    { ModContent.TileType<BacciliteOre>(), true },
        //    { ModContent.TileType<RhodiumOre>(), true },
        //    { ModContent.TileType<OsmiumOre>(), true },
        //    { ModContent.TileType<IridiumOre>(), true },
        //};

        public static readonly Dictionary<int, int> OresToChunks = new Dictionary<int, int>
        {
            { TileID.Copper, ModContent.ItemType<CopperChunk>() },
            { TileID.Tin, ModContent.ItemType<TinChunk>() },
            { ModContent.TileType<BronzeOre>(), ModContent.ItemType<BronzeChunk>() },
            { TileID.Iron, ModContent.ItemType<IronChunk>() },
            { TileID.Lead, ModContent.ItemType<LeadChunk>() },
            { ModContent.TileType<NickelOre>(), ModContent.ItemType<NickelChunk>() },
            { TileID.Silver, ModContent.ItemType<SilverChunk>() },
            { TileID.Tungsten, ModContent.ItemType<TungstenChunk>() },
            { ModContent.TileType<ZincOre>(), ModContent.ItemType<ZincChunk>() },
            { TileID.Gold, ModContent.ItemType<GoldChunk>() },
            { TileID.Platinum, ModContent.ItemType<PlatinumChunk>() },
            { TileID.Demonite, ModContent.ItemType<DemoniteChunk>() },
            { TileID.Crimtane, ModContent.ItemType<CrimtaneChunk>() },
            { ModContent.TileType<BacciliteOre>(), ModContent.ItemType<BacciliteChunk>() },
            { ModContent.TileType<BismuthOre>(), ModContent.ItemType<BismuthChunk>() },
            { ModContent.TileType<RhodiumOre>(), ModContent.ItemType<RhodiumChunk>() },
            { ModContent.TileType<OsmiumOre>(), ModContent.ItemType<OsmiumChunk>() },
            { ModContent.TileType<IridiumOre>(), ModContent.ItemType<IridiumChunk>() },
            { TileID.Hellstone, ModContent.ItemType<HellstoneChunk>() },
            { TileID.Cobalt, ModContent.ItemType<CobaltChunk>() },
            { TileID.Palladium, ModContent.ItemType<PalladiumChunk>() },
            { ModContent.TileType<DurataniumOre>(), ModContent.ItemType<DurataniumChunk>() },
            { TileID.Mythril, ModContent.ItemType<MythrilChunk>() },
            { TileID.Orichalcum, ModContent.ItemType<OrichalcumChunk>() },
            { ModContent.TileType<NaquadahOre>(), ModContent.ItemType<NaquadahChunk>() },
            { TileID.Adamantite, ModContent.ItemType<AdamantiteChunk>() },
            { TileID.Titanium, ModContent.ItemType<TitaniumChunk>() },
            { ModContent.TileType<TroxiniumOre>(), ModContent.ItemType<TroxiniumChunk>() },
            { ModContent.TileType<HallowedOre>(), ModContent.ItemType<HallowedChunk>() },
            //{ ModContent.TileType<FeroziumOre>(), ModContent.ItemType<FeroziumChunk>() },
            { TileID.Chlorophyte, ModContent.ItemType<ChlorophyteChunk>() },
            //{ ModContent.TileType<XanthophyteOre>(), ModContent.ItemType<XanthophyteChunk>() },
            //{ ModContent.TileType<ShroomiteOre>(), ModContent.ItemType<ShroomiteChunk>() },
            { ModContent.TileType<CaesiumOre>(), ModContent.ItemType<CaesiumChunk>() },
            //{ ModContent.TileType<PyroscoricOre>(), ModContent.ItemType<PyroscoricChunk>() },
            //{ ModContent.TileType<TritanoriumOre>(), ModContent.ItemType<TritanoriumChunk>() },
            //{ ModContent.TileType<UnvolanditeOre>(), ModContent.ItemType<UnvolanditeChunk>() },
            //{ ModContent.TileType<VorazylcumOre>(), ModContent.ItemType<VorazylcumChunk>() },
            //{ ModContent.TileType<HydrolythOre>(), ModContent.ItemType<HydrolythChunk>() }
        };

        public static readonly Dictionary<int, int> OresToBars = new Dictionary<int, int>
        {
            { TileID.Copper, ItemID.CopperBar },
            { TileID.Tin, ItemID.TinBar },
            { ModContent.TileType<BronzeOre>(), ModContent.ItemType<BronzeBar>() },
            { TileID.Iron, ItemID.IronBar },
            { TileID.Lead, ItemID.LeadBar },
            { ModContent.TileType<NickelOre>(), ModContent.ItemType<NickelBar>() },
            { TileID.Silver, ItemID.SilverBar },
            { TileID.Tungsten, ItemID.TungstenBar },
            { ModContent.TileType<ZincOre>(), ModContent.ItemType<ZincBar>() },
            { TileID.Gold, ItemID.GoldBar },
            { TileID.Platinum, ItemID.PlatinumBar },
            { TileID.Demonite, ItemID.DemoniteBar },
            { TileID.Crimtane, ItemID.CrimtaneBar },
            { ModContent.TileType<BacciliteOre>(), ModContent.ItemType<BacciliteBar>() },
            { ModContent.TileType<BismuthOre>(), ModContent.ItemType<BismuthBar>() },
            { ModContent.TileType<RhodiumOre>(), ModContent.ItemType<RhodiumBar>() },
            { ModContent.TileType<OsmiumOre>(), ModContent.ItemType<OsmiumBar>() },
            { ModContent.TileType<IridiumOre>(), ModContent.ItemType<IridiumBar>() },
            { TileID.Hellstone, ItemID.HellstoneBar },
            { TileID.Cobalt, ItemID.CobaltBar },
            { TileID.Palladium, ItemID.PalladiumBar },
            { ModContent.TileType<DurataniumOre>(), ModContent.ItemType<DurataniumBar>() },
            { TileID.Mythril, ItemID.MythrilBar },
            { TileID.Orichalcum, ItemID.OrichalcumBar },
            { ModContent.TileType<NaquadahOre>(), ModContent.ItemType<NaquadahBar>() },
            { TileID.Adamantite, ItemID.AdamantiteBar },
            { TileID.Titanium, ItemID.TitaniumBar },
            { ModContent.TileType<TroxiniumOre>(), ModContent.ItemType<TroxiniumBar>() },
            { ModContent.TileType<HallowedOre>(), ItemID.HallowedBar },
            //{ ModContent.TileType<FeroziumOre>(), ModContent.ItemType<FeroziumBar>() },
            { TileID.Chlorophyte, ItemID.ChlorophyteBar },
            //{ ModContent.TileType<XanthophyteOre>(), ModContent.ItemType<XanthophyteBar>() },
            { ModContent.TileType<ShroomiteOre>(), ItemID.ShroomiteBar },
            { ModContent.TileType<CaesiumOre>(), ModContent.ItemType<CaesiumBar>() },
			//{ ModContent.TileType<PyroscoricOre>(), ModContent.ItemType<PyroscoricBar>() },
			//{ ModContent.TileType<TritanoriumOre>(), ModContent.ItemType<TritanoriumBar>() },
			//{ ModContent.TileType<UnvolanditeOre>(), ModContent.ItemType<UnvolanditeBar>() },
			//{ ModContent.TileType<VorazylcumOre>(), ModContent.ItemType<VorazylcumBar>() },
			//{ ModContent.TileType<HydrolythOre>(), ModContent.ItemType<HydrolythBar>() }
			{ ModContent.TileType<Boltstone>(), ModContent.ItemType<StaminaCrystal>() },
			{ ModContent.TileType<Heartstone>(), ItemID.LifeCrystal },
			{ ModContent.TileType<Starstone>(), ItemID.ManaCrystal }
		};

		public static readonly Dictionary<int, int> OreTilesToItems = new Dictionary<int, int>
		{
			{ TileID.Copper, ItemID.CopperOre },
			{ TileID.Tin, ItemID.TinOre },
			{ ModContent.TileType<BronzeOre>(), ModContent.ItemType<Items.Material.Ores.BronzeOre>() },
			{ TileID.Iron, ItemID.IronOre },
			{ TileID.Lead, ItemID.LeadOre },
			{ ModContent.TileType<NickelOre>(), ModContent.ItemType<Items.Material.Ores.NickelOre>() },
			{ TileID.Silver, ItemID.SilverOre },
			{ TileID.Tungsten, ItemID.TungstenOre },
			{ ModContent.TileType<ZincOre>(), ModContent.ItemType<Items.Material.Ores.ZincOre>() },
			{ TileID.Gold, ItemID.GoldOre },
			{ TileID.Platinum, ItemID.PlatinumOre },
			{ TileID.Demonite, ItemID.DemoniteOre },
			{ TileID.Crimtane, ItemID.CrimtaneOre },
			{ ModContent.TileType<BacciliteOre>(), ModContent.ItemType<Items.Material.Ores.BacciliteOre>() },
			{ ModContent.TileType<BismuthOre>(), ModContent.ItemType<Items.Material.Ores.BismuthOre>() },
			{ ModContent.TileType<RhodiumOre>(), ModContent.ItemType<Items.Material.Ores.RhodiumOre>() },
			{ ModContent.TileType<OsmiumOre>(), ModContent.ItemType<Items.Material.Ores.OsmiumOre>() },
			{ ModContent.TileType<IridiumOre>(), ModContent.ItemType<Items.Material.Ores.IridiumOre>() },
			{ TileID.Hellstone, ItemID.Hellstone },
			{ TileID.Cobalt, ItemID.CobaltOre },
			{ TileID.Palladium, ItemID.PalladiumOre },
			{ ModContent.TileType<DurataniumOre>(), ModContent.ItemType<Items.Material.Ores.DurataniumOre>() },
			{ TileID.Mythril, ItemID.MythrilOre },
			{ TileID.Orichalcum, ItemID.OrichalcumOre },
			{ ModContent.TileType<NaquadahOre>(), ModContent.ItemType<Items.Material.Ores.NaquadahOre>() },
			{ TileID.Adamantite, ItemID.AdamantiteOre },
			{ TileID.Titanium, ItemID.TitaniumOre },
			{ ModContent.TileType<TroxiniumOre>(), ModContent.ItemType<Items.Material.Ores.TroxiniumOre>() },
			{ ModContent.TileType<HallowedOre>(), ModContent.ItemType<Items.Material.Ores.HallowedOre>() },
            //{ ModContent.TileType<FeroziumOre>(), ModContent.ItemType<Items.Material.Ores.FeroziumOre>() },
            { TileID.Chlorophyte, ItemID.ChlorophyteBar },
            //{ ModContent.TileType<XanthophyteOre>(), ModContent.ItemType<Items.Material.Ores.XanthophyteOre>() },
            { ModContent.TileType<ShroomiteOre>(), ModContent.ItemType<Items.Material.Ores.ShroomiteOre>() },
            { ModContent.TileType<CaesiumOre>(), ModContent.ItemType<Items.Material.Ores.CaesiumOre>() },
			//{ ModContent.TileType<PyroscoricOre>(), ModContent.ItemType<PyroscoricBar>() },
			//{ ModContent.TileType<TritanoriumOre>(), ModContent.ItemType<TritanoriumBar>() },
			//{ ModContent.TileType<UnvolanditeOre>(), ModContent.ItemType<UnvolanditeBar>() },
			//{ ModContent.TileType<VorazylcumOre>(), ModContent.ItemType<VorazylcumBar>() },
			//{ ModContent.TileType<HydrolythOre>(), ModContent.ItemType<HydrolythBar>() }
			{ ModContent.TileType<Boltstone>(), ModContent.ItemType<Items.Material.Ores.Boltstone>() },
			{ ModContent.TileType<Heartstone>(), ModContent.ItemType<Items.Material.Ores.Heartstone>() },
			{ ModContent.TileType<Starstone>(), ModContent.ItemType<Items.Material.Ores.Starstone>() }
		};
	}
}
