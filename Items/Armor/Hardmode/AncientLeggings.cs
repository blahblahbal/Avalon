using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
public class AncientLeggings : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(25);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 15);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.SolarFlareLeggings)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.FragmentStardust, 10)
			.AddIngredient(ItemID.FragmentVortex, 10)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
			.AddTile(ModContent.TileType<Tiles.CaesiumForge>())
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.NebulaLeggings)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddIngredient(ItemID.FragmentStardust, 10)
			.AddIngredient(ItemID.FragmentVortex, 10)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
			.AddTile(ModContent.TileType<Tiles.CaesiumForge>())
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.StardustLeggings)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddIngredient(ItemID.FragmentVortex, 10)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
			.AddTile(ModContent.TileType<Tiles.CaesiumForge>())
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.VortexLeggings)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.FragmentStardust, 10)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
			.AddTile(ModContent.TileType<Tiles.CaesiumForge>())
			.Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.maxMinions += 6;
		player.statManaMax2 += 100;
	}
}
