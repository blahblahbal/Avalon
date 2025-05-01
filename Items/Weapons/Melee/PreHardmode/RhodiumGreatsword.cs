using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class RhodiumGreatsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(26, 5f, 20, crit: 6);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.RhodiumBar>(), 14)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 3)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
