using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
class XanthophyteHat : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 8;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 6);
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<XanthophytePlate>() && legs.type == ModContent.ItemType<XanthophyteLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Xanthophyte",
			Language.GetTextValue("Mods.Avalon.MagicText"),
			Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
		player.GetModPlayer<AvalonPlayer>().XanthophyteFossil = true;
		player.GetAttackSpeed(DamageClass.Magic) += 0.1f;

	}

    public override void UpdateEquip(Player player)
    {
        player.manaCost -= 0.15f;
        player.statManaMax2 += 100;
        player.GetDamage(DamageClass.Magic) += 0.18f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 12)
            .AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
