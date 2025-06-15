using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.StaminaScrolls;

public class TeleportScroll : ModItem
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
			.AddIngredient(ModContent.ItemType<Material.ChaosDust>(), 15)
			.AddIngredient(ItemID.SoulofSight, 5)
			.AddTile(TileID.Loom)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().TeleportUnlocked = player.GetModPlayer<AvalonPlayer>().StaminaAbilitiesEnabled;
	}
}
