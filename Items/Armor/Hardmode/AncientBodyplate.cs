using Avalon.Common.Extensions;
using Avalon.Tiles.Furniture.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
public class AncientBodyplate : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(35);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 20);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.SolarFlareBreastplate)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.FragmentStardust, 10)
			.AddIngredient(ItemID.FragmentVortex, 10)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
			.AddTile(ModContent.TileType<CaesiumForge>())
			.Register();

		CreateRecipe(1).AddIngredient(ItemID.NebulaBreastplate).AddIngredient(ItemID.FragmentSolar, 10)
			.AddIngredient(ItemID.FragmentStardust, 10)
			.AddIngredient(ItemID.FragmentVortex, 10)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
			.AddTile(ModContent.TileType<CaesiumForge>())
			.Register();

		CreateRecipe(1).AddIngredient(ItemID.StardustBreastplate)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddIngredient(ItemID.FragmentVortex, 10)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
			.AddTile(ModContent.TileType<CaesiumForge>())
			.Register();

		CreateRecipe(1).AddIngredient(ItemID.VortexBreastplate)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.FragmentStardust, 10)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
			.AddTile(ModContent.TileType<CaesiumForge>())
			.Register();
	}
	public override void UpdateEquip(Player player)
	{
		player.aggro += 500;
		player.GetKnockback(DamageClass.Summon) += 0.1f;
	}
}
