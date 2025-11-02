using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_Rock : ModAchievement
{
	public required CustomFlagCondition ConditionFlag;
	public override void SetStaticDefaults()
	{
		Achievement.SetCategory(AchievementCategory.Challenger);

		ConditionFlag = AddCondition();
	}
}