using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class ZincBroadsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(14, 6f, 18, false, width: 24, height: 28);
		Item.value = Item.sellPrice(silver: 11);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
