using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace ExxoAvalonOrigins.Common
{
    internal class AvalonWorld : ModSystem
    {
        public enum CopperVariant
        {
            copper,
            tin,
            bronze,
            random
        }
        public enum IronVariant
        {
            iron,
            lead,
            nickel,
            random
        }
        public enum SilverVariant
        {
            silver,
            tungsten,
            zinc,
            random
        }
        public enum GoldVariant
        {
            gold,
            platinum,
            bismuth,
            random
        }
        public enum RhodiumVariant
        {
            rhodium,
            osmium,
            iridium,
            random
        }

        public static CopperVariant copperOre = CopperVariant.random;
        public static IronVariant ironOre = IronVariant.random;
        public static SilverVariant silverOre = SilverVariant.random;
        public static GoldVariant goldOre = GoldVariant.random;
        public static RhodiumVariant rhodiumOre = RhodiumVariant.random;

        public override void SaveWorldData(TagCompound tag)
        {
            tag["RhodiumVariant"] = rhodiumOre;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.ContainsKey("RhodiumVariant"))
            {
                rhodiumOre = tag.Get<RhodiumVariant>("RhodiumVariant");
            }
        }
    }
}
