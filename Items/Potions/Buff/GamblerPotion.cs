using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class GamblerPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(137, 126, 13),
			new Color(198, 184, 23),
			new Color(246, 229, 34)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Gambler>(), TimeUtils.MinutesToTicks(4), PotionCorkType.Obsidian);
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
