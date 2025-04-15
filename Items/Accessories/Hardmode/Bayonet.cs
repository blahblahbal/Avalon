using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class Bayonet : ModItem
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
			.AddIngredient(ModContent.ItemType<HiddenBlade>())
			.AddIngredient(ModContent.ItemType<AmmoMagazine>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.BrokenWeaponry>()] = true;
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Unloaded>()] = true;
	}
}
