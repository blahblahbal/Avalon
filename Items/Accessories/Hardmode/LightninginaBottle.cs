using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Material;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class LightninginaBottle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 3);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().LightningInABottle = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Bottle)
			.AddIngredient(ModContent.ItemType<Material.Shards.TornadoShard>(), 4)
			.AddIngredient(ModContent.ItemType<Material.LivingLightningBlock>(), 30)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class AngryNimbusDropsHook : ModHook
{
	protected override void Apply()
	{
		On_ItemDropDatabase.RegisterToNPC += On_ItemDropDatabase_RegisterToNPC;
	}

	private IItemDropRule On_ItemDropDatabase_RegisterToNPC(On_ItemDropDatabase.orig_RegisterToNPC orig, ItemDropDatabase self, int type, IItemDropRule entry)
	{
		if (type == NPCID.AngryNimbus)
		{
			entry = new AllFromOptionsDropRuleWithBonusRoll(8, 15, 1, 3, 1, 4, ModContent.ItemType<LivingLightningBlock>(), ItemID.Cloud, ItemID.RainCloud);
		}
		return orig.Invoke(self, type, entry);
	}
}
public class AllFromOptionsDropRuleWithBonusRoll : IItemDropRule
{
	public int[] dropIds;
	public int minDrop;
	public int maxDrop;
	public int bonusNumerator;
	public int bonusDenominator;
	public int bonusMin;
	public int bonusMax;
	public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

	public AllFromOptionsDropRuleWithBonusRoll(int minimumDropped, int maximumDropped, int bonusDropsNumerator, int bonusDropsDenominator, int minimumBonusDrops, int maximumBonusDrops, params int[] options)
	{
		minDrop = minimumDropped;
		maxDrop = maximumDropped;
		bonusNumerator = bonusDropsNumerator;
		bonusDenominator = bonusDropsDenominator;
		bonusMin = minimumBonusDrops;
		bonusMax = maximumBonusDrops;
		dropIds = options;
		ChainedRules = new List<IItemDropRuleChainAttempt>();
	}

	public bool CanDrop(DropAttemptInfo info)
	{
		return true;
	}

	public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
	{
		for (int i = 0; i < dropIds.Length; i++)
		{
			drops.Add(new DropRateInfo(dropIds[i], minDrop, maxDrop + bonusMax, ratesInfo.parentDroprateChance, ratesInfo.conditions));
		}
		Chains.ReportDroprates(ChainedRules, ratesInfo.parentDroprateChance, drops, ratesInfo);
	}

	public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
	{
		for (int i = 0; i < dropIds.Length; i++)
		{
			int stack = Main.rand.Next(minDrop, maxDrop + 1);
			if (Main.rand.NextBool(bonusNumerator, bonusDenominator))
			{
				stack += Main.rand.Next(bonusMin, bonusMax + 1);
			}
			CommonCode.DropItem(info, dropIds[i], stack);
		}
		ItemDropAttemptResult result = default(ItemDropAttemptResult);
		result.State = ItemDropAttemptResultState.Success;
		return result;
	}
}
