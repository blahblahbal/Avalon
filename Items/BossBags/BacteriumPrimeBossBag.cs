using Avalon.Common.Extensions;
using Avalon.Items.Accessories.Expert;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Items.Vanity;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.BossBags;

public class BacteriumPrimeBossBag : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.BossBag[Type] = true;
		ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
		Item.ResearchUnlockCount = 3;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTreasureBag(TreasureBagRarities.EvilTier);
	}
	public override bool CanRightClick()
	{
		return true;
	}
	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 1, 80, 111));
		itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Booger>(), 1, 20, 40));
		itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BadgeOfBacteria>(), 1));
		itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacteriumPrimeMask>(), 7));
	}
}
