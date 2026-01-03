using Avalon.Common.Extensions;
using Avalon.NPCs.Bosses.Hardmode.Mechasting;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class MechanicalWasp : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 3;
	}
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}

	public override void SetDefaults()
	{
		Item.DefaultToSpawner();
		Item.rare = ItemRarityID.Orange;
	}

	public override bool CanUseItem(Player player)
	{
		if (NPC.AnyNPCs(ModContent.NPCType<Mechasting>()) || Main.dayTime) return false;
		return true;
	}

	public override bool? UseItem(Player player)
	{
		SoundEngine.PlaySound(SoundID.Roar, player.position);
		NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Mechasting>());
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Stinger, 6)
			.AddRecipeGroup(RecipeGroupID.IronBar, 5)
			.AddIngredient(ItemID.SoulofFlight, 9)
			.AddIngredient(ItemID.SoulofNight, 6)
			.AddTile(TileID.MythrilAnvil)
			.SortAfterFirstRecipesOf(ItemID.MechanicalSkull)
			.Register();
		//CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.MosquitoProboscis>(), 9).AddIngredient(ItemID.HallowedBar, 10).AddIngredient(ModContent.ItemType<Material.DragonScale>(), 2).AddIngredient(ItemID.SoulofFlight, 15).AddTile(ModContent.TileType<Tiles.HallowedAltar>()).Register();
	}
}
