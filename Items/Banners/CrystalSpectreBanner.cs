using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Banners;

public class CrystalSpectreBanner : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMonsterBanner(67);
	}
}
