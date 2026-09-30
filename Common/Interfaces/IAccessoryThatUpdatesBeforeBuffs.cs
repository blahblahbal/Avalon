using Terraria;

namespace Avalon.Common.Interfaces;

internal interface IAccessoryThatUpdatesBeforeBuffs
{
	public void UpdateAccessoryEarly(Player player);
}
