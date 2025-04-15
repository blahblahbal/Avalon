using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class FlaskOfPathogens : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] =
		[
			Color.Purple,
			Color.MediumPurple
		];
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.FlaskofCursedFlames);
		Item.buffType = ModContent.BuffType<Buffs.ImbuePathogen>();
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.BottledWater)
			.AddIngredient(ModContent.ItemType<Pathogen>(), 2)
			.AddTile(TileID.ImbuingStation)
			.SortAfterFirstRecipesOf(ItemID.FlaskofCursedFlames)
			.Register();
	}
}
