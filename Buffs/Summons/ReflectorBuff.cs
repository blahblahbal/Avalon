using Avalon.Projectiles.Summon.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Summons;

public class ReflectorBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[Type] = true;
		Main.buffNoSave[Type] = true;
	}
	public override void Update(Player player, ref int buffIndex)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<Reflector>()] > 0)
		{
			player.buffTime[buffIndex] = 18000;
		}
		else
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
	}
}
