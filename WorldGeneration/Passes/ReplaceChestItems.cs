using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Consumables;
using Avalon.Items.Weapons.Magic.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes;

public class ReplaceChestItems : GenPass
{
    public ReplaceChestItems() : base("ReplaceChestItems", 10)
    {

    }
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        foreach (Chest c in Main.chest)
        {
            if (c != null)
            {
                foreach (Item i in c.item)
                {
                    if (i != null && (i.type == ItemID.IceBlade || i.type == ItemID.SnowballCannon ||
                        i.type == ItemID.IceBoomerang || i.type == ItemID.IceSkates ||
                        i.type == ItemID.BlizzardinaBottle || i.type == ItemID.FlurryBoots) &&
                        WorldGen.genRand.NextBool(7))
                    {
                        i.SetDefaults(ModContent.ItemType<GlacierStaff>());
                        i.Prefix(-1);
                    }
                    if (i != null && (i.type == ItemID.IceBlade || i.type == ItemID.SnowballCannon ||
                        i.type == ItemID.IceBoomerang || i.type == ItemID.IceSkates ||
                        i.type == ItemID.BlizzardinaBottle || i.type == ItemID.FlurryBoots) &&
                        WorldGen.genRand.NextBool(7))
                    {
                        i.SetDefaults(ModContent.ItemType<FrozenLyre>());
                        i.Prefix(-1);
                    }
                    //if (i != null && ModContent.GetInstance<AvalonWorld>().WorldEvil == Enums.WorldEvil.Contagion)
                    //{
                    //    if (i.type == ItemID.ScourgeoftheCorruptor)
                    //    {
                    //        i.SetDefaults(ModContent.ItemType<VirulentScythe>());
                    //        i.Prefix(-1);
                    //    }
                    //}
                    //if (i != null && i.type == ItemID.EnchantedBoomerang)
                    //{
                    //    i.SetDefaults(ModContent.ItemType<EnchantedBar>());
                    //    i.stack = WorldGen.genRand.Next(35, 49);
                    //}
                    //if (WorldBiomeManager.WorldJungle == "Avalon/TropicsAlternateBiome")
                    //{
                    //    if (i?.type == ItemID.Boomstick)
                    //    {
                    //        i.SetDefaults(ModContent.ItemType<Thompson>());
                    //        i.Prefix(-1);
                    //    }
                    //}
                    if (i?.type == ItemID.StaffofRegrowth || i?.type == ItemID.FeralClaws || i?.type == ItemID.AnkletoftheWind || i?.type == ItemID.Boomstick)
                    {
                        if (WorldGen.genRand.NextBool(5))
                        {
                            i.SetDefaults(ModContent.ItemType<FlowerofTheJungle>());
                            i.Prefix(-1);
                        }
                    }
                    if (Main.tile[c.x, c.y].TileFrameX >= 288 && Main.tile[c.x, c.y].TileFrameX < 324 && (Main.tile[c.x, c.y].TileType == 21 || Main.tile[c.x, c.y].TileType == 441))
                    {
                        if (i?.type == ItemID.BandofRegeneration)
                        {
                            if (WorldGen.genRand.NextBool(2))
                            {
                                i.SetDefaults(ModContent.ItemType<BandofStamina>());
                                i.Prefix(-1);
                            }
                        }
                    }
                    if (Main.tile[c.x, c.y].TileFrameX >= 360 && Main.tile[c.x, c.y].TileFrameX < 396 && (Main.tile[c.x, c.y].TileType == 21 || Main.tile[c.x, c.y].TileType == 441))
                    {
                        if (i?.type == ItemID.BandofRegeneration)
                        {
                            i.SetDefaults(ModContent.ItemType<BandofStamina>());
                            i.Prefix(-1);
                        }
                    }

                    if (Main.tile[c.x, c.y].TileFrameX >= 72 && Main.tile[c.x, c.y].TileFrameX < 108 && Main.tile[c.x, c.y].TileType == 21)
                    {
                        if (i?.type == ItemID.SuspiciousLookingEye)
                        {
                            i.SetDefaults(ModContent.ItemType<DesertHorn>());
                        }
                    }
                }
            }
        }
    }
}
