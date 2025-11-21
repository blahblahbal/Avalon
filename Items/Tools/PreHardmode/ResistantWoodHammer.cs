using Avalon.Common.Extensions;
using Avalon.Items.Armor.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class ResistantWoodHammer : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToHammer(55, 12, 5.5f, 28, 28);
		Item.value = Item.sellPrice(copper: 10);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Items.Placeable.Tile.ResistantWood>(), 8)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<ResistantWoodGreaves>())
			.Register();
	}
}
