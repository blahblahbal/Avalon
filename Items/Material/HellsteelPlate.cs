using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class HellsteelPlate : ModItem
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
        Item.value = Item.sellPrice(0, 0, 2);
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.height = dims.Height;
    }
}
