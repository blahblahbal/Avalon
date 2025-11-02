using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_Fatality : ModAchievement
{
	public required CustomFlagCondition DrinkBottledLava;
	public override void SetStaticDefaults()
	{
		Achievement.SetCategory(AchievementCategory.Challenger);

		DrinkBottledLava = AddCondition();
	}
	public override Position GetDefaultPosition() => new After("DRINK_BOTTLED_WATER_WHILE_DROWNING");
}