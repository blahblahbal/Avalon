using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExxoAvalonOrigins.Common
{
    internal class AvalonWorld
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
        public static CopperVariant copperOre = CopperVariant.random;
        public static IronVariant ironOre = IronVariant.random;
        public static SilverVariant silverOre = SilverVariant.random;
        public static GoldVariant goldOre = GoldVariant.random;

    }
}
