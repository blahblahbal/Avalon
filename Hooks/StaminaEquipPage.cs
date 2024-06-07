using Avalon.Common;
using Terraria.UI;
using Terraria;

namespace Avalon.Hooks;

internal class StaminaEquipPage : ModHook
{
    protected override void Apply()
    {
        On_ItemSlot.SelectEquipPage += On_ItemSlot_SelectEquipPage;
    }

    private void On_ItemSlot_SelectEquipPage(On_ItemSlot.orig_SelectEquipPage orig, Item item)
    {
        if (!item.IsAir)
        {
			if (item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll)
			{
				Main.EquipPage = 2;
			}
			else orig.Invoke(item);
		}
		else orig.Invoke(item);
	}
}
