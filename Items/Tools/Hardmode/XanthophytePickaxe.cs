using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class XanthophytePickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(202, 40, 5f, 7, 25, 1, 1.15f);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 4, 32);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 18)
			.AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void HoldItem(Player player)
	{
		if (player.inventory[player.selectedItem].type == Type)
		{
			player.pickSpeed -= 0.05f;
		}
	}
}
