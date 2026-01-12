using Avalon.Tiles.Hellcastle;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_WoFsHouse : ModAchievement
{
	public override void SetStaticDefaults()
	{
		Achievement.SetCategory(AchievementCategory.Collector); // temple raider is also collector fsr

		AddTileDestroyedCondition([ModContent.TileType<UltraResistantWood>()]);
	}
	public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");
}