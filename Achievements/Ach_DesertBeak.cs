using Avalon.NPCs.Bosses.PreHardmode.DesertBeak;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_DesertBeak : ModAchievement
{
	public override void SetStaticDefaults()
	{
		AddNPCKilledCondition(ModContent.NPCType<DesertBeak>());
	}
	public override Position GetDefaultPosition() => new After("ITS_GETTING_HOT_IN_HERE");
}