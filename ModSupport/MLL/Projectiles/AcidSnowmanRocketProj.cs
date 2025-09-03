using Avalon.ModSupport.MLL.Dusts;
using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Projectiles;

public class AcidSnowmanRocketProj : LiquidSnowmanRocketProjBase
{
	public override int DustType() => ModContent.DustType<AcidLiquidSplash>();
	public override int LiquidType() => LiquidLoader.LiquidType<Acid>();
}