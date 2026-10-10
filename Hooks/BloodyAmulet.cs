using Avalon.Common;
using Terraria;
using Terraria.ID;

namespace Avalon.Hooks;

internal class BloodyAmulet : ModHook
{
    public override void Load()
    {
		On_Player.ItemCheck_CheckCanUse += On_Player_ItemCheck_CheckCanUse;
		On_Player.ItemCheck_UseEventItems += On_Player_ItemCheck_UseEventItems;
    }

	private void On_Player_ItemCheck_UseEventItems(On_Player.orig_ItemCheck_UseEventItems orig, Player self, Item sItem)
	{
		if (sItem.type == ItemID.BloodMoonStarter) return;
		orig.Invoke(self, sItem);
	}

	private bool On_Player_ItemCheck_CheckCanUse(On_Player.orig_ItemCheck_CheckCanUse orig, Player self, Item sItem)
	{
		if (sItem.type == ItemID.BloodMoonStarter) return true;
		return orig.Invoke(self, sItem);
	}
}
