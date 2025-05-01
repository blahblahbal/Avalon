using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Banners;

public class MatterManBanner : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMonsterBanner(43);
	}
}
