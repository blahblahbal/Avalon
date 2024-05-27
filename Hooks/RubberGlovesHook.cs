using Avalon.Common;
using Avalon.Common.Players;
using Terraria;

namespace Avalon.Hooks;

internal class RubberGlovesHook : ModHook
{
	protected override void Apply()
	{
		On_Player.ApplyAttackCooldown += On_Player_ApplyAttackCooldown;
		On_Player.SetMeleeHitCooldown += On_Player_SetMeleeHitCooldown;
	}

	private void On_Player_SetMeleeHitCooldown(On_Player.orig_SetMeleeHitCooldown orig, Player self, int npcIndex, int timeInFrames)
	{
		orig.Invoke(self, npcIndex, timeInFrames);
		if (self.GetModPlayer<AvalonPlayer>().RubberGloves)
		{
			self.meleeNPCHitCooldown[npcIndex] = self.itemAnimation / 6 * 5;
		}
	}

	private void On_Player_ApplyAttackCooldown(On_Player.orig_ApplyAttackCooldown orig, Player self)
	{
		orig.Invoke(self);
		if (self.GetModPlayer<AvalonPlayer>().RubberGloves)
		{
			self.attackCD = self.attackCD / 5 * 6;
			if (self.attackCD < 1) self.attackCD = 1;
		}
	}
}
