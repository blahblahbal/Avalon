using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.Superhardmode;

public class Dominance : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTome(6, 3);
		Item.defense = 11;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.22f;
		player.GetCritChance(DamageClass.Generic) += 8;
		player.manaCost -= 0.1f;
		player.statLifeMax2 += 80;
		player.statManaMax2 += 140;
	}

	//public override void AddRecipes()
	//{
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<TheOasisRemembered>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<TheOasisRemembered>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<SceneofCarnage>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<SceneofCarnage>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<ThePlumHarvest>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<ThePlumHarvest>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<ChantoftheWaterDragon>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<ChantoftheWaterDragon>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<TheThreeScholars>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<TheThreeScholars>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	//}
}
