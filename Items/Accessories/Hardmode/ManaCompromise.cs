using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class ManaCompromise : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 6, 70, 0);
        Item.accessory = true;
        Item.height = dims.Height;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.ManaSickness] = true;
        player.manaFlower = true;
        player.GetDamage(DamageClass.Magic) -= 0.12f;
        player.manaCost -= 0.08f;
    }
}
