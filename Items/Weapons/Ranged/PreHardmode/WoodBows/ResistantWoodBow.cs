using Avalon.Items.Weapons.Melee.Maces.WoodenClubs;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.WoodBows;

public class ResistantWoodBow : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.AshWoodBow);
		Item.useAnimation += 2;
		Item.useTime += 2;
		Item.damage += 1;
		Item.shootSpeed += 2;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(6, 0);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Placeable.Tile.ResistantWood>(), 10)
			.AddTile(TileID.WorkBenches)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<ResistantWoodClub>())
			.Register();
	}
}
