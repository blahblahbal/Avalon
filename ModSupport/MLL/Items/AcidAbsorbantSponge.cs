using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;

namespace Avalon.ModSupport.MLL.Items;
public class AcidAbsorbantSponge : LiquidSpongeItemBase
{
	public override int LiquidType() => LiquidLoader.LiquidType<Acid>();
}