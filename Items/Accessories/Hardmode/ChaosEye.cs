using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class ChaosEye : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 8);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().ChaosCharm = true;
		player.GetCritChance(DamageClass.Generic) += 8;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<ChaosCharm>())
			.AddIngredient(ItemID.EyeoftheGolem)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
