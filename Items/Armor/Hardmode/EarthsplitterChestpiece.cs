using Avalon.Common.Players;
using Avalon.Items.Armor.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
public class EarthsplitterChestpiece : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(9);
		Item.rare = ModContent.RarityType<Rarities.CrispyRarity>();
		Item.value = Item.sellPrice(0, 3);
	}
	public override void UpdateEquip(Player player)
	{
		player.nightVision = true;
		player.GetModPlayer<AvalonPlayer>().HookBonus = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
			.AddIngredient(ItemID.ShadowScalemail)
			.AddIngredient(ItemID.MiningShirt)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
			.AddIngredient(ItemID.AncientShadowScalemail)
			.AddIngredient(ItemID.MiningShirt)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
			.AddIngredient(ItemID.CrimsonScalemail)
			.AddIngredient(ItemID.MiningShirt)
			.AddTile(TileID.Anvils)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
			.AddIngredient(ModContent.ItemType<ViruthornScalemail>())
			.AddIngredient(ItemID.MiningShirt)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
