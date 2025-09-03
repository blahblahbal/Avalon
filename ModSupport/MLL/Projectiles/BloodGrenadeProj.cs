using Avalon.ModSupport.MLL.Dusts;
using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Projectiles;

public class BloodGrenadeProj : LiquidGrenadeProjBase
{
	public override int DustType() => ModContent.DustType<BloodLiquidSplash>();
	public override int LiquidType() => LiquidLoader.LiquidType<Blood>();
}