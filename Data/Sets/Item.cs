using Avalon.Items.Material.Herbs;
using Avalon.Items.Tools.PreHardmode;
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
    }
}
