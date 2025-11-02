using Avalon.Items.Material;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_LuckyStiff : ModAchievement
{
	public override void SetStaticDefaults()
	{
		Achievement.SetCategory(AchievementCategory.Collector);

		AddItemPickupCondition(ModContent.ItemType<FourLeafClover>());
	}
}