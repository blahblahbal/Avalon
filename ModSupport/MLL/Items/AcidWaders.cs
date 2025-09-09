using Avalon.Common.Players;
using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

[AutoloadEquip(EquipType.Shoes)]
public class AcidWaders : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 10);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().AcidWalk = true;
		player.GetModPlayer<AvalonPlayer>().AcidDmgReduction = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.WaterWalkingBoots)
			.AddIngredient(ModContent.ItemType<Acidskipper>(), 3)
			.AddIngredient(ModContent.ItemType<LifeDew>(), 5)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
