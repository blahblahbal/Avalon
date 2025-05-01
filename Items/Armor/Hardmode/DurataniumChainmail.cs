using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
public class DurataniumChainmail : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(10);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 60);
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Generic) += 0.05f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 20)
			.AddTile(TileID.Anvils).Register();
	}
}
[AutoloadEquip(EquipType.Body)]
public class AncientDurataniumChainmail : DurataniumChainmail
{
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 20)
			.AddTile(TileID.DemonAltar).Register();
	}
}
