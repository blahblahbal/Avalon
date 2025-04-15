using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Shield)]
public class PalladiumOmegaShield : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.defense = 4;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 2);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<CobaltCrossShield>())
			.AddIngredient(ModContent.ItemType<PalladiumCrossShield>())
			.AddIngredient(ModContent.ItemType<DurataniumCrossShield>())
			.AddIngredient(ItemID.SoulofSight, 5)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().CobShield = true;
		player.GetModPlayer<AvalonPlayer>().PallShield = true;
		player.GetModPlayer<AvalonPlayer>().DuraShield = true;
		player.GetModPlayer<AvalonPlayer>().PallOmegaShield = true;
		player.noKnockback = true;
	}
}
