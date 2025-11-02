using Avalon.Items.Accessories.PreHardmode;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_Safe : ModAchievement
{
	public override void SetStaticDefaults()
	{
		Achievement.SetCategory(AchievementCategory.Collector);

		AddItemCraftCondition(ModContent.ItemType<GuardianBoots>());
	}
}