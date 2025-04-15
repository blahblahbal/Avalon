using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class DullingTotem : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.lifeRegen = 2;
		Item.defense = 10;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 63);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.Shackle, 6)
			.AddIngredient(ItemID.BandofRegeneration)
			.AddTile(TileID.Anvils).Register();
	}
}
