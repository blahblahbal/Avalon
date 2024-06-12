using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Expert;

class CrystalSkull : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
        Item.defense = 4;
		Item.expert = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
		player.GetModPlayer<AvalonPlayer>().CrystalSkull = true;
		Lighting.AddLight(player.position, 1.5f, 1.5f, 1.5f);
    }
}
