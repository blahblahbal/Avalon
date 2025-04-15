using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Neck)]
public class ForsakenCross : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 3);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.longInvince = true;
		if (player.immune)
		{
			player.GetCritChance(DamageClass.Generic) += 7;
			player.GetDamage(DamageClass.Generic) += 0.07f;
		}
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<ForsakenRelic>())
			.AddIngredient(ItemID.CrossNecklace)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
