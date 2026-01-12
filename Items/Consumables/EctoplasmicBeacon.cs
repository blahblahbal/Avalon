using Avalon.Common.Extensions;
using Avalon.Tiles.Furniture.Functional;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class EctoplasmicBeacon : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 3;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpawner(width: 14, height: 28);
		Item.useStyle = ItemUseStyleID.None;
		//Item.useStyle = ItemUseStyleID.HoldUp;
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
	}

	//public override bool CanUseItem(Player player)
	//{
	//	return !NPC.AnyNPCs(ModContent.NPCType<Phantasm>()) && player.InModBiome<Biomes.Hellcastle>() &&
	//		   NPC.downedMoonlord && Main.hardMode;
	//}

	//public override bool? UseItem(Player player)
	//{
	//	//NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Phantasm>());
	//	//SoundEngine.PlaySound(SoundID.Roar, player.position);
	//	return true;
	//}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Ectoplasm, 10)
			.AddIngredient(ItemID.LunarBar, 5)
			.AddIngredient(ItemID.BeetleHusk, 8) // might change later lol
			.AddTile(ModContent.TileType<LibraryAltar>())
			.Register();
	}
}
