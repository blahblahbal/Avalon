using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_Hellevator : ModAchievement
{
	public required CustomFlagCondition ConditionFlag;
	public override void SetStaticDefaults()
	{
		Achievement.SetCategory(AchievementCategory.Explorer);

		ConditionFlag = AddCondition();
	}
	public override Position GetDefaultPosition() => new After("ROCK_BOTTOM");
}