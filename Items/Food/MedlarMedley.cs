using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Food;

public class MedlarMedley : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[Item.type] = new Color[3]
		{
			new Color(237, 223, 144),
			new Color(213, 193, 133),
			new Color(186, 167, 120)
		};
		ItemID.Sets.IsFood[Type] = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Durian>())
			.AddIngredient(ModContent.ItemType<Medlar>())
			.AddIngredient(ItemID.Bottle)
			.AddTile(TileID.CookingPots)
			.Register();
	}

	public override void SetDefaults()
	{
		// DefaultToFood sets all of the food related item defaults such as the buff type, buff duration, use sound, and animation time.
		Item.DefaultToFood(22, 18, BuffID.WellFed, TimeUtils.MinutesToTicks(20), true);
		Item.value = Item.sellPrice(0, 0, 40);
		Item.rare = ItemRarityID.Green;
	}
}
