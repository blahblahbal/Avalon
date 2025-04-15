using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
public class FleshWrappings : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(9);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 16)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.GetKnockback(DamageClass.Summon) += 0.09f;
		player.moveSpeed += 0.1f;
	}
}
