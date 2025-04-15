using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

public class Omnibag : ModItem
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
		Item.value = Item.sellPrice(0, 1);
		Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<BagofIck>())
			.AddIngredient(ModContent.ItemType<BagofBlood>())
			.AddIngredient(ModContent.ItemType<BagofShadows>())
			.AddIngredient(ModContent.ItemType<BagofHallows>())
			.AddIngredient(ModContent.ItemType<BagofFrost>())
			.AddIngredient(ModContent.ItemType<BagofFire>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (!hideVisual)
		{
			UpdateVanity(player);
		}
	}

	public override void UpdateVanity(Player player)
	{
		ModContent.GetInstance<BagofBlood>().UpdateVanity(player);
		ModContent.GetInstance<BagofFire>().UpdateVanity(player);
		ModContent.GetInstance<BagofFrost>().UpdateVanity(player);
		ModContent.GetInstance<BagofHallows>().UpdateVanity(player);
		ModContent.GetInstance<BagofIck>().UpdateVanity(player);
		ModContent.GetInstance<BagofShadows>().UpdateVanity(player);
	}
}
