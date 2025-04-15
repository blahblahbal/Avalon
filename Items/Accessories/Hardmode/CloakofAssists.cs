using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class CloakofAssists : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 8);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.honeyCombItem = Item;
		player.starCloakItem = Item;
		player.panic = player.GetModPlayer<AvalonPlayer>().LightningInABottle = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.StarCloak)
			.AddIngredient(ItemID.PanicNecklace)
			.AddIngredient(ItemID.HoneyComb)
			.AddIngredient(ModContent.ItemType<LightninginaBottle>())
			.AddTile(TileID.TinkerersWorkbench).Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.StarCloak)
			.AddIngredient(ItemID.SweetheartNecklace)
			.AddIngredient(ModContent.ItemType<LightninginaBottle>())
			.AddTile(TileID.TinkerersWorkbench).Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.BeeCloak)
			.AddIngredient(ItemID.PanicNecklace)
			.AddIngredient(ModContent.ItemType<LightninginaBottle>())
			.AddTile(TileID.TinkerersWorkbench).Register();
	}
}
