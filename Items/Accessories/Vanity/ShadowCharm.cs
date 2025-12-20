using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

public class ShadowCharm : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ModContent.RarityType<Rarities.SapphirephlyRarity>();
		Item.value = Item.sellPrice(0, 0, 45);
		Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
	}
	public override void UpdateVanity(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().ShadowCharm = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.ShadowScale, 3)
			.AddIngredient(ItemID.DemoniteBar, 5)
			.AddIngredient(ItemID.NinjaHood)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.ShadowScale, 3)
			.AddIngredient(ItemID.DemoniteBar, 5)
			.AddIngredient(ItemID.NinjaShirt)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.ShadowScale, 3)
			.AddIngredient(ItemID.DemoniteBar, 5)
			.AddIngredient(ItemID.NinjaPants)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.TissueSample, 3)
			.AddIngredient(ItemID.CrimtaneBar, 5)
			.AddIngredient(ItemID.NinjaHood)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.TissueSample, 3)
			.AddIngredient(ItemID.CrimtaneBar, 5)
			.AddIngredient(ItemID.NinjaShirt)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.TissueSample, 3)
			.AddIngredient(ItemID.CrimtaneBar, 5)
			.AddIngredient(ItemID.NinjaPants)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Booger>(), 3)
			.AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 5)
			.AddIngredient(ItemID.NinjaHood)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Booger>(), 3)
			.AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 5)
			.AddIngredient(ItemID.NinjaShirt)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Booger>(), 3)
			.AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 5)
			.AddIngredient(ItemID.NinjaPants)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
