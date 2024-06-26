using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
class XanthophyteMask : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 14;
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
			Language.GetTextValue("Mods.Avalon.RangedText"),
			Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
		player.GetModPlayer<AvalonPlayer>().XanthophyteFossil = true;
		player.GetAttackSpeed(DamageClass.Ranged) += 0.1f;

	}

	public override void UpdateEquip(Player player)
    {
        player.ammoCost80 = true;
        player.GetDamage(DamageClass.Ranged) += 0.2f;
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
