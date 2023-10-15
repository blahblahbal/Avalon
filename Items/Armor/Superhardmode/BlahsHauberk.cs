using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Superhardmode;

[AutoloadEquip(EquipType.Body)]
class BlahsHauberk : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 100;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(2, 0, 0, 0);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.aggro += 1000;
        player.manaCost -= 0.3f;
        player.statManaMax2 += 800;
        player.maxMinions += 12;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return (head.type == ModContent.ItemType<BlahsHeadguard>() && body.type == ModContent.ItemType<BlahsHauberk>() && legs.type == ModContent.ItemType<BlahsCuisses>());
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Melee and Ranged Stealth, Go Berserk, Rosebuds, Spectrum Speed, Attackers also take double full damage, and Spectre Heal";
        //player.Avalon().doubleDamage = player.ghostHeal = player.Avalon().ghostSilence = player.Avalon().meleeStealth = player.shroomiteStealth = true;
        //player.Avalon().spectrumSpeed = player.Avalon().goBerserk = player.Avalon().roseMagic = true;
    }
}
