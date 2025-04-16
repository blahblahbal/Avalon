using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class CordycepsLeggings : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(3);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 60);
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.04f;
		player.maxMinions++;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.TropicalShroomCap>(), 8)
			.AddIngredient(ModContent.ItemType<Material.Root>(), 2)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
