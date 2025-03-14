using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class GauntletPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[1] { Color.DarkSlateBlue };
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.Gauntlet>();
		Item.consumable = true;
		Item.rare = ItemRarityID.Orange;
		Item.width = dims.Width;
		Item.useTime = 17;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.maxStack = 9999;
		Item.useAnimation = 17;
		Item.height = dims.Height;
		Item.buffTime = TimeUtils.MinutesToTicks(5);
		Item.UseSound = SoundID.Item3;
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
