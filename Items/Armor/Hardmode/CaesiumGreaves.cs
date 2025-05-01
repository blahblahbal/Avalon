using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
public class CaesiumGreaves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(21);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 8);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<CaesiumBar>(), 28)
			.AddIngredient(ItemID.HellstoneBar, 9)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddTile(TileID.MythrilAnvil).Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
		player.moveSpeed += 0.15f;
	}
}
