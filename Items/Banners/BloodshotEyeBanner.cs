using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Banners;

public class BloodshotEyeBanner : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMonsterBanner(7);
	}
}
