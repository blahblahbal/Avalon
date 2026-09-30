using Avalon.Common.DebuffPotency;
using Avalon.Common.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;

namespace Avalon.Common.Extensions;

public static class PlayerExtensions
{
	extension(Player player)
	{
		public float DebuffPotency { get => player.GetModPlayer<DebuffPotencyPlayer>().DebuffPotency; set => player.GetModPlayer<DebuffPotencyPlayer>().DebuffPotency = value; }
		public AvalonPlayer AvalonPlayer => player.GetModPlayer<AvalonPlayer>();

		public void AddDOT(int dps, NetworkText deathText)
		{
			var instance = player.AvalonPlayer;
			if (player.lifeRegen > 0)
				player.lifeRegen = 0;
			player.lifeRegen -= dps * 2;
			instance.DebuffDeathTextOverride = deathText;
		}
	}
}
