using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
public class DurataniumHeadgear : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(4);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 40);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return (body.type == ModContent.ItemType<DurataniumChainmail>() || body.type == ModContent.ItemType<AncientDurataniumChainmail>()) && (legs.type == ModContent.ItemType<DurataniumGreaves>() || legs.type == ModContent.ItemType<AncientDurataniumGreaves>());
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Duratanium");
		player.GetModPlayer<AvalonPlayer>().DefDebuff = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Magic) += 0.06f;
		player.manaCost -= 0.05f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 10)
			.AddTile(TileID.Anvils).Register();
	}
}
[AutoloadEquip(EquipType.Head)]
public class AncientDurataniumHeadgear : DurataniumHeadgear
{
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 10)
			.AddTile(TileID.DemonAltar).Register();
	}
}
