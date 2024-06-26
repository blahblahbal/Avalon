using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Prefixes;

public class Robust : ExxoPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.35f;

    public override bool CanRoll(Item item) => true;

    public override void UpdateOwnerPlayer(Player player)
	{
		player.statDefense += 3;
		player.GetDamage(DamageClass.Generic) += 0.03f;
	}
}
