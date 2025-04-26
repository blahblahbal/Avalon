using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
public class BacteriumPrimeMask : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBossMask();
	}
}
