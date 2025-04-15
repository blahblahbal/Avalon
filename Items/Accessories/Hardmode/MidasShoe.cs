using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class MidasShoe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<GoldenShield>())
			.AddIngredient(ModContent.ItemType<RubberBoot>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[BuffID.Ichor] = true;
		player.buffImmune[BuffID.Electrified] = true;
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Electrified>()] = true;
	}
}
