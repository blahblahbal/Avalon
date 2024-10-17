using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Avalon.Common;

namespace Avalon.Items.Other
{
	public class InactiveCoating : ModItem
	{
		public override void SetDefaults()
		{
			Item.paintCoating = Data.Sets.AvalonCoatingsID.ActuatorCoating;
			Item.width = 24;
			Item.height = 24;
			Item.value = Item.buyPrice(0, 0, 2);
			Item.buyPrice(silver: 2);
			Item.maxStack = Item.CommonMaxStack;
		}
	}
}
