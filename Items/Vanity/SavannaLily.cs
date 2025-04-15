using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
public class SavannaLily : ModItem
{
    public override void SetStaticDefaults()
	{
        ArmorIDs.Head.Sets.DrawFullHair[EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "SavannaLily", EquipType.Head)] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.vanity = true;
        Item.value = Item.sellPrice(copper: 20);
        Item.height = dims.Height;
    }
}
