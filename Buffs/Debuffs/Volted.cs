using Avalon.Common.Extensions;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class Volted : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}
	public override void Update(Player player, ref int buffIndex)
	{
		int type = Main.rand.Next(10) + 1;
		player.AddDOT(player.controlLeft || player.controlRight? 4 : 20, NetworkText.FromKey(type > 4 ? $"Mods.Avalon.DeathText.Electrocuted_{type - 4}" : $"DeathText.Electrocuted_{type}", $"{player.name}"));
		player.electrified = true;
	}
}
