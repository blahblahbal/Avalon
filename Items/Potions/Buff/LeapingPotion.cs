using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class LeapingPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
		{
			Color.DarkOrange
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.Leaping>();
		Item.consumable = true;
		Item.rare = ItemRarityID.Orange;
		Item.width = dims.Width;
		Item.useTime = 17;
		Item.value = 2000;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.maxStack = 9999;
		Item.useAnimation = 17;
		Item.height = dims.Height;
		Item.buffTime = TimeUtils.MinutesToTicks(6);
		Item.UseSound = SoundID.Item3;
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Holybird>()).AddIngredient(ItemID.FallenStar).AddIngredient(ItemID.Vine).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddTile(TileID.Bottles).Register();
	//}
}
