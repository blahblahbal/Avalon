using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Fish.Quest;

class Snotpiranha : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.value = 10;
        Item.maxStack = 1;
        Item.height = dims.Height;
        Item.questItem = true;
        Item.rare = ItemRarityID.Quest;
        Item.uniqueStack = true;
    }
    public override bool IsQuestFish()
    {
        return true;
    }
    public override bool IsAnglerQuestAvailable()
    {
        return ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion;
    }

    public override void AnglerQuestChat(ref string description, ref string catchLocation)
    {
        description = "Mods.Avalon.QuestFish.Snotpiranha.Description";
        catchLocation = "Mods.Avalon.QuestFish.Pathofish.CatchLocation";
    }
}
