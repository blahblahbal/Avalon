using Avalon.Items.Material;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.BossBags;

public class KingStingBossBag : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.BossBag[Type] = true;
		ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
		Item.ResearchUnlockCount = 3;
	}

	public override void SetDefaults()
	{
		Item.DefaultToTreasureBag(ClassExtensions.TreasureBagRarities.SkeleTier);
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaspFiber>(), 1, 16, 27)); // wasp fiber
		itemLoot.Add(ItemDropRule.Common(ItemID.LesserHealingPotion, 1, 5, 16)); // healing potions
		itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Vanity.KingStingMask>(), 7));
		// other loot comparable to queen bee
	}
}
