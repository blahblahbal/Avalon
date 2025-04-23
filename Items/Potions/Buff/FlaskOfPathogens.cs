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
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(109, 72, 182),
			new Color(134, 136, 192),
			new Color(210, 182, 239)
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
