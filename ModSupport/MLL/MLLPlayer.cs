using Avalon.Data.Sets;
using Avalon.ModSupport.MLL.Items;
using Avalon.ModSupport.MLL.Liquids;
using Microsoft.Xna.Framework;
using ModLiquidLib.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL;
public class MLLPlayer : ModPlayer
{
	public bool accAcidFishing;
	public override void ResetEffects()
	{
		accAcidFishing = false;
	}
	public bool CanFishInAcid(FishingAttempt attempt) => ItemSets.CanFishInAcid[attempt.playerFishingConditions.PoleItemType] || ItemSets.IsAcidBait[attempt.playerFishingConditions.BaitItemType] || accAcidFishing;
	public override void ModifyFishingAttempt(ref FishingAttempt attempt)
	{
	}
	public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
	{
		int questFish = attempt.questFish;
		int attemptLiquidType = Main.tile[attempt.X, attempt.Y].LiquidType;
		bool acid = attemptLiquidType == LiquidLoader.LiquidType<Acid>();
		bool blood = attemptLiquidType == LiquidLoader.LiquidType<Blood>();

		if (acid)
		{
			if (!CanFishInAcid(attempt))
			{
				itemDrop = -1;
				return;
			}
			if (attempt.crate && Player.ZoneDungeon)
			{
				return;
			}
			else if (attempt.legendary && Main.rand.NextBool(3))
			{
				itemDrop = Main.rand.NextFromList([ModContent.ItemType<BottomlessAcidBucket>(), ModContent.ItemType<AcidAbsorbantSponge>(), ModContent.ItemType<AcidproofFishingHook>()]);
			}
			else if (attempt.rare && Player.ZoneDungeon && Main.hardMode)
			{
				itemDrop = ModContent.ItemType<Avalon.Items.Consumables.BiomeLockbox>();
			}
			else if (attempt.uncommon)
			{
				if (Player.ZoneDungeon && questFish == ModContent.ItemType<ArmoredBonefish>())
				{
					itemDrop = ModContent.ItemType<ArmoredBonefish>();
				}
				else
				{
					if (attempt.heightLevel > 1 && Main.rand.NextBool(5))
					{
						itemDrop = ModContent.ItemType<AlkalineJellyfish>();
					}
					else
					{
						// generic fish
						itemDrop = Main.rand.Next(1) switch
						{
							0 or _ => ModContent.ItemType<Acidskipper>()
						};
					}
				}
			}
			else
			{
				itemDrop = -1;
			}
			return;
		}
		if (blood)
		{
			itemDrop = -1;
			return;
		}
	}
}