using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class MagnetPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[2]
		{
			Color.Red,
			Color.Blue
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.Magnet>();
		Item.consumable = true;
		Item.rare = ItemRarityID.Green;
		Item.width = dims.Width;
		Item.useTime = 17;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.maxStack = 9999;
		Item.useAnimation = 17;
		Item.height = dims.Height;
		Item.buffTime = TimeUtils.MinutesToTicks(4);
		Item.UseSound = SoundID.Item3;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddIngredient(ItemID.Ebonkoi)
			.AddIngredient(ItemID.Blinkroot)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddIngredient(ItemID.Hemopiranha)
			.AddIngredient(ItemID.Blinkroot)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddIngredient(ModContent.ItemType<Fish.SicklyTrout>())
			.AddIngredient(ItemID.Blinkroot)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
