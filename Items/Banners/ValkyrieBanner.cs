using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Banners;

public class ValkyrieBanner : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMonsterBanner(58);
	}
}
