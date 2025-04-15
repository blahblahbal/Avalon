using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class IridiumPants : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(8);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Magic) += 0.11f;
		player.statManaMax2 += 40;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 17)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 5)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
