using Avalon.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Avalon.Hooks;

internal class DupePotionHook : ModHook
{
	protected override void Apply()
	{
		On_Projectile.AI_061_FishingBobber_GiveItemToPlayer += On_Projectile_AI_061_FishingBobber_GiveItemToPlayer;
	}

	private void On_Projectile_AI_061_FishingBobber_GiveItemToPlayer(On_Projectile.orig_AI_061_FishingBobber_GiveItemToPlayer orig, Projectile self, Player thePlayer, int itemType)
	{
		orig.Invoke(self, thePlayer, itemType);
		int stack = 1;
		if (itemType == ItemID.BombFish)
		{
			int finalFishingLevel = thePlayer.GetFishingConditions().FinalFishingLevel;
			int minValue = (finalFishingLevel / 20 + 3) / 2;
			int num = (finalFishingLevel / 10 + 6) / 2;
			if (Main.rand.Next(50) < finalFishingLevel)
			{
				num++;
			}
			if (Main.rand.Next(100) < finalFishingLevel)
			{
				num++;
			}
			if (Main.rand.Next(150) < finalFishingLevel)
			{
				num++;
			}
			if (Main.rand.Next(200) < finalFishingLevel)
			{
				num++;
			}
			stack = num;
		}
		if (itemType == ItemID.FrostDaggerfish)
		{
			int finalFishingLevel2 = thePlayer.GetFishingConditions().FinalFishingLevel;
			int minValue2 = (finalFishingLevel2 / 4 + 15) / 2;
			int num2 = (finalFishingLevel2 / 2 + 40) / 2;
			if (Main.rand.Next(50) < finalFishingLevel2)
			{
				num2 += 6;
			}
			if (Main.rand.Next(100) < finalFishingLevel2)
			{
				num2 += 6;
			}
			if (Main.rand.Next(150) < finalFishingLevel2)
			{
				num2 += 6;
			}
			if (Main.rand.Next(200) < finalFishingLevel2)
			{
				num2 += 6;
			}
			stack = num2;
		}
		if (Main.rand.NextBool(30))
		{
			Item.NewItem(new EntitySource_OverfullInventory(thePlayer), self.position, itemType, stack);
		}
	}
}
