using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Items.Armor.PreHardmode;
using Avalon.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
public class EarthsplitterHelm : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(10);
		Item.rare = ModContent.RarityType<CrispyRarity>();
		Item.value = Item.sellPrice(0, 3);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 2)
			.AddIngredient(ItemID.ShadowHelmet)
			.AddIngredient(ItemID.MiningHelmet)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 2)
			.AddIngredient(ItemID.AncientShadowHelmet)
			.AddIngredient(ItemID.MiningHelmet)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 2)
			.AddIngredient(ItemID.CrimsonHelmet)
			.AddIngredient(ItemID.MiningHelmet)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 2)
			.AddIngredient(ModContent.ItemType<ViruthornHelmet>())
			.AddIngredient(ItemID.MiningHelmet)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<EarthsplitterChestpiece>() && legs.type == ModContent.ItemType<EarthsplitterLeggings>();
	}

	public override void UpdateArmorSet(Player player)
	{
		AvalonPlayer modPlayer = player.GetModPlayer<AvalonPlayer>();
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Earthsplitter"); // "Ore blocks have a 33% chance to drop double ore";

		modPlayer.OreDupe = true;
	}

	public override void UpdateEquip(Player player)
	{
		Lighting.AddLight(player.position, 0.8f, 0.8f, 0.8f);
	}
}
