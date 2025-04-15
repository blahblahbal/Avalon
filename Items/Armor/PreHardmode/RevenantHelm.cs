using Avalon.Common.Players;
using Avalon.Items.Material.Shards;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class RevenantHelm : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetDefaults()
	{
		Item.DefaultToArmor(7);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 2);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.ShadowScale, 5)
			.AddIngredient(ItemID.Bone, 40)
			.AddIngredient(ModContent.ItemType<UndeadShard>(), 8)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.TissueSample, 5)
			.AddIngredient(ItemID.Bone, 40)
			.AddIngredient(ModContent.ItemType<UndeadShard>(), 8)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.Booger>(), 5)
			.AddIngredient(ItemID.Bone, 40)
			.AddIngredient(ModContent.ItemType<UndeadShard>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<RevenantChestplate>() && legs.type == ModContent.ItemType<RevenantGreaves>();
	}
	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Revenant");
		player.GetModPlayer<AvalonPlayer>().ZombieArmor = true;
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.04f;
		player.GetAttackSpeed(DamageClass.Melee) += 0.03f;
	}
}
