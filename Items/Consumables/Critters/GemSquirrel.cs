using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables.Critters;

class PeridotSquirrel : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GemSquirrelSapphire);
        Item.makeNPC = ModContent.NPCType<NPCs.Critters.PeridotSquirrel>();
    }
}
class TourmalineSquirrel : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GemSquirrelAmethyst);
        Item.makeNPC = ModContent.NPCType<NPCs.Critters.TourmalineSquirrel>();
    }
}
class ZirconSquirrel : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GemSquirrelRuby);
        Item.makeNPC = ModContent.NPCType<NPCs.Critters.ZirconSquirrel>();
    }
}
