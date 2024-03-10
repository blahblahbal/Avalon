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
        return true;
    }

    public override void AnglerQuestChat(ref string description, ref string catchLocation)
    {
        description = "I don't even want to tell you about this one. It's so nasty, it looks like boogers! Go and catch one, but make sure you bag it before you give it to me. Ewww!";
        catchLocation = "Caught in the Contagion";
    }
}
