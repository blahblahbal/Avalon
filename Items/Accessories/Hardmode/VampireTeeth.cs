using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class VampireTeeth : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.height = dims.Height;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().VampireTeeth = true;
    }
}
