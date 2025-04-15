using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Neck)]
public class ThornHeartAmulet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Chain)
			.AddIngredient(ItemID.LifeCrystal)
			.AddIngredient(ItemID.Stinger, 6)
			.AddIngredient(ItemID.SoulofNight, 8)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		float dmg = 2 * (float)Math.Floor((player.statLifeMax2 - (double)player.statLife) / player.statLifeMax2 * 10) / 50;
		if (dmg < 0) dmg = 0;
		player.GetDamage(DamageClass.Generic) += dmg;
	}
}
