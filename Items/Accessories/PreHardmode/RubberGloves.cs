using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class RubberGloves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(gold: 1);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetAttackSpeed(DamageClass.Melee) -= 0.24f;
		player.GetModPlayer<AvalonPlayer>().RubberGloves = true;
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type).AddTile(TileID.WorkBenches)
	//        .AddIngredient(ModContent.ItemType<StaminaCrystal>(), 3)
	//        .AddRecipeGroup("GoldBar", 4)
	//        .AddIngredient(ModContent.ItemType<StaminaPotion>(), 2)
	//        .AddTile(TileID.TinkerersWorkbench).Register();
	//}
}
