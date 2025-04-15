using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class RhotukaSpinner : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		Item.DefaultToSpinner(-1, 0, 0f, 0f);
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 0, 0, 8);
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(5).AddIngredient(ItemID.WoodenArrow, 5).AddIngredient(ItemID.Vertebrae).AddTile(TileID.Anvils).Register();
	//}
}
