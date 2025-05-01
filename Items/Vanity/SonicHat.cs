using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class SonicHat : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToVanity();
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 0, 50);
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.MushroomGrassSeeds, 5)
				.AddIngredient(ItemID.Silk, 20)
				.AddTile(TileID.Loom)
				.Register();
		}
	}
}
