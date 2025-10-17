using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.OreSwords;

public class NickelBroadsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(14, 5.5f, 20, false, width: 24, height: 28);
		Item.value = Item.sellPrice(silver: 4, copper: 50);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
