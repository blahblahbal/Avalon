using Avalon.Common.Extensions;
using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class ResistantWoodHelmet : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToArmor(3);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<ResistantWood>(), 25)
			.AddTile(TileID.WorkBenches)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<ResistantWoodBreastplate>())
			.Register();
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<ResistantWoodBreastplate>() && legs.type == ModContent.ItemType<ResistantWoodGreaves>();
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.ResistantWood");
		player.endurance += 0.1f;
		player.GetArmorPenetration(DamageClass.Generic) += 0.1f;
	}
}
