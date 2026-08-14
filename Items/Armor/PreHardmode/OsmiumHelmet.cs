using Avalon.Common.Extensions;
using Avalon.Dusts;
using Avalon.Items.Material.Bars;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
public class OsmiumHelmet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(8);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<OsmiumJerkin>() && legs.type == ModContent.ItemType<OsmiumTreads>();
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Osmium");
		player.GetModPlayer<OsmiumSetBonusPlayer>().Active = true;
	}
	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 60;
		player.lifeRegen += 2;
		player.GetDamage(DamageClass.Generic) += 0.12f;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<OsmiumBar>(), 15)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 4)
			.AddTile(TileID.Anvils).Register();
	}
}
public class OsmiumSetBonusPlayer : ModPlayer
{
	public bool Active = false;
	public float LifeLeftToLose = 0;
	const float DAMAGE_TO_DOT_PERCENT = 0.25f;
	public float DOTTimer = 0;
	public override void ResetEffects()
	{
		Active = false;
	}
	public override void ModifyHurt(ref Player.HurtModifiers modifiers)
	{
		if (Active)
		{
			modifiers.FinalDamage *= (1f - DAMAGE_TO_DOT_PERCENT);
		}
	}
	public override void OnHurt(Player.HurtInfo info)
	{
		if (Active)
		{
			LifeLeftToLose += info.Damage * DAMAGE_TO_DOT_PERCENT;
		}
	}
	public override void PostUpdateMiscEffects()
	{
		int damage = Math.Min(3, (int)Math.Round(LifeLeftToLose));
		DOTTimer++;
		if(damage > 0 && Main.rand.NextBool(5))
		{
			int type = ModContent.DustType<SimpleColorableGlowyDustFlat>();
			Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, type);
			d.velocity += Player.velocity;
			d.color = new Color(0.3f, Main.rand.NextFloat(0.4f, 0.75f), 1f, 0.75f);
			d.noGravity = !Main.rand.NextBool(8);
			d.scale = d.noGravity ? 1.3f : 0.5f;
			d.noLight = true;

			Dust d2 = Dust.NewDustPerfect(d.position, type);
			d2.frame = d.frame;
			d2.rotation = d.rotation;
			d2.velocity = d.velocity;
			d2.color = new Color(1f, 1f, 1f, 0f);
			d2.noGravity = d.noGravity;
			d2.scale = d.scale * Main.rand.NextFloat(0.4f,0.8f);
			d2.noLight = true;
		}
		if(DOTTimer > 30)
		{
			DOTTimer = 0;
			if (LifeLeftToLose <= 0)
			{
				LifeLeftToLose = 0;
			}
			else if(damage > 0)
			{
				Player.statLife -= damage;
				LifeLeftToLose -= damage;
				CombatText.NewText(Player.Hitbox, Color.Lerp(CombatText.LifeRegenNegative, Color.DodgerBlue, 0.5f), damage, dramatic: false, dot: true);

				//CombatText.NewText(Player.Hitbox, CombatText.LifeRegenNegative, damage, dramatic: false, dot: true);
				if (Player.statLife <= 0 && Player.whoAmI == Main.myPlayer)
				{
					Player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromKey($"Mods.Avalon.DeathText.OsmiumSetBonus", Player.name)), 3, 0);
				}
			}
		}
	}
}
