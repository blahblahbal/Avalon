using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class GauntletPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(40, 49, 77),
			new Color(94, 108, 151),
			new Color(164, 198, 204)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Gauntlet>(), TimeUtils.MinutesToTicks(5), ClassExtensions.PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>()).AddIngredient(ItemID.Deathweed).AddIngredient(ItemID.IronOre, 3).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>()).AddIngredient(ModContent.ItemType<Material.Herbs.Bloodberry>()).AddIngredient(ItemID.IronOre, 3).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>()).AddIngredient(ItemID.Deathweed).AddIngredient(ItemID.LeadOre, 3).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>()).AddIngredient(ModContent.ItemType<Material.Herbs.Bloodberry>()).AddIngredient(ItemID.LeadOre, 3).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>()).AddIngredient(ModContent.ItemType<Material.Herbs.Barfbush>()).AddIngredient(ItemID.IronOre, 3).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>()).AddIngredient(ModContent.ItemType<Material.Herbs.Barfbush>()).AddIngredient(ItemID.LeadOre, 3).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>()).AddIngredient(ItemID.Deathweed).AddIngredient(ModContent.ItemType<Material.Ores.NickelOre>(), 3).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>()).AddIngredient(ModContent.ItemType<Material.Herbs.Bloodberry>()).AddIngredient(ModContent.ItemType<Material.Ores.NickelOre>(), 3).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>()).AddIngredient(ModContent.ItemType<Material.Herbs.Barfbush>()).AddIngredient(ModContent.ItemType<Material.Ores.NickelOre>(), 3).AddTile(TileID.Bottles).Register();
	}
}
