using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
public class XanthophyteMask : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(14);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 6);
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
