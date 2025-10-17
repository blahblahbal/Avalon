using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.OreSwords;

public class BronzeBroadsword : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.TinBroadsword);
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 6)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
