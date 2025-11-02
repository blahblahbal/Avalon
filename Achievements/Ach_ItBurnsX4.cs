using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_ItBurnsX4 : ModAchievement
{
	public required CustomFlagCondition ConditionFlag;
	public override void SetStaticDefaults()
	{
		ConditionFlag = AddCondition();
	}
}