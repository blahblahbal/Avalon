using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.Interfaces;

public interface IItemWithReleaseButtonMidSwingEffect
{
	void ReleaseButtonMidSwing(Player player);
}
public class ItemWithReleaseButtonMidSwingEffectPlayer : ModPlayer
{
	public bool HasReleasedButton = false;
	public override void ResetEffects()
	{
		if (Player.ItemAnimationJustStarted || !Player.ItemAnimationActive)
		{
			HasReleasedButton = false;
		}
	}
	public override bool PreItemCheck()
	{
		if(!HasReleasedButton && Player.HeldItem.ModItem is IItemWithReleaseButtonMidSwingEffect i && !Player.controlUseItem && Player.ItemAnimationActive)
		{
			i.ReleaseButtonMidSwing(Player);
			HasReleasedButton = true;
			NetMessage.SendData(MessageID.PlayerControls, number: Player.whoAmI);
		}
		return base.PreItemCheck();
	}
}