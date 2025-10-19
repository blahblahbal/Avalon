using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.CrystalUnity;
public class AmberShardBuff : ModBuff
{
	public override void Update(Player player, ref int buffIndex)
	{
		player.GetAttackSpeed(DamageClass.Generic) += 0.15f;
	}
}
public class AmethystShardBuff : ModBuff
{
	public override void Update(Player player, ref int buffIndex)
	{
		player.statDefense += 6;
	}
}
public class RubyShardBuff : ModBuff
{
	public override void Update(Player player, ref int buffIndex)
	{
		player.GetArmorPenetration(DamageClass.Generic) += 5;
	}
}
public class SapphireShardBuff : ModBuff
{
	public override void Update(Player player, ref int buffIndex)
	{
		player.GetDamage(DamageClass.Magic) += 0.08f;
	}
}
