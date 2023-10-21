using System.Collections.Generic;
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

        public static readonly HashSet<int> Stalac = new(){ModContent.TileType<ContagionStalactgmites>()};

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
    }
}
