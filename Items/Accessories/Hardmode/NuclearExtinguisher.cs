using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class NuclearExtinguisher : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 4);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[BuffID.Blackout] = true;
		player.buffImmune[BuffID.CursedInferno] = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<GreekExtinguisher>())
			.AddIngredient(ModContent.ItemType<SixHundredWattLightbulb>())
			.AddTile(TileID.TinkerersWorkbench).Register();
	}
}
