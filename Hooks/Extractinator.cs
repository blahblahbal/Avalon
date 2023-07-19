using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class Extractinator : ModHook
    {
        protected override void Apply()
        {
            On_Player.DropItemFromExtractinator += OnDropItemFromExtractinator;
        }
        private static void OnDropItemFromExtractinator(On_Player.orig_DropItemFromExtractinator orig, Player self, int itemType, int stack)
        {
            if (itemType is ItemID.TinOre or ItemID.CopperOre)
            {
                if (Main.rand.NextBool(2))
                {
                    itemType = ModContent.ItemType<Items.Material.Ores.BronzeOre>();
                }
            }
            else if (itemType is ItemID.IronOre or ItemID.LeadOre)
            {
                if (Main.rand.NextBool(2))
                {
                    itemType = ModContent.ItemType<Items.Material.Ores.NickelOre>();
                }
            }
            else if (itemType is ItemID.SilverOre or ItemID.TungstenOre)
            {
                if (Main.rand.NextBool(2))
                {
                    itemType = ModContent.ItemType<Items.Material.Ores.ZincOre>();
                }
            }
            else if (itemType is ItemID.GoldOre or ItemID.PlatinumOre)
            {
                int rn = Main.rand.Next(4);
                if (Main.rand.NextBool(2))
                {
                    if (rn == 0)
                    {
                        itemType = ModContent.ItemType<Items.Material.Ores.BismuthOre>();
                    }
                    else if (rn == 1)
                    {
                        itemType = ModContent.ItemType<Items.Material.Ores.RhodiumOre>();
                    }
                    else if (rn == 2)
                    {
                        itemType = ModContent.ItemType<Items.Material.Ores.OsmiumOre>();
                    }
                    else if (rn == 3)
                    {
                        itemType = ModContent.ItemType<Items.Material.Ores.IridiumOre>();
                    }
                }
            }
            else if (itemType is ItemID.Topaz or ItemID.Ruby or ItemID.Amethyst or ItemID.Diamond or ItemID.Emerald or ItemID.Sapphire)
            {
                if (Main.rand.NextBool(3))
                {
                    switch (Main.rand.Next(6))
                    {
                        case 0:
                            itemType = ModContent.ItemType<Items.Material.Ores.Tourmaline>();
                            break;
                        case 1:
                            itemType = ModContent.ItemType<Items.Material.Ores.Peridot>();
                            break;
                        case 2:
                            itemType = ModContent.ItemType<Items.Material.Ores.Zircon>();
                            break;
                        case 3:
                            itemType = ModContent.ItemType<Items.Material.Ores.Heartstone>();
                            break;
                        case 4:
                            itemType = ModContent.ItemType<Items.Material.Ores.Boltstone>();
                            break;
                        case 5:
                            itemType = ModContent.ItemType<Items.Material.Ores.Starstone>();
                            break;
                    }
                }
            }
            // add HM ores later
            orig(self, itemType, stack);
        }
    }
}
