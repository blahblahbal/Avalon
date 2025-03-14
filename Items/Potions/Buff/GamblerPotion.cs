using Avalon.Common;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class GamblerPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[2] {
			Color.Yellow,
			Color.LightYellow
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.Gambler>();
		Item.consumable = true;
		Item.rare = ItemRarityID.LightRed;
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
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<BottledLava>())
			.AddIngredient(ModContent.ItemType<FakeFourLeafClover>())
			.AddIngredient(ItemID.VileMushroom)
			.AddIngredient(ItemID.SilverCoin)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<BottledLava>())
			.AddIngredient(ModContent.ItemType<FakeFourLeafClover>())
			.AddIngredient(ItemID.ViciousMushroom)
			.AddIngredient(ItemID.SilverCoin)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<BottledLava>())
			.AddIngredient(ModContent.ItemType<FakeFourLeafClover>())
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddIngredient(ItemID.SilverCoin)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
