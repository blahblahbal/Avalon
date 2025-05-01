using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class CordycepsWrappings : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(4);
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
			.AddIngredient(ModContent.ItemType<Material.TropicalShroomCap>(), 16)
			.AddIngredient(ModContent.ItemType<Material.MosquitoProboscis>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
