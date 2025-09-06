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
	public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
	{
		int questFish = attempt.questFish;
		int attemptLiquidType = Main.tile[attempt.X, attempt.Y].LiquidType;
		bool acid = attemptLiquidType == LiquidLoader.LiquidType<Acid>();
		bool blood = attemptLiquidType == LiquidLoader.LiquidType<Blood>();

		if (acid)
		{
			if (attempt.crate && Player.ZoneDungeon)
			{
				return;
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
					int r = Main.rand.Next(1);
					if (r == 0)
					{
						itemDrop = ModContent.ItemType<Acidskipper>();
						return;
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