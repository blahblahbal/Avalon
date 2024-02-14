using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class OutpostKey : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.maxStack = 9999;
        Item.width = dims.Width;
        Item.value = 0;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Lime;
    }
}
