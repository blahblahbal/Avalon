using Avalon.Common.Players;
using Avalon.Items.Material;
using Avalon.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
public class AncientHeadpiece : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(30);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 10);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.SolarFlareHelmet)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.FragmentStardust, 10)
			.AddIngredient(ItemID.FragmentVortex, 10)
			.AddIngredient(ModContent.ItemType<LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<GhostintheMachine>())
			.AddTile(ModContent.TileType<CaesiumForge>())
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.NebulaHelmet)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddIngredient(ItemID.FragmentStardust, 10)
			.AddIngredient(ItemID.FragmentVortex, 10)
			.AddIngredient(ModContent.ItemType<LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<GhostintheMachine>())
			.AddTile(ModContent.TileType<CaesiumForge>())
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.StardustHelmet)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddIngredient(ItemID.FragmentVortex, 10)
			.AddIngredient(ModContent.ItemType<LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<GhostintheMachine>())
			.AddTile(ModContent.TileType<CaesiumForge>())
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.VortexHelmet)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.FragmentStardust, 10)
			.AddIngredient(ItemID.FragmentSolar, 10)
			.AddIngredient(ModContent.ItemType<LifeDew>(), 5)
			.AddIngredient(ModContent.ItemType<GhostintheMachine>())
			.AddTile(ModContent.TileType<CaesiumForge>())
			.Register();
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<AncientBodyplate>() &&
			   legs.type == ModContent.ItemType<AncientLeggings>();
	}

	public override void UpdateArmorSet(Player player)
	{
		AvalonPlayer modPlayer = player.GetModPlayer<AvalonPlayer>();
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Ancient", Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
		modPlayer.AncientLessCost = true;
		modPlayer.AncientRangedBonus = true;
		modPlayer.AncientMinionGuide = true;
		modPlayer.AncientSandVortex = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Generic) += 0.23f;
		player.GetCritChance(DamageClass.Generic) += 6;
	}
}
