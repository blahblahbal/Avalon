using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Other;

public class ImmortalityPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(116, 11, 11),
			new Color(230, 18, 10),
			new Color(255, 101, 95)
		];
	}

	public override void SetDefaults()
	{
		Item.width = 14;
		Item.height = 24;
		Item.maxStack = Item.CommonMaxStack;
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 4);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>())
			.AddIngredient(ItemID.Blinkroot)
			.AddIngredient(ItemID.SpecularFish)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
