using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
class AncientTitaniumHeadgear : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 7;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.value = 100000;
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return (body.type == ModContent.ItemType<AncientTitaniumPlateMail>() || body.type == ModContent.ItemType<RhodiumPlateMail>()) &&
            (legs.type == ModContent.ItemType<AncientTitaniumGreaves>() || legs.type == ModContent.ItemType<RhodiumGreaves>());
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "9% increased damage";
        player.GetDamage(DamageClass.Generic) += 0.09f;
    }

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 40;
        player.GetDamage(DamageClass.Ranged) += 0.14f;
    }
}
