using Terraria;
using Terraria.ModLoader;
using Avalon.Common.Players;

namespace Avalon.Buffs.Debuffs;

public class CaesiumPoison : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.lifeRegen > 0)
		{
			player.lifeRegen = 0;
		}
		player.lifeRegenTime = 0;
		player.GetModPlayer<AvalonPlayer>().CaesiumPoison = true;
		player.blind = true;
	}
}
