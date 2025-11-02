using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_FalseAlarm : ModAchievement
{
	public required CustomFlagCondition ConditionFlag;
	public override void SetStaticDefaults()
	{
		ConditionFlag = AddCondition();
	}
	public override Position GetDefaultPosition() => new After("TIN_FOIL_HATTER");
}