using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class AcidBucket : LiquidBucketItemBase
{
	public override int LiquidType() => LiquidLoader.LiquidType<Acid>();
	public override bool OverwriteLiquids => true;
}
