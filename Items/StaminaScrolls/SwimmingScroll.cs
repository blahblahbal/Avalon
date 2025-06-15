using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.StaminaScrolls;

public class SwimmingScroll : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.StaminaScrolls;
	}
	public override void SetDefaults()
	{
		Item.DefaultToStaminaScroll();
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BlankScroll>())
			.AddIngredient(ItemID.Leather, 2)
			.AddIngredient(ItemID.FlipperPotion, 2)
			.AddTile(TileID.Loom)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().SwimmingUnlocked = player.GetModPlayer<AvalonPlayer>().StaminaAbilitiesEnabled;
	}
}
