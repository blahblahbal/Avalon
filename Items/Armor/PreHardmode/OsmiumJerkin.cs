using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class OsmiumJerkin : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(8);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override void UpdateEquip(Player player)
	{
		player.GetKnockback(DamageClass.Generic) += 0.15f;
		player.GetModPlayer<AvalonPlayer>().MeleeScale += 0.3f;
		player.whipRangeMultiplier += 0.3f;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<OsmiumBar>(), 20)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 6)
			.AddTile(TileID.Anvils).Register();
	}
}
