using Avalon.Common;
using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class GPSPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(152, 152, 152),
			new Color(81, 136, 246),
			new Color(255, 148, 148)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.GPS>(), TimeUtils.MinutesToTicks(5));
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Beak>()).AddIngredient(ItemID.IronOre).AddIngredient(ItemID.Blinkroot).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Beak>()).AddIngredient(ItemID.LeadOre).AddIngredient(ItemID.Blinkroot).AddTile(TileID.Bottles).Register();
		CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Beak>()).AddIngredient(ModContent.ItemType<NickelOre>()).AddIngredient(ItemID.Blinkroot).AddTile(TileID.Bottles).Register();
	}
}
