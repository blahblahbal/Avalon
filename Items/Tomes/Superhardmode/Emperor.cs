using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.Superhardmode;

class Emperor : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Cyan;
		Item.width = dims.Width;
		Item.value = 250000;
		Item.height = dims.Height;
		Item.defense = 14;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 7;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.25f;
		player.GetCritChance(DamageClass.Generic) += 12;
		player.manaCost -= 0.2f;
		player.statLifeMax2 += 100;
		player.statManaMax2 += 200;
		player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 90;
	}

	//public override void AddRecipes()
	//{
	//    CreateRecipe(1)
	//        .AddIngredient(ModContent.ItemType<Dominance>())
	//        .AddIngredient(ModContent.ItemType<VictoryPiece>(), 3)
	//        .AddIngredient(ModContent.ItemType<SoulofTorture>(), 25)
	//        .AddTile(ModContent.TileType<Tiles.TomeForge>())
	//        .Register();
	//}
}
