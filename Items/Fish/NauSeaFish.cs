using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Fish;

public class NauSeaFish : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.maxStack = 9999;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Blue;
        Item.value = 7500;
    }
}
