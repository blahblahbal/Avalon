using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tokens;

class ContagionToken : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("Tokens");
    }
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
        //DisplayName.SetDefault("Contagion Token");
        //Tooltip.SetDefault("Used to make things\nGathered in the Contagion, having defeated a boss");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.rare = ItemRarityID.Blue;
        Item.maxStack = 9999;
        Item.value = 0;
        Item.height = dims.Height;
    }
}
