using Avalon.Common.Extensions;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class IridiumHat : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(7);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<IridiumPlateMail>() && legs.type == ModContent.ItemType<IridiumPants>();
	}
	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Iridium");
		player.GetModPlayer<IridiumSetBonusPlayer>().Active = true;
	}
	public override void UpdateEquip(Player player)
	{
		player.GetCritChance(DamageClass.Generic) += 10;
		player.manaCost -= 0.2f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 15)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 4)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
public class IridiumSetBonusPlayer : ModPlayer
{
	public bool Active = false;
	public int ManaStealCooldown = 0;
	public override void ResetEffects()
	{
		ManaStealCooldown--;
		Active = false;
	}
	public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
	{
		if (!Active || !item.DamageType.CountsAsClass(DamageClass.Magic) || ContentSamples.ProjectilesByType[item.shoot].IsMinionOrSentryRelated)
			return;
		float ManaPercent = Player.statMana / (float)Player.statManaMax2;
		damage += Utils.Remap(ManaPercent, 0, 1f, 0.2f, 0);
	}
	public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (!proj.IsMinionOrSentryRelated && !proj.DamageType.CountsAsClass(DamageClass.Magic) && ManaStealCooldown <= 0)
		{
			ManaStealCooldown = 30;
			int manaRestore = Math.Min(10, Player.statManaMax2 - Player.statMana);
			if (manaRestore == 0)
				return;
			Player.statMana += manaRestore;
			Player.ManaEffect(manaRestore);
		}
	}
}
