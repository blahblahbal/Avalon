using Avalon.NPCs.Bosses.PreHardmode;
using Terraria.ModLoader;

namespace Avalon.Achievements;
public class Ach_BacteriumPrime : ModAchievement
{
	public override void SetStaticDefaults()
	{
		AddNPCKilledCondition(ModContent.NPCType<BacteriumPrime>());
	}
	public override Position GetDefaultPosition() => new After("MASTERMIND");
}