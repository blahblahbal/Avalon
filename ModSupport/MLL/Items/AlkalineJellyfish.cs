using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class AlkalineJellyfish : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.CanBePlacedOnWeaponRacks[Type] = true;
		Item.ResearchUnlockCount = 3;
	}
	public override void SetDefaults()
	{
		Item.useStyle = ItemUseStyleID.Swing;
		Item.autoReuse = true;
		Item.useTurn = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.maxStack = Item.CommonMaxStack;
		Item.consumable = true;
		Item.width = 12;
		Item.height = 12;
		Item.noUseGraphic = true;
		Item.bait = 35;
		Item.value = Item.sellPrice(0, 4);

		Item.rare = ItemRarityID.Green;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.BlueJellyfish)
			.AddIngredient<LifeDew>()
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.GreenJellyfish)
			.AddIngredient<LifeDew>()
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.PinkJellyfish)
			.AddIngredient<LifeDew>()
			.Register();
	}
}
