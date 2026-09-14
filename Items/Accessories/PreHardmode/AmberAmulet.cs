using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Neck)]
public class AmberAmulet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 30);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.pickSpeed += 0.1f;
		player.moveSpeed += 0.1f;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Amber, 12)
			.AddIngredient(ItemID.Chain)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
