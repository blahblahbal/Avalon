using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
public class DurataniumGreaves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(8);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 30);
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += 0.05f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 15)
			.AddTile(TileID.Anvils).Register();
	}
}
[AutoloadEquip(EquipType.Legs)]
public class AncientDurataniumGreaves : DurataniumGreaves
{
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 15)
			.AddTile(TileID.DemonAltar).Register();
	}
}
