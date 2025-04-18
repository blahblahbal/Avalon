using Avalon.Common;
using Avalon.Items.Fish;
using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class FuryPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
		{
			Color.GreenYellow
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.Fury>();
		Item.UseSound = SoundID.Item3;
		Item.consumable = true;
		Item.rare = ItemRarityID.Blue;
		Item.width = dims.Width;
		Item.useTime = 17;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.maxStack = 9999;
		Item.value = Item.sellPrice(0, 0, 2, 0);
		Item.useAnimation = 17;
		Item.height = dims.Height;
		Item.buffTime = TimeUtils.MinutesToTicks(4);
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.BottledWater)
			.AddIngredient(ModContent.ItemType<Ickfish>())
			.AddIngredient(ModContent.ItemType<Barfbush>())
			.AddTile(TileID.Bottles)
			.Register();
	}
}
