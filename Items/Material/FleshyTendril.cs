using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class FleshyTendril : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Fleshy Tendril");
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.value = 50;
        Item.maxStack = 9999;
        Item.height = dims.Height;
    }
}
