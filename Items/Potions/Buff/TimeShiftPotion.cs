using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class TimeShiftPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
		{
			Color.Tan
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.TimeShift>();
		Item.consumable = true;
		Item.rare = ItemRarityID.LightRed;
		Item.width = dims.Width;
		Item.useTime = 17;
		Item.value = 2000;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.maxStack = 9999;
		Item.useAnimation = 17;
		Item.height = dims.Height;
		Item.buffTime = TimeUtils.MinutesToTicks(9);
		Item.UseSound = SoundID.Item3;
	}
	public override void AddRecipes()
	{
		CreateRecipe(5)
			.AddIngredient(ItemID.BottledWater)
			.AddIngredient(ItemID.BottledHoney, 5)
			.AddIngredient(ItemID.Feather, 8)
			.AddIngredient(ItemID.FastClock)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
