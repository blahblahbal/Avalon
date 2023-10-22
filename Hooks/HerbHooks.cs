using Avalon.Common;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Avalon.Items.Material.Herbs;
using Terraria.ID;

namespace Avalon.Hooks
{
    internal class HerbHooks : ModHook
    {
        protected override void Apply()
        {
            On_ItemDropDatabase.RegisterHerbBag += OnRegisterHerbBag;
            On_WorldGen.PlaceAlch += On_WorldGen_PlaceAlch;
        }

        private bool On_WorldGen_PlaceAlch(On_WorldGen.orig_PlaceAlch orig, int x, int y, int style)
        {
            if (style == 3 && (Main.tile[x, y + 1].TileType == TileID.Crimstone || Main.tile[x, y + 1].TileType == TileID.CrimsonGrass))
                return false;
            if (style == 0 && Main.tile[x, y + 1].TileType == TileID.HallowedGrass)
                return false;
            return orig(x, y, style);
        }

        private void OnRegisterHerbBag(On_ItemDropDatabase.orig_RegisterHerbBag orig, ItemDropDatabase self)
        {
            self.RegisterToItem(3093, new HerbBagDropsItemDropRule(313, 314, 315, 317, 316, 318, 2358, 307, 308, 309, 311, 310, 312, 2357,
                ModContent.ItemType<Barfbush>(), ModContent.ItemType<BarfbushSeeds>(), ModContent.ItemType<Bloodberry>(), ModContent.ItemType<BloodberrySeeds>(),
                ModContent.ItemType<Sweetstem>(), ModContent.ItemType<SweetstemSeeds>()));
        }
    }
}
