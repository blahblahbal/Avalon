using Avalon.Common;
using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class CloverPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[2]
		{
			Color.Lime,
			Color.Yellow
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.Clover>();
		Item.consumable = true;
		Item.rare = ItemRarityID.Green;
		Item.width = dims.Width;
		Item.useTime = 17;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.maxStack = 9999;
		Item.useAnimation = 17;
		Item.height = dims.Height;
		Item.buffTime = TimeUtils.MinutesToTicks(30);
		Item.UseSound = SoundID.Item3;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.FakeFourLeafClover>())
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Holybird>())
			.AddIngredient(ItemID.Fireblossom)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe(20)
			.AddIngredient(ModContent.ItemType<Material.FourLeafClover>())
			.AddIngredient(ModContent.ItemType<Material.BottledLava>(), 20)
			.AddIngredient(ModContent.ItemType<Holybird>(), 20)
			.AddIngredient(ItemID.Fireblossom, 20)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
