using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class StarbrightPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[2]
		{
			Color.Blue,
			Color.Cyan
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.Starbright>();
		Item.consumable = true;
		Item.rare = ItemRarityID.Green;
		Item.width = dims.Width;
		Item.useTime = 17;
		Item.value = 2000;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.maxStack = 9999;
		Item.useAnimation = 17;
		Item.height = dims.Height;
		Item.buffTime = TimeUtils.MinutesToTicks(5);
		Item.UseSound = SoundID.Item3;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ItemID.FallenStar, 2).AddIngredient(ItemID.Lens).AddIngredient(ItemID.Meteorite).AddTile(TileID.Bottles).Register();
	}
}
