using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.BossBags;

public class ArmageddonSlimeBossBag : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetStaticDefaults()
	{
		ItemID.Sets.BossBag[Type] = true;
		Item.ResearchUnlockCount = 3;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTreasureBag(TreasureBagRarities.ArmaTier);
	}
	public override bool CanRightClick()
	{
		return true;
	}
	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		//itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkMatterSoilBlock>(), 1, 100, 211));
	}
}
