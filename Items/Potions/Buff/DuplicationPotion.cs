using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class DuplicationPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(29, 88, 50),
			new Color(38, 144, 74),
			new Color(116, 192, 142)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Dupe>(), TimeUtils.MinutesToTicks(8));
	}
	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<BottledLava>(), 2)
			.AddIngredient(ItemID.SoulofSight, 1)
			.AddIngredient(ModContent.ItemType<Sweetstem>(), 2)
			.AddIngredient(ModContent.ItemType<Sulphur>(), 2)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
