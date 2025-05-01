using Avalon.Common.Extensions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

public class DragonOrb : ModItem
{
	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
		ItemID.Sets.ItemNoGravity[Item.type] = true;
		ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		Item.ResearchUnlockCount = 2;
	}

	public override void SetDefaults()
	{
		Item.DefaultToTomeMaterial();
		Item.rare = ItemRarityID.LightRed;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.CraftedTomeMats;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<ElementDust>(), 3)
			.AddIngredient(ModContent.ItemType<ElementDiamond>(), 2)
			.AddIngredient(ModContent.ItemType<RubybeadHerb>(), 6)
			.AddIngredient(ModContent.ItemType<DewOrb>(), 3)
			.AddIngredient(ModContent.ItemType<StrongVenom>(), 5)
			.AddIngredient(ModContent.ItemType<MysticalTotem>())
			.AddIngredient(ModContent.ItemType<DewofHerbs>(), 3)
			.AddIngredient(ModContent.ItemType<MysticalClaw>(), 4)
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
