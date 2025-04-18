using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class BuildersToolbelt : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.sellPrice(0, 30);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		Player.tileRangeX += 10;
		Player.tileRangeY += 10;
		player.carpet = true;
		player.wallSpeed += 0.45f;
		player.tileSpeed += 0.45f;
		player.accWatch = 3;
		player.accCompass = 1;
		player.accDepthMeter = 1;
		player.GetModPlayer<AvalonPlayer>().BuilderBelt = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<GoblinToolbelt>())
			.AddIngredient(ItemID.PortableCementMixer)
			.AddIngredient(ItemID.BrickLayer)
			.AddIngredient(ItemID.ExtendoGrip)
			.AddIngredient(ItemID.FlyingCarpet)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
