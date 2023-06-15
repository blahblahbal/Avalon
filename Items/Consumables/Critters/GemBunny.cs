using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables.Critters;

class PeridotBunny : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GemBunnySapphire);
        Item.makeNPC = ModContent.NPCType<NPCs.Critters.PeridotBunny>();
    }
}
class TourmalineBunny : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GemBunnyAmethyst);
        Item.makeNPC = ModContent.NPCType<NPCs.Critters.TourmalineBunny>();
    }
}
class ZirconBunny : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GemBunnyRuby);
        Item.makeNPC = ModContent.NPCType<NPCs.Critters.ZirconBunny>();
    }
}
