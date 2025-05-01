using Avalon.Buffs;
using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class RejuvenationPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(117, 26, 59),
			new Color(224, 40, 107),
			new Color(255, 151, 189)
		];
	}
	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name)) && tt.Name.Equals("Tooltip0"));
		if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().ThePill)
		{
			var thing = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("Mods.Avalon.RejuvenationPotion.ThePill"));
			tooltips.Insert(index, thing);
		}
		else
		{
			var thing = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("Mods.Avalon.RejuvenationPotion.NoThePill"));
			tooltips.Insert(index, thing);
		}
	}
	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(0, 0);
		Item.potion = true;
	}
	public override bool? UseItem(Player player)
	{
		player.AddBuff(ModContent.BuffType<Rejuvenation>(), 60 * 15);
		return true;
	}
	public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
	{
		player.AddBuff(ModContent.BuffType<Rejuvenation>(), 60 * 15);
		base.GetHealLife(player, quickHeal, ref healValue);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.BottledWater)
			.AddIngredient(ItemID.Mushroom)
			.AddIngredient(ItemID.PinkGel)
			.AddIngredient(ModContent.ItemType<Sweetstem>())
			.AddTile(TileID.Bottles)
			.Register();
	}
}
