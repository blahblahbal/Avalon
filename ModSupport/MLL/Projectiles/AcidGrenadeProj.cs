using Avalon.ModSupport.MLL.Dusts;
using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Projectiles;

public class AcidGrenadeProj : LiquidGrenadeProjBase
{
	public override int DustType() => ModContent.DustType<AcidLiquidSplash>();
	public override int LiquidType() => LiquidLoader.LiquidType<Acid>();
}