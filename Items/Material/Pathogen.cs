using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class Pathogen : ModItem
{
    public override void SetStaticDefaults()
    {
        //Tooltip.SetDefault("'Blech'");
        //SacrificeTotal = 25;
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 4500;
        Item.height = dims.Height;
    }
}
