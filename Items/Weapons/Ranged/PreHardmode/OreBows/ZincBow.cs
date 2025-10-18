using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.OreBows;
public class ZincBow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBow(11, 0f, 6.6f, 25, 25, width: 12, height: 28);
		Item.value = Item.sellPrice(silver: 9);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 7)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
