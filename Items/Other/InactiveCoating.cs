using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Other
{
	public class InactiveCoating : ModItem
	{
		public override void SetDefaults()
		{
			Item.paintCoating = Data.Sets.AvalonCoatingsID.ActuatorCoating;
			Item.DefaultToMisc(24, 24);
			Item.value = Item.buyPrice(0, 0, 2);
		}
	}
}
