using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Superhardmode;

[AutoloadEquip(EquipType.Head)]
class BlahsHeadguard : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 100;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(2);
        Item.height = dims.Height;
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<BlahsHauberk>() && legs.type == ModContent.ItemType<BlahsCuisses>();
    }

    public override void UpdateArmorSet(Player player)
    {
        //player.GetModPlayer<Players.ExxoEquipEffectPlayer>().BlahArmor = true;
        player.setBonus = "Melee and Ranged Stealth, Attackers also take double full damage, and Spectre Heal and Silence";
        //player.Avalon().meleeStealth = true;
        player.shroomiteStealth = true;
        //player.Avalon().doubleDamage = true;
        player.ghostHeal = true;
        //player.thorns = true;
        //player.Avalon().ghostSilence = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.35f;
        player.GetCritChance(DamageClass.Generic) += 11;
    }
}
