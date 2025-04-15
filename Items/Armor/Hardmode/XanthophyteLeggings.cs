using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
public class XanthophyteLeggings : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(13);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 3, 60);
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance(DamageClass.Generic) += 9;
		player.moveSpeed += 0.1f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 18)
			.AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
