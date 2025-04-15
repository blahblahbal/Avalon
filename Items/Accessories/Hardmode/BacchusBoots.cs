using Avalon.Common.Players;
using Avalon.Items.Accessories.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Shoes)]
public class BacchusBoots : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.LightPurple;
		Item.value = Item.sellPrice(0, 2);
		Item.defense = 3;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.maxMinions += 2;
		player.GetDamage(DamageClass.Summon) += 0.08f;
		player.GetArmorPenetration(DamageClass.Generic) += 5;
		player.maxTurrets++;
		player.noKnockback = true;
		player.noFallDmg = true;
		player.fireWalk = true;
		player.GetModPlayer<AvalonPlayer>().TrapImmune = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<DionysusAmulet>())
			.AddIngredient(ModContent.ItemType<GuardianBoots>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
