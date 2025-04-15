using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

public class ShadowPulseBag : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.VanityBags;
	}
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Lime;
		Item.vanity = true;
		Item.value = Item.sellPrice(0, 2);
		Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Omnibag>())
			.AddIngredient(ModContent.ItemType<ShadowPulse>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
		player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
		if (!hideVisual)
		{
			UpdateVanity(player);
		}
	}

	public override void UpdateVanity(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().PulseCharm = true;
		player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
		ModContent.GetInstance<Omnibag>().UpdateVanity(player);
	}
}
