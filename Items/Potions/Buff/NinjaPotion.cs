using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class NinjaPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(1, 1, 1),
			new Color(49, 49, 49),
			new Color(102, 102, 102)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Ninja>(), TimeUtils.MinutesToTicks(4), ClassExtensions.PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ItemID.Blinkroot)
			.AddIngredient(ItemID.DemoniteOre)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ItemID.Blinkroot)
			.AddIngredient(ItemID.CrimtaneOre)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ItemID.Blinkroot)
			.AddIngredient(ModContent.ItemType<Material.Ores.BacciliteOre>())
			.AddTile(TileID.Bottles)
			.Register();
	}
}
