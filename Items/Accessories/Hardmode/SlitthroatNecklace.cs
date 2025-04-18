using Avalon.Common.Players;
using Avalon.Items.Accessories.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Neck)]
public class SlitthroatNecklace : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 6);
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().BloodyWhetstone = true;
		player.GetArmorPenetration(DamageClass.Melee) += 15;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<BlackWhetstone>())
			.AddIngredient(ModContent.ItemType<BloodyWhetstone>())
			.AddIngredient(ItemID.SharkToothNecklace)
			.AddIngredient(ItemID.SoulofFright, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
