using Avalon.Items.Material.Bars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode; 

[AutoloadEquip(EquipType.Head)]
public class OsmiumHelmet : ModItem
{
    public override void SetDefaults()
    {
        Item.defense = 8;
        Item.rare = ItemRarityID.Orange;
        Item.Size = new Vector2(32);
        Item.value = Item.sellPrice(0, 1, 20, 0);
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<OsmiumJerkin>() && legs.type == ModContent.ItemType<OsmiumTreads>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Osmium");
        player.statDefense += 5;
    }
    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 40;
        player.GetDamage(DamageClass.Ranged) += 0.12f;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<OsmiumBar>(), 15)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 4)
            .AddTile(TileID.Anvils).Register();
    }
}
