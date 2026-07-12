using Avalon.Common.Extensions;
using Terraria.ModLoader;

namespace Avalon.Items.Banners;

public abstract class BannerItem(int style) : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMonsterBanner(style);
	}
}
public class MimeBanner() : BannerItem(0);
public class BloodshotEyeBanner() : BannerItem(1);
public class BlazeBanner() : BannerItem(2);
public class BactusBanner() : BannerItem(3);
public class IckslimeBanner() : BannerItem(4);
public class OreSlimeBanner() : BannerItem(5);
public class MineralSlimeBanner() : BannerItem(6);
public class HellboundLizardBanner() : BannerItem(7);
public class GargoyleBanner() : BannerItem(8);
public class CursedScepterBanner() : BannerItem(9);
public class EctoHandBanner() : BannerItem(10);
public class CaesiumSeekerBanner() : BannerItem(11);
public class CaesiumBruteBanner() : BannerItem(12);
public class CaesiumStalkerBanner() : BannerItem(13);
public class RafflesiaBanner() : BannerItem(14);
public class PoisonDartFrogBanner() : BannerItem(15);
public class EvilVultureBanner() : BannerItem(16);
public class CursedFlamerBanner() : BannerItem(17);
public class FallenHeroBanner() : BannerItem(18);
public class TropicalSlimeBanner() : BannerItem(19);
public class ViralMummyBanner() : BannerItem(20);
public class VirisBanner() : BannerItem(21);
public class ContagionMimicBanner() : BannerItem(22);
public class BloodyVultureBanner() : BannerItem(23);
public class SicklyVultureBanner() : BannerItem(24);
public class DyeSlimeBanner() : BannerItem(25);
public class HalloworBanner() : BannerItem(26);
public class CougherBanner() : BannerItem(27);
public class PyrasiteBanner() : BannerItem(28);
public class IrateBonesBanner() : BannerItem(29);
public class BoneFishBanner() : BannerItem(30);