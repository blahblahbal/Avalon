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
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(20, 93, 11),
			new Color(100, 179, 15),
			new Color(190, 231, 54)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Fury>(), TimeUtils.MinutesToTicks(4));
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
