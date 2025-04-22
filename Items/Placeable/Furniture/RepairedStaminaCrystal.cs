using Avalon.Items.Consumables;
using Avalon.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture
{
	public class RepairedStaminaCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 10;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.RepairedManaCrystal);
			Item.createTile = ModContent.TileType<PlacedStaminaCrystal>();
			Item.placeStyle = 0;
		}
		public override void AddRecipes()
		{
			CreateRecipe().AddCondition(Condition.InGraveyard).AddTile(TileID.HeavyWorkBench).AddIngredient(ModContent.ItemType<StaminaCrystal>()).Register();
		}
	}
}
