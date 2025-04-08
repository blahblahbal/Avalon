using Avalon.Common.Players;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Avalon.Items.Tomes.Superhardmode;

class TheWorld : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
		Item.width = dims.Width;
		Item.value = 250000;
		Item.height = dims.Height;
		Item.defense = 18;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 8;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.35f;
		player.GetCritChance(DamageClass.Generic) += 20;
		player.manaCost -= 0.25f;
		player.statLifeMax2 += 160;
		player.statManaMax2 += 260;
		player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 120;
	}

	public override void AddRecipes()
	{
		// TODO: implement post-UB drops
		//CreateRecipe(1).AddIngredient(ModContent.ItemType<Dominance>()).AddIngredient(ModContent.ItemType<VictoryPiece>(), 3).AddIngredient(ModContent.ItemType<SoulofTorture>(), 25).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
	}
}
