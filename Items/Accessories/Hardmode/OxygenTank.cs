using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class OxygenTank : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 2);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[BuffID.Suffocation] = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddIngredient(ItemID.ChlorophyteBar, 20)
			.AddIngredient(ItemID.GillsPotion, 2)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
