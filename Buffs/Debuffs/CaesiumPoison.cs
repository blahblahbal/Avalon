using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class CaesiumPoison : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}
	public override void Update(Player player, ref int buffIndex)
	{
		player.AddDOT(30, NetworkText.FromKey($"Mods.Avalon.DeathText.CaesiumPoison_1", $"{player.name}"));
		player.endurance -= 0.15f;
		player.blind = true;
	}
}
