using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Material;

public class Root : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(silver: 1);
        Item.height = dims.Height;
    }
}
