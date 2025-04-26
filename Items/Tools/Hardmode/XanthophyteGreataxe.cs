using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class XanthophyteGreataxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAxe(115, 72, 7f, 7, 30, 1);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 4, 32);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 18)
			.AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
