using Avalon.Items.Accessories.Superhardmode;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Magic.Superhardmode;
using Avalon.Items.Weapons.Ranged.Superhardmode;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.BossBags;

public class WallofSteelBossBag : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.BossBag[Type] = true;
		Item.ResearchUnlockCount = 3;
	}

	public override void SetDefaults()
	{
		Item.DefaultToTreasureBag(ClassExtensions.TreasureBagRarities.WosTier);
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		//itemLoot.Add(ItemDropRule.ByCondition(new DropConditions.NotUsedMechHeart(), ModContent.ItemType<MechanicalHeart>()));
		itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulofBlight>(), 1, 40, 56));
		itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HellsteelPlate>(), 1, 20, 31));

		itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<FleshBoiler>(),
			ModContent.ItemType<MagicCleaver>(), ModContent.ItemType<BubbleBoost>() }));
	}
}
