using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Neck)]
public class TourmalineAmulet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 70);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetCritChance(DamageClass.Generic) += 5;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>(), 12)
			.AddIngredient(ItemID.Chain)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
