using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.Superhardmode;

class SceneofCarnage : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Yellow;
		Item.width = dims.Width;
		Item.value = Item.sellPrice(0, 0, 40);
		Item.height = dims.Height;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 5;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
		player.GetDamage(DamageClass.Melee) += 0.15f;
	}

	//public override void AddRecipes()
	//{
	//    CreateRecipe(1)
	//        .AddIngredient(ModContent.ItemType<DragonOrb>())
	//        .AddIngredient(ModContent.ItemType<BerserkerBar>(), 25)
	//        .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
	//        .AddIngredient(ModContent.ItemType<DarkMatterGel>(), 100)
	//        .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3)
	//        .AddTile(ModContent.TileType<Tiles.TomeForge>())
	//        .Register();
	//}
}
