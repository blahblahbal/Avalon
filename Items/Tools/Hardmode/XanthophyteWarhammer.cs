using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class XanthophyteWarhammer : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToHammer(90, 83, 8f, 14, 35, 1);
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
}
