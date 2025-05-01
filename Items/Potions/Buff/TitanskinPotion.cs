using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class TitanskinPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(74, 74, 74),
			new Color(135, 135, 135),
			new Color(217, 217, 217)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Titanskin>(), TimeUtils.MinutesToTicks(4), PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Material.Ores.RhodiumOre>())
			.AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>())
			.AddIngredient(ItemID.SoulofMight)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Material.Ores.OsmiumOre>())
			.AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>())
			.AddIngredient(ItemID.SoulofMight)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Material.Ores.IridiumOre>())
			.AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>())
			.AddIngredient(ItemID.SoulofMight)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
