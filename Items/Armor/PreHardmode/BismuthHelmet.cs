using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class BismuthHelmet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(5);
		Item.value = Item.sellPrice(0, 0, 40);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<BismuthChainmail>() && legs.type == ModContent.ItemType<BismuthGreaves>();
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 20).AddTile(TileID.Anvils).Register();
	}
	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Bismuth");
		player.statDefense += 4;
	}
}
