using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class OsmiumTreads : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(8);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Magic) += 0.12f;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<OsmiumBar>(), 17)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 5)
			.AddTile(TileID.Anvils).Register();
	}
}
