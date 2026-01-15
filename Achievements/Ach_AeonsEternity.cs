using Avalon.Items.Weapons.Melee.Swords;
using System.Collections.Generic;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_AeonsEternity : ModAchievement
{
	public override void SetStaticDefaults()
	{
		Achievement.SetCategory(AchievementCategory.Collector);

		AddItemCraftCondition(ModContent.ItemType<AeonsEternity>());
	}
	public override Position GetDefaultPosition() => new After("ITS_GETTING_HOT_IN_HERE");
	public override IEnumerable<Position> GetModdedConstraints()
	{
		yield return new After(ModContent.GetInstance<Ach_DesertBeak>());
	}
}