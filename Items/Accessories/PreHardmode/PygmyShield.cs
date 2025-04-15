using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class PygmyShield : ModItem
{
	/*public override void SetStaticDefaults() //For the Future
	{
		if (ExxoAvalonOrigins.Depths == null)
			return;
		ExxoAvalonOrigins.Depths.Call("CobaltShieldOnlyItem", Type, true);
	}*/

	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.defense = 3;
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 75);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.CobaltShield)
			.AddIngredient(ItemID.PygmyNecklace)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.maxMinions++;
		player.noKnockback = true;
	}
}
