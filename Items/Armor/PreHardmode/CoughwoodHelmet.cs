using Avalon.Common.Extensions;
using Avalon.Tiles.Contagion.Coughwood;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class CoughwoodHelmet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(2);
		Item.value = Item.sellPrice(0, 0, 2);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<CoughwoodBreastplate>() && legs.type == ModContent.ItemType<CoughwoodGreaves>();
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Coughwood>(), 20)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.OneDef");
		player.statDefense++;
	}
}
