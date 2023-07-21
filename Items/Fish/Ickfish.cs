using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Items.Fish;

class Ickfish : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.value = 10;
        Item.maxStack = 9999;
        Item.height = dims.Height;
    }
}
