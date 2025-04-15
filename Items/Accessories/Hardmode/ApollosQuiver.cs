using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Back)]
public class ApollosQuiver : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 7);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.DestroyerEmblem)
			.AddIngredient(ItemID.MagicQuiver)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.magicQuiver = true;
		player.arrowDamage += 0.15f;
		player.GetCritChance(DamageClass.Ranged) += 5;
		player.GetModPlayer<AvalonPlayer>().RangedCritDamage += 0.25f;
		player.GetModPlayer<AvalonPlayer>().MaxRangedCrit -= 5;
	}
}
