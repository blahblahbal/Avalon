using Avalon.Items.Weapons.Melee.Swords;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_Unification : ModAchievement
{
	public override void SetStaticDefaults()
	{
		Achievement.SetCategory(AchievementCategory.Collector);

		AddItemCraftCondition(ModContent.ItemType<VertexOfExcalibur>());
	}
	public override Position GetDefaultPosition() => new After("SWORD_OF_THE_HERO");
}