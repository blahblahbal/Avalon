using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Unobtainable;

[AutoloadEquip(EquipType.Body)]
public class UnderwearBody : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
	}
}
