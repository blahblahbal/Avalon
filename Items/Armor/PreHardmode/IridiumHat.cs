using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class IridiumHat : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(7);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1, 20);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<IridiumPlateMail>() && legs.type == ModContent.ItemType<IridiumPants>();
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Iridium");
		player.GetCritChance<GenericDamageClass>() += 9;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Ranged) += 0.11f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 15)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 4)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
