using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.WoodBows;

public class CoughwoodBow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBow(9, 0f, 7f, 27, 27, width: 12, height: 28);
		Item.value = Item.sellPrice(copper: 20);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Tiles.Contagion.Coughwood.Coughwood>(), 10)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
