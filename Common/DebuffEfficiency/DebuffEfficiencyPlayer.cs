using Avalon.Common.Extensions;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common.DebuffEfficiency;

public class DebuffEfficiencyPlayer : ModPlayer
{
	/// <summary>
	/// Changes to this need to happen before buffs are updated, use IBuffThatNeedsToUpdatePlayerEarly if on a buff.
	/// </summary>
	public float DebuffEfficiency = 1;
	public override void ResetEffects()
	{
		DebuffEfficiency = 1;
	}
	public override void Load()
	{
		On_Player.UpdateJumpHeight += On_Player_UpdateJumpHeight;
	}

	private void On_Player_UpdateJumpHeight(On_Player.orig_UpdateJumpHeight orig, Player self)
	{
		orig(self);
		float efficiencyDiff = self.DebuffEfficiency - 1f;
		if (self.dazed)
		{
			Player.jumpHeight = (int)Math.Round(Player.jumpHeight * (1f - (efficiencyDiff * 1 / 5f)));
			Player.jumpSpeed = (int)Math.Round(Player.jumpHeight * (1f - (efficiencyDiff * 0.5f)));
		}
	}

	public override void PostUpdateMiscEffects()
	{
		float efficiencyDiff = DebuffEfficiency - 1f;
		if(Player.lifeRegen < 0)
		{
			Player.lifeRegen = (int)(Player.lifeRegen * (1f - efficiencyDiff));
		}
		if (Player.slowOgreSpit)
		{
			Player.moveSpeed *= 1f - (efficiencyDiff * 0.75f);
			if (Player.velocity.Y == 0f && Math.Abs(Player.velocity.X) > 1f)
			{
				Player.velocity.X *= 1f - (efficiencyDiff * 0.5f);
			}
		}
		if (Player.chilled)
		{
			Player.moveSpeed *= 1f - (efficiencyDiff * 0.25f);
		}
		if (Player.slow)
		{
			Player.moveSpeed *= 1f - (efficiencyDiff * 0.5f);
		}
		if (Player.burned)
		{
			Player.moveSpeed *= 1f - (efficiencyDiff * 0.5f);
		}
		if (Player.dazed)
		{
			Player.moveSpeed *= 1f - (efficiencyDiff * 0.666f);
		}
	}
	public override void NaturalLifeRegen(ref float regen)
	{
		if (Player.rabid)
			regen *= 1f - (DebuffEfficiency * 0.5f);
	}
}
