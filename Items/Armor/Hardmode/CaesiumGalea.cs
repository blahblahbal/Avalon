using Avalon.Buffs;
using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
public class CaesiumGalea : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(31);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 10);
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<CaesiumPlateMail>() && legs.type == ModContent.ItemType<CaesiumGreaves>();
	}
	public override void ArmorSetShadows(Player player)
	{
		if (player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
			player.armorEffectDrawOutlinesForbidden = true;
		else
			player.armorEffectDrawShadow = true;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<CaesiumBar>(), 30)
			.AddIngredient(ItemID.HellstoneBar, 10)
			.AddIngredient(ItemID.SoulofSight, 5)
			.AddTile(TileID.MythrilAnvil).Register();
	}
	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Caesium", Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
		player.GetDamage(DamageClass.Melee) += 0.05f;
		player.statDefense += 4;
		player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoost = true;
	}
	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.08f;
	}
}

public class CaesiumBoostingStancePlayer : ModPlayer
{
	public bool CaesiumBoost;
	public bool CaesiumBoostActive;
	public override void ResetEffects()
	{
		CaesiumBoost = false;
	}
	public override void PostUpdateEquips()
	{
		if (!CaesiumBoost)
			CaesiumBoostActive = false;
		//if (Player.doubleTapCardinalTimer[Main.ReversedUpDownArmorSetBonuses ? 1 : 0] < 15 && ((Player.releaseUp && Main.ReversedUpDownArmorSetBonuses && Player.controlUp) || (Player.releaseDown && !Main.ReversedUpDownArmorSetBonuses && Player.controlDown)))
		//{
		if (CaesiumBoost && !Player.mount.Active && Player.PlayerDoublePressedSetBonusActivateKey())
		{
			CaesiumBoostActive = !CaesiumBoostActive;
		}
		//}
	}
	public override void PostUpdate()
	{
		if (CaesiumBoostActive)
		{
			Player.AddBuff(ModContent.BuffType<CaesiumBoostingStance>(), 2);
		}
	}
}
