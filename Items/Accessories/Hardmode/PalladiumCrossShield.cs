using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Shield)]
public class PalladiumCrossShield : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.defense = 2;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 8);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().PallShield = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.PalladiumBar, 15)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
