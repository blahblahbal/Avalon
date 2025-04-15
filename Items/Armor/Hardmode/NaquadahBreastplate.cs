using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
public class NaquadahBreastplate : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(14);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 60);
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Generic) += 0.06f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 20)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
