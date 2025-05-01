using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
public class DesertBeakMask : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBossMask();
	}
}
