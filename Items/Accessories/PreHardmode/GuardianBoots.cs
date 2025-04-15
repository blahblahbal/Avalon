using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Shoes)]
public class GuardianBoots : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.defense = 2;
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 1, 44);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.ObsidianHorseshoe)
			.AddIngredient(ItemID.CobaltShield)
			.AddIngredient(ItemID.Spike, 50)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.ObsidianShield)
			.AddIngredient(ItemID.LuckyHorseshoe)
			.AddIngredient(ItemID.Spike, 50)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().TrapImmune = true;
		player.noKnockback = true;
		player.noFallDmg = true;
		player.fireWalk = true;
	}
}
