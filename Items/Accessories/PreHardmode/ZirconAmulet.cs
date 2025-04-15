using Avalon.Common.Players;
using Avalon.Items.Material.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Neck)]
public class ZirconAmulet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 70);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.05f);
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Zircon>(), 12)
			.AddIngredient(ItemID.Chain)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
