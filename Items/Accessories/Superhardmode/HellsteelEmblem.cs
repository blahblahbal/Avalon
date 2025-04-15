using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;

public class HellsteelEmblem : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Red;
		Item.value = Item.sellPrice(0, 9);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.15f;
		player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.3f);
		player.GetModPlayer<AvalonPlayer>().TrapImmune = true;
		player.GetModPlayer<AvalonPlayer>().AllMaxCrit(8);
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddIngredient(ModContent.ItemType<ChaosEmblem>())
	//        .AddIngredient(ModContent.ItemType<GuardianBoots>())
	//        .AddIngredient(ModContent.ItemType<Material.HellsteelPlate>(), 20)
	//        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
	//        .Register();
	//}
}
