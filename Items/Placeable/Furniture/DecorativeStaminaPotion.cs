using Avalon.Items.Potions.Other;
using Avalon.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture
{
	public class DecorativeStaminaPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 30;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.PlaceableHealingPotion);
			Item.createTile = ModContent.TileType<PlacedStaminaPotion>();
			Item.placeStyle = 0;
		}
		public override void AddRecipes()
		{
			CreateRecipe().AddTile(TileID.HeavyWorkBench).AddIngredient(ModContent.ItemType<LesserStaminaPotion>()).Register();
		}
	}
}
