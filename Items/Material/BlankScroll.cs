using Avalon.Items.Consumables;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class BlankScroll : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 3;
	}
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = Item.CommonMaxStack;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.UseSound = new SoundStyle("Avalon/Sounds/Item/Scroll");
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Leather, 2)
			.AddIngredient(ModContent.ItemType<StaminaCrystal>())
			.AddTile(TileID.Loom)
			.Register();
	}
}
