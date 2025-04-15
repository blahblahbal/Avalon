using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class ChaosEmblem : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 6);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.3f);
		player.GetDamage(DamageClass.Generic) += 0.1f;
		player.GetModPlayer<AvalonPlayer>().AllMaxCrit(8);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<ChaosCrystal>())
			.AddIngredient(ItemID.AvengerEmblem)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
