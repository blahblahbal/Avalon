using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.OreBows;
public class NickelBow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBow(10, 0f, 6.5f, 27, 27, width: 12, height: 28);
		Item.value = Item.sellPrice(silver: 3, copper: 60);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 7)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
