using Avalon.Common.DebuffEfficiency;
using Avalon.Common.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Avalon.Common.Extensions;

public static class PlayerExtensions
{
	extension(Player player)
	{
		public float DebuffEfficiency => player.GetModPlayer<DebuffEfficiencyPlayer>().DebuffEfficiency;
		public AvalonPlayer AvalonPlayer => player.GetModPlayer<AvalonPlayer>();
	}
}
