using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class UnderworldKey : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 20);
		Item.rare = ItemRarityID.Yellow;
	}

	public override bool ConsumeItem(Player player)
	{
		return NPC.downedPlantBoss;
	}
	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name)) && tt.Name.Equals("Tooltip0"));
		if (!NPC.downedPlantBoss)
		{
			var thing = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("LegacyTooltip.59"));
			tooltips.Insert(index, thing);
		}
		else
		{
			var thing = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("Mods.Avalon.UnderworldKey.AfterPlantera"));
			tooltips.Insert(index, thing);
		}
	}

	//public override void AddRecipes()
	//{
	//    CreateRecipe()
	//        .AddIngredient(ItemID.TempleKey)
	//        .AddIngredient(ModContent.ItemType<UnderworldKeyMold>())
	//        .AddIngredient(ItemID.SoulofFright, 5)
	//        .AddIngredient(ItemID.SoulofMight, 5)
	//        .AddIngredient(ItemID.SoulofSight, 5)
	//        .AddTile(TileID.MythrilAnvil)
	//        .Register();
	//}
}
