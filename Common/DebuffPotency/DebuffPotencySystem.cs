using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common.DebuffPotency;

public class DebuffPotencySystem : ModSystem
{
	public override void ModifyLightingBrightness(ref float scale)
	{
		float efficiency = 1f - Main.LocalPlayer.GetModPlayer<DebuffPotencyPlayer>().DebuffPotency;
		if (Main.LocalPlayer.blind)
		{
			scale *= 1f - (0.05f * efficiency);
		}
		if (Main.LocalPlayer.blackout)
		{
			scale *= 1f - (0.15f * efficiency);
		}
		if (Main.LocalPlayer.headcovered)
		{
			scale *= 1f - (0.15f * efficiency);
		}
	}
}
