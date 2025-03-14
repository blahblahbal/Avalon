using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class SupersonicPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
		{
			Color.Gray
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.Supersonic>();
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
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Material.Herbs.Holybird>())
			.AddIngredient(ItemID.Cobweb, 5)
			.AddIngredient(ItemID.Cloud)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>())
			.AddTile(TileID.Bottles)
			.Register();
	}
}
