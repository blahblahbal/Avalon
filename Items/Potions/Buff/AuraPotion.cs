using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class AuraPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(122, 0, 0),
			new Color(204, 0, 0),
			new Color(255, 81, 81)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.CrimsonDrain>(), TimeUtils.MinutesToTicks(5), PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ItemID.Deathweed)
			.AddIngredient(ItemID.Spike)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Barfbush>())
			.AddIngredient(ItemID.Spike)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Bloodberry>())
			.AddIngredient(ItemID.Spike)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
