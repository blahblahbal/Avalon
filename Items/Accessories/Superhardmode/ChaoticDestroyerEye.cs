using Avalon.Common.Players;
using Avalon.Items.Accessories.Hardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;

public class ChaoticDestroyerEye : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 9, 75);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.2f);
		player.GetDamage(DamageClass.Generic) += 0.1f;
		player.GetModPlayer<AvalonPlayer>().ChaosCharm = true;
		player.GetCritChance(DamageClass.Generic) += 8;
		player.GetModPlayer<AvalonPlayer>().TrapImmune = true;
		player.GetModPlayer<AvalonPlayer>().AllMaxCrit(3);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<ChaosEye>())
			.AddIngredient(ModContent.ItemType<HellsteelEmblem>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
