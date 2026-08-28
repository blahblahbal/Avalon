using Avalon.Buffs.Debuffs;
using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class RhodiumHeadgear : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(7);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.RhodiumBar>(), 15).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 4).AddTile(TileID.Anvils).Register();
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return (body.type == ModContent.ItemType<AncientTitaniumPlateMail>() || body.type == ModContent.ItemType<RhodiumPlateMail>()) &&
			(legs.type == ModContent.ItemType<AncientTitaniumGreaves>() || legs.type == ModContent.ItemType<RhodiumGreaves>());
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Rhodium");
		player.GetModPlayer<RhodiumSetBonusPlayer>().Active = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance(DamageClass.Generic) += 10;
		player.manaCost -= 0.1f;
	}
}
public class RhodiumSetBonusPlayer : ModPlayer
{
	public bool Active = false;
	public override void ResetEffects()
	{
		Active = false;
	}
	private void ApplyTag(NPC target)
	{
		target.AddBuff(ModContent.BuffType<RhodiumTag>(), 240);
		Player.MinionAttackTargetNPC = target.whoAmI;
	}
	private void ApplyDamage(NPC target, float multiplier, ref NPC.HitModifiers modifiers)
	{
		if (target.HasBuff<RhodiumTag>())
		{
			modifiers.FlatBonusDamage += 4 * multiplier;
			if (Main.rand.NextBool(8))
			{
				int i = Item.NewItem(target.GetSource_OnHit(target), target.Hitbox, ItemID.Star, 1, true, 0, false);
				Main.item[i].velocity += Main.rand.NextVector2Circular(5, 2);
				if (Main.netMode == NetmodeID.MultiplayerClient)
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i, 1f);
			}
			target.RequestBuffRemoval(ModContent.BuffType<RhodiumTag>());
		}
	}
	public override void ModifyHitNPCWithProj(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
	{
		if (projectile.npcProj || projectile.trap)
			return;
		if (!projectile.IsMinionOrSentryRelated && Active)
		{
			ApplyTag(target);
			return;
		}
		ApplyDamage(target, ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type], ref modifiers);
	}
	public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
	{
		if(Active)
			ApplyTag(target);
	}
}