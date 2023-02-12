using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.IO;
using ExxoAvalonOrigins.Items.Weapons.Magic.PreHardmode;

namespace ExxoAvalonOrigins.WorldGeneration.Passes;

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
                    if (i != null && (i.type == ItemID.IceMirror || i.type == ItemID.Fish ||
                        i.type == ItemID.IceBlade || i.type == ItemID.SnowballCannon ||
                        i.type == ItemID.IceBoomerang || i.type == ItemID.IceSkates ||
                        i.type == ItemID.BlizzardinaBottle || i.type == ItemID.FlurryBoots) &&
                        WorldGen.genRand.NextBool(5))
                    {
                        i.SetDefaults(ModContent.ItemType<GlacierStaff>());
                        i.Prefix(-1);
                    }
                    if (i != null && (i.type == ItemID.IceMirror || i.type == ItemID.Fish ||
                        i.type == ItemID.IceBlade || i.type == ItemID.SnowballCannon ||
                        i.type == ItemID.IceBoomerang || i.type == ItemID.IceSkates ||
                        i.type == ItemID.BlizzardinaBottle || i.type == ItemID.FlurryBoots) &&
                        WorldGen.genRand.NextBool(5))
                    {
                        i.SetDefaults(ModContent.ItemType<FrozenLyre>());
                        i.Prefix(-1);
                    }
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
                    if (i?.type == ItemID.StaffofRegrowth && WorldGen.genRand.NextBool(2))
                    {
                        i.SetDefaults(ModContent.ItemType<FlowerofTheJungle>());
                        i.Prefix(-1);
                    }
                }
            }
        }
    }
}
