using Avalon.Items.Material.Herbs;
using Avalon.Items.Placeable.Wall;
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
    }
}
